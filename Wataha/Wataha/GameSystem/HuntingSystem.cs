using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Static;
using Wataha.GameSystem.Interfejs;

namespace Wataha.GameSystem
{

    public class HuntingSystem
    {

        Camera camera;
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        RenderTarget2D renderTarget;
        GameObjects.Static.Plane plane;
        GameObjects.Static.Environment trees;
        Skybox skybox;
        ColisionSystem colisionSystem;

        public HUDHunting hudHunting;
        public bool active = false;
        public List<Animal> animals;
        public Wolf huntingWolf;
        public Wataha.GameObjects.Movable.Wataha huntingWataha; // składa się z jednego wilka


        public HuntingSystem(Camera camera, GraphicsDevice device, GraphicsDeviceManager graphics, RenderTarget2D rt, GameObjects.Static.Plane plane, GameObjects.Static.Environment trees, Skybox skybox)
        {
            this.camera = camera;
            this.device = device;
            this.graphics = graphics;
            this.plane = plane;
            this.trees = trees;
            this.skybox = skybox;
            this.renderTarget = rt;
            colisionSystem = new ColisionSystem();
            huntingWataha = new GameObjects.Movable.Wataha(camera);


        }


        public void InitializeHunting(Wolf wolf)
        {
            huntingWolf.Name = wolf.Name;
            huntingWolf.strength = wolf.strength;
            huntingWolf.speed = wolf.speed;
            huntingWolf.resistance = wolf.resistance;
            huntingWolf.energy = wolf.energy;

            huntingWataha.wolves.Add(huntingWolf);

            if (wolf.resistance <= 8)
                hudHunting.seconds = 10;
            else if (wolf.resistance >= 9 && wolf.resistance <= 10)
                hudHunting.seconds = 15;
            else if (wolf.resistance >= 11 && wolf.resistance <= 15)
                hudHunting.seconds = 20;
            else if (wolf.resistance >= 16 && wolf.resistance <= 18)
                hudHunting.seconds = 25;
            else if (wolf.resistance >= 19 && wolf.resistance <= 20)
                hudHunting.seconds = 30;

            if (wolf.energy > 90)
                hudHunting.seconds += 5;
            else if (wolf.energy <= 90 && wolf.energy >= 80)
                hudHunting.seconds += 2;
            else if (wolf.energy < 70 && wolf.energy >= 60)
                hudHunting.seconds -= 4;
            else if (wolf.energy < 60)
                hudHunting.seconds -= 7;

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

            
            hudHunting.energyLoss = (wolf.strength *3 + wolf.resistance*2 + wolf.speed)/ 2;
            wolf.energy -= hudHunting.energyLoss;

        }


        public void ClearHunting()
        {

            Matrix worldH = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldH *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldH *= Matrix.CreateTranslation(new Vector3(0, 15.0f, camera.CamPos.Z - 5));
            worldH *= Matrix.CreateScale(0.2f);

            huntingWolf.position = worldH.Translation;
            huntingWolf.position += new Vector3(0, -1f, 0); ;
            huntingWolf.angle = 180;



            huntingWolf.strength = 0;
            huntingWolf.speed = 0;
            huntingWolf.resistance = 0;
            huntingWolf.energy = 0;

            huntingWataha.wolves.Clear();

            hudHunting.huntedMeat = 0;
            hudHunting.energyLoss = 0;
            hudHunting.ifInfoHuntingWindow = true;
            hudHunting.ifEndHuntingWindow = false;

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


            if (!hudHunting.ifInfoHuntingWindow && !hudHunting.ifEndHuntingWindow)
            {
                foreach (Wolf w in huntingWataha.wolves)
                {
                    colisionSystem.IsEnvironmentCollision(w, trees, huntingWataha);
                }


                huntingWataha.Update(gameTime);
            }

            hudHunting.Update(gameTime);
            
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

            plane.Draw(camera, "ShadowMap");

            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowMap");
            }
            trees.Draw(camera, "ShadowMap");

            device.SetRenderTarget(null);

            plane.shadowMap = (Texture2D)renderTarget;
            trees.shadowMap = (Texture2D)renderTarget;
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.shadowMap = (Texture2D)renderTarget;
            }


            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);
            device.BlendState = BlendState.AlphaBlend;

            plane.Draw(camera, "ShadowedScene");

            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowedScene");
            }
            trees.Draw(camera, "ShadowedScene");

            device.BlendState = BlendState.Opaque;

            skybox.Draw(camera);

            plane.shadowMap = null;
            trees.shadowMap = null;
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.shadowMap = null;
            }

            hudHunting.Draw();

            graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


        }





    }
}
