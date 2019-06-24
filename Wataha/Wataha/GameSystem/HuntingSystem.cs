using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Wataha.GameObjects;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem.Interfejs;

namespace Wataha.GameSystem
{

    public class HuntingSystem
    {
        Random rand = new Random();
        Camera camera;
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        RenderTarget2D renderTarget;
        GameObjects.Static.Environment trees;
        Skybox skybox;
        ColisionSystem colisionSystem;
        Model rabitModel;
        Effect shadowEffect;

        public List<Animal> rabits;
        public List<Animal> sheeps;
        public List<Animal> boars;
        public Matrix spawnPoint;
        public List<Vector3> spawns;
        public HUDHunting hudHunting;
        public bool active = false;
        ContentManager Content;
        public Wolf huntingWolf;
        public Wataha.GameObjects.Movable.Wataha huntingWataha; // składa się z jednego wilka

        double time, timeS, timeB;


        public HuntingSystem(Camera camera, GraphicsDevice device, GraphicsDeviceManager graphics, RenderTarget2D rt, Model rabitModel, Effect effect,  GameObjects.Static.Environment trees, Skybox skybox, ContentManager Content)
        {
            this.Content = Content;
            this.camera = camera;
            this.device = device;
            this.graphics = graphics;
            this.trees = trees;
            this.skybox = skybox;
            this.renderTarget = rt;
            this.shadowEffect = effect;

            colisionSystem = new ColisionSystem();
            huntingWataha = new GameObjects.Movable.Wataha(camera);
            this.rabitModel = rabitModel;
            spawns = new List<Vector3>();
            rabits = new List<Animal>();
            sheeps = new List<Animal>();
            boars = new List<Animal>();
            GenerateVectors();


        }


        public void InitializeHunting(Wolf wolf)
        {
            Matrix worldH = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldH *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldH *= Matrix.CreateTranslation(new Vector3(0, 12f, camera.CamPos.Z - 5));
            worldH *= Matrix.CreateScale(0.2f);
            string wolfPath = "wilk2";
            if(wolf.Name == "Kimiko")
            {
                wolfPath = "wilk3";
            }
            else if(wolf.Name == "Hatsu")
            {
                wolfPath = "wilk4";
            }
            Dictionary<String, String> animationsW2 = new Dictionary<string, string>();
            animationsW2.Add("Idle", "wilk2");
            animationsW2.Add("Atak", "wilk2A");
            huntingWolf = new Wolf(Content.Load<Model>("Wolf2"), animationsW2, Content, worldH, 3.0f, camera, 0, 0, 0, "S");
            huntingWolf.Name = wolf.Name;
            huntingWolf.strength = wolf.strength;
            huntingWolf.speed = wolf.speed;
            huntingWolf.resistance = wolf.resistance;
            huntingWolf.energy = wolf.energy;
            huntingWolf.isHunting = true;

            huntingWataha.wolves.Add(huntingWolf);

            if (wolf.resistance <= 8)
                hudHunting.seconds = 12;
            else if (wolf.resistance >= 9 && wolf.resistance <= 10)
                hudHunting.seconds = 16;
            else if (wolf.resistance >= 11 && wolf.resistance <= 15)
                hudHunting.seconds = 20;
            else if (wolf.resistance >= 16 && wolf.resistance <= 18)
                hudHunting.seconds = 25;
            else if (wolf.resistance >= 19 && wolf.resistance <= 20)
                hudHunting.seconds = 30;

            if (wolf.energy > 90)
                hudHunting.seconds += 4;
            else if (wolf.energy <= 90 && wolf.energy >= 80)
                hudHunting.seconds += 2;
            else if (wolf.energy < 70 && wolf.energy >= 60)
                hudHunting.seconds -= 2;
            else if (wolf.energy < 60)
                hudHunting.seconds -= 4;

            if (wolf.strength < 8)
                hudHunting.maxMeat = 10;
            else if (wolf.strength >= 8 && wolf.strength <= 12)
                hudHunting.maxMeat = 30;
            else if (wolf.strength <= 15)
                hudHunting.maxMeat = 50;
            else if (wolf.strength <= 18)
                hudHunting.maxMeat = 80;
            else if (wolf.strength <= 20)
                hudHunting.maxMeat = 100;


            hudHunting.energyLoss = (wolf.strength * 3 + wolf.resistance * 2 + wolf.speed) / 2;
            wolf.energy -= hudHunting.energyLoss;
            for (int i = 0; i < 3; i++)
            {
                GenerateRabits(huntingWataha.wolves[0], rabitModel);
            
            }
     
       



        }


        public void ClearHunting()
        {
            Resources.Meat += hudHunting.huntedMeat;

            time = 0;
            timeS = 0;
            timeB = 0;

            //Matrix worldH = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            //worldH *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            //worldH *= Matrix.CreateTranslation(new Vector3(0, 15.0f, camera.CamPos.Z - 5));
            //worldH *= Matrix.CreateScale(0.2f);

            //huntingWolf.position = worldH.Translation;
            //huntingWolf.position += new Vector3(0, -1f, 0); ;
            //huntingWolf.angle = 180;



            huntingWolf.strength = 0;
            huntingWolf.speed = 0;
            huntingWolf.resistance = 0;
            huntingWolf.energy = 0;

            huntingWataha.wolves.Clear();

            hudHunting.huntedMeat = 0;
            hudHunting.energyLoss = 0;
            hudHunting.ifInfoHuntingWindow = true;
            hudHunting.ifEndHuntingWindow = false;

            rabits.Clear();
            sheeps.Clear();
            boars.Clear();


        }

        public void Update(GameTime gameTime)
        {
            if (InputSystem.newKeybordState.IsKeyDown(Keys.P))
            {
                ClearHunting();
                active = false;
            }
            if (hudHunting.okButtonEvent() && hudHunting.ifEndHuntingWindow)
            {
                ClearHunting();
                active = false;
            }



            if (time >= 3)
            {
                GenerateRabits(huntingWataha.wolves[0], rabitModel);
                time = 0;
            }
            else
            {
                time += gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            }

            if(timeS >= 6)
            {
                GenerateSheeps(huntingWataha.wolves[0], rabitModel);
                timeS = 0;
            }
            else
            {
                timeS += gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            }
            if(timeB >= 9)
            {
                GenerateBoars(huntingWataha.wolves[0], rabitModel);
                timeB = 0;
            }
            else
            {
                timeB += gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            }


            CheckKilledRabits();
            CheckKilledSheeps();
            CheckKilledBoars();

            if (!hudHunting.ifInfoHuntingWindow && !hudHunting.ifEndHuntingWindow)
            {
                foreach (Wolf w in huntingWataha.wolves)
                {
                    colisionSystem.IsEnvironmentCollision(w, trees, huntingWataha);
                }

                foreach (Animal a in rabits)
                {
                    if (a.active)
                    {
                        a.Update(gameTime);
                        colisionSystem.IsEnvironmentCollision(a, trees);
                    }


                }
                foreach (Animal a in sheeps)
                {
                    if (a.active)
                    {
                        a.Update(gameTime);
                        colisionSystem.IsEnvironmentCollision(a, trees);
                    }


                }
                foreach (Animal a in boars)
                {
                    if (a.active)
                    {
                        a.Update(gameTime);
                        colisionSystem.IsEnvironmentCollision(a, trees);
                    }


                }
                huntingWataha.Update(gameTime);
                GameObject.changeDay();

            }

            hudHunting.Update(gameTime);
            trees.Update(gameTime);


        }

        public void Draw()
        {

            graphics.GraphicsDevice.Clear(Color.Black);
            RasterizerState originalRasterizerState = graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState;


            device.SetRenderTarget(renderTarget);
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);




            foreach (Animal a in rabits)
            {
                if (a.active)
                    a.Draw(camera, "ShadowMap");
            }
            foreach (Animal a in sheeps)
            {
                if (a.active)
                    a.Draw(camera, "ShadowMap");
            }
            foreach (Animal a in boars)
            {
                if (a.active)
                    a.Draw(camera, "ShadowMap");
            }

            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowMap");
            }
            trees.Draw(camera, "ShadowMap");

            device.SetRenderTarget(null);




            trees.shadowMap = (Texture2D)renderTarget;

            foreach (Animal a in rabits)
            {
                if (a.active)
                    a.shadowMap = (Texture2D)renderTarget;
            }
            foreach (Animal a in sheeps)
            {
                if (a.active)
                    a.shadowMap = (Texture2D)renderTarget;
            }
            foreach (Animal a in boars)
            {
                if (a.active)
                    a.shadowMap = (Texture2D)renderTarget;
            }
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.shadowMap = (Texture2D)renderTarget;
            }


            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            device.BlendState = BlendState.AlphaBlend;



            foreach (Animal a in rabits)
            {
                if (a.active)
                    a.Draw(camera, "ShadowedScene");
            }
            foreach (Animal a in sheeps)
            {
                if (a.active)
                    a.Draw(camera, "ShadowedScene");
            }
            foreach (Animal a in boars)
            {
                if (a.active)
                    a.Draw(camera, "ShadowedScene");
            }
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowedScene");
            }
            trees.Draw(camera, "ShadowedScene");

            device.BlendState = BlendState.Opaque;

            skybox.Draw(camera);


            trees.shadowMap = null;
            foreach (Animal a in rabits)
            {
                if (a.active)
                    a.shadowMap = null;

            }
            foreach (Animal a in sheeps)
            {
                if (a.active)
                    a.shadowMap = null;

            }
            foreach (Animal a in boars)
            {
                if (a.active)
                    a.shadowMap = null;

            }
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.shadowMap = null;
            }

            hudHunting.Draw();

            graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


        }

        void GenerateVectors()
        {
            spawns.Add(new Vector3(-60f, 1.8f, 30f));
            spawns.Add(new Vector3(60f, 1.8f, -40f));
            spawns.Add(new Vector3(-30f, 1.8f, -50f));
            spawns.Add(new Vector3(55f, 1.8f, -80f));
            spawns.Add(new Vector3(45f, 1.8f, -110f));
            spawns.Add(new Vector3(0f, 1.8f, -80f));
            spawns.Add(new Vector3(45f, 1.8f, -190f));
            spawns.Add(new Vector3(-50f, 1.8f, -180f));
            spawns.Add(new Vector3(-35f, 1.8f, -20f));
            spawns.Add(new Vector3(55f, 1.8f, -60f));
        }
        private void GenerateSpawn()
        {



            Matrix world = new Matrix();
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(spawns[rand.Next(0, 9)]);

            spawnPoint = world;


        }

        public void GenerateRabits(Wolf wolf, Model model)
        {
            GenerateSpawn();
            Dictionary<String, String> animations = new Dictionary<string, string>();
            animations.Add("Idle", "RabitIdle");
            animations.Add("Move", "RabitM");
            Animal rabit = new Animal(wolf, model, animations, Content, spawnPoint, 8, 2, "rabit");
            rabit.ajustHeight(-1.05f);
            spawnPoint = new Matrix();
     
            rabit.animations["Idle"].generateTags();
            rabit.animations["Move"].generateTags();
            rabit.animations["Idle"].SetEffect(shadowEffect,true);
            rabit.animations["Move"].SetEffect(shadowEffect,true);

            rabits.Add(rabit);

        }
        public void GenerateSheeps(Wolf wolf, Model model)
        {
            GenerateSpawn();
            Dictionary<String, String> animations = new Dictionary<string, string>();
            animations.Add("Move", "SheepM");
            Animal sheep = new Animal(wolf, model, animations, Content, spawnPoint, 16,  8, "sheep");
            sheep.ajustHeight(-1.05f);
            sheep.animations["Move"].frameSpeed = 0.01f;
            spawnPoint = new Matrix();
            sheep.animationSystem.animation.generateTags();
            sheep.animationSystem.animation.SetEffect(shadowEffect, true);
            sheeps.Add(sheep);

        }
        public void GenerateBoars(Wolf wolf, Model model)
        {
            GenerateSpawn();
            Dictionary<String, String> animations = new Dictionary<string, string>();
            animations.Add("Move", "BoarM");
            Animal boar = new Animal(wolf, model, animations, Content, spawnPoint, 16, 20, "boar");
            boar.ajustHeight(-1.05f);
            boar.animations["Move"].frameSpeed = 0.01f;
            spawnPoint = new Matrix();
            boar.animationSystem.animation.generateTags();
            boar.animationSystem.animation.SetEffect(shadowEffect, true);
            boars.Add(boar);

        }
        void CheckKilledRabits()
        {
            List<Animal> tmp = new List<Animal>();
            foreach (Animal a in rabits)
            {
                if (Vector3.Distance(a.position, huntingWataha.wolves[0].position) <= 8 && InputSystem.newKeybordState.IsKeyDown(Keys.E) && InputSystem.oldKeybordState.IsKeyUp(Keys.E))
                {
                    AudioSystem.playGrowl(0);
                    a.active = false;
                    tmp.Add(a);

                    if (hudHunting.huntedMeat + a.meat <= hudHunting.maxMeat)
                    {
                        hudHunting.huntedMeat += a.meat;
                    }
                    else
                    {
                        hudHunting.huntedMeat = hudHunting.maxMeat;
                    }


                }
            }
            foreach (Animal a in tmp)
            {
                rabits.Remove(a);
            }
        }

        void CheckKilledSheeps()
        {
            List<Animal> tmp = new List<Animal>();
            foreach (Animal a in sheeps)
            {
                if (Vector3.Distance(a.position, huntingWataha.wolves[0].position) <= 6 && InputSystem.newKeybordState.IsKeyDown(Keys.E) && InputSystem.oldKeybordState.IsKeyUp(Keys.E))
                {
                    AudioSystem.playGrowl(0);
                    a.active = false;
                    tmp.Add(a);

                    if (hudHunting.huntedMeat + a.meat <= hudHunting.maxMeat)
                    {
                        hudHunting.huntedMeat += a.meat;
                    }
                    else
                    {
                        hudHunting.huntedMeat = hudHunting.maxMeat;
                    }


                }
            }
            foreach (Animal a in tmp)
            {
                sheeps.Remove(a);
            }
        }
        void CheckKilledBoars()
        {
            List<Animal> tmp = new List<Animal>();
            foreach (Animal a in boars)
            {
                if (Vector3.Distance(a.position, huntingWataha.wolves[0].position) <= 6 && InputSystem.newKeybordState.IsKeyDown(Keys.E) && InputSystem.oldKeybordState.IsKeyUp(Keys.E))
                {
                    AudioSystem.playGrowl(0);
                    a.active = false;
                    tmp.Add(a);

                    if (hudHunting.huntedMeat + a.meat <= hudHunting.maxMeat)
                    {
                        hudHunting.huntedMeat += a.meat;
                    }
                    else
                    {
                        hudHunting.huntedMeat = hudHunting.maxMeat;
                    }


                }
            }
            foreach (Animal a in tmp)
            {
                boars.Remove(a);
            }
        }
    }
}
