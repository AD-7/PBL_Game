using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameSystem;
using Wataha.GameSystem.ParticleSystem;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Interable;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using Wataha.GameSystem.Interfejs;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Linq;

namespace Wataha
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        GraphicsDevice device;
        SpriteBatch spriteBatch;
        Random rand = new Random();

        private Wolf wolf, wolf2, wolf3;
        private Animal rabit;
        private GameObjects.Movable.Wataha wataha;

        Skybox skybox;

        private Matrix world;
        private Camera camera;
        private ColisionSystem colisionSystem;
        private AudioSystem audioSystem;
        private ParticleSystem ps;
        private QuestSystem questSystem;

        private GameObjects.Static.Environment trees, huntingTrees;
        private GameObjects.Static.Environment blockade, blockade2, croft, barrell;
        private Effect simpleEffect;
        RenderTarget2D renderTarget;
        HUDController hud;

        private bool gameInMainMenu = true;
        private MainMenu mainMenu;

        BillboardSystem billboardTest;
        BillboardSystem billboardTest2;
        /// <summary>
        /// Spawn points
        /// </summary>
        public List<Vector3> spawns;
        public List<Animal> rabits;

        public Game1()
        {
            Trace.Listeners.Add((new TextWriterTraceListener("TextWriterOutput.log", "myListener")));
            Trace.AutoFlush = true;

            Trace.WriteLine("otaweto");

            graphics = new GraphicsDeviceManager(this);
            //  Window.AllowUserResizing = true;

            //graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            Content = new ContentManager(this.Services, "Content");
            audioSystem = new AudioSystem(Content);

            spawns = new List<Vector3>();
            rabits = new List<Animal>();
            GenerateVectors();
            
            Trace.WriteLine("Utorzono game1");

        }

        public void LoadGame()
        {
            Trace.WriteLine("LoadGAmeStart");

            string fileName = "save.txt";
            SaveSystem saveGameInfo = new SaveSystem();


            FileStream fs = new FileStream(fileName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                saveGameInfo = (SaveSystem)formatter.Deserialize(fs);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally { fs.Close(); }

            wolf.position = new Vector3(saveGameInfo.Wolf1PositionX, saveGameInfo.Wolf1PositionY, saveGameInfo.Wolf1PositionZ);
            wolf2.position = new Vector3(saveGameInfo.Wolf2PositionX, saveGameInfo.Wolf2PositionY, saveGameInfo.Wolf2PositionZ);
            wolf3.position = new Vector3(saveGameInfo.Wolf3PositionX, saveGameInfo.Wolf3PositionY, saveGameInfo.Wolf3PositionZ);
            wolf.strength = saveGameInfo.Wolf1Strength;
            wolf.resistance = saveGameInfo.Wolf1Resistance;
            wolf.speed = saveGameInfo.Wolf1Speed;
            wolf.energy = saveGameInfo.Wolf1Energy;
            wolf.Name = saveGameInfo.Wolf1Name;
            wolf2.strength = saveGameInfo.Wolf2Strength;
            wolf2.resistance = saveGameInfo.Wolf2Resistance;
            wolf2.speed = saveGameInfo.Wolf2Speed;
            wolf2.energy = saveGameInfo.Wolf2Energy;
            wolf2.Name = saveGameInfo.Wolf2Name;
            wolf3.strength = saveGameInfo.Wolf3Strength;
            wolf3.resistance = saveGameInfo.Wolf3Resistance;
            wolf3.speed = saveGameInfo.Wolf3Speed;
            wolf3.energy = saveGameInfo.Wolf3Energy;
            wolf3.Name = saveGameInfo.Wolf3Name;
            Resources.Meat = saveGameInfo.Meat;
            Resources.Goldfangs = saveGameInfo.GoldFang;
            Resources.Whitefangs = saveGameInfo.WhiteFang;


            Trace.WriteLine("LoadGameEND");

        }

        protected override void Initialize()
        {
            base.Initialize();
            Trace.WriteLine("inicializacjia");

        }

        protected override void LoadContent()
        {
            Trace.WriteLine("LaodCOntentStart");

            device = GraphicsDevice;

            Content.RootDirectory = "Content";
            //graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = device.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = device.DisplayMode.Width;
            //     graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            //graphics.SynchronizeWithVerticalRetrace = false;
            //IsFixedTimeStep = false;
            graphics.ApplyChanges();

            Trace.WriteLine("LoadContentGrahic");


            camera = new Camera();
            Trace.WriteLine("Camera");

            colisionSystem = new ColisionSystem();
            Trace.WriteLine("systemKolizi");

            questSystem = new QuestSystem();
            Trace.WriteLine("quest");




            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            simpleEffect = Content.Load<Effect>("Effects/Light");
            ps = new ParticleSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/fire2"), 400, new Vector2(0.0001f, 0.00001f), 0.3f, Vector3.Zero, 0.1f);


            Vector3[] positions = new Vector3[6];
            positions[0] = new Vector3(2, 0, 2);
            positions[1] = new Vector3(10, 0, -10);
            positions[2] = new Vector3(8, 0, -20);
            positions[3] = new Vector3(20, 0, -30);
            positions[4] = new Vector3(40, 0, -10);
            positions[5] = new Vector3(50, 0, -20);
            billboardTest = new BillboardSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/grass"), new Vector2(0.001f), positions);

            Trace.WriteLine("bilbord");


            //Vector3[] positions2 = new Vector3[6];
            //positions2[0] = new Vector3(2, 2, 2);
            //positions2[1] = new Vector3(10, 2, -10);
            //positions2[2] = new Vector3(8, 2, -20);
            //positions2[3] = new Vector3(20, 2, -30);
            //positions2[4] = new Vector3(40, 2, -10);
            //positions2[5] = new Vector3(50, 2, -20);

            //     billboardTest2 = new BillboardSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/questionMark"), new Vector2(0.001f), positions);


            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
        
            Matrix world3 = Matrix.CreateTranslation(new Vector3(0, 0, 0));

            skybox = new Skybox("Skyboxes/skybox", Content);

            Trace.WriteLine("skybox");

            wataha = new GameObjects.Movable.Wataha(camera);

            Matrix world2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            world2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));

            world2 *= Matrix.CreateTranslation(new Vector3(0, 12f, camera.CamPos.Z - 5));
            world2 *= Matrix.CreateScale(0.2f);

            Matrix worldw2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw2 *= Matrix.CreateTranslation(new Vector3(14, 12f, camera.CamPos.Z - 12));
            worldw2 *= Matrix.CreateScale(0.2f);
            Matrix worldw3 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw3 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw3 *= Matrix.CreateTranslation(new Vector3(-10, 12f, camera.CamPos.Z - 7));
            worldw3 *= Matrix.CreateScale(0.2f);

            wolf = new Wolf(Content.Load<Model>("Wolf"), "wilk2", Content, world2, 3.0f, camera, 12, 9, 10, "Kimiko");
            wolf2 = new Wolf(Content.Load<Model>("Wolf2"), "wilk3", Content, worldw2, 3.0f, camera, 10, 3, 11, "Yua");
            wolf3 = new Wolf(Content.Load<Model>("Wolf3"), "wilk4", Content, worldw3, 3.0f, camera, 9, 5, 8, "Hatsu");


            for(int i =0; i < 8; i++)
            {
                GenerateRabits(wolf, Content.Load<Model>("RabitIdle/Rabbitstand1_000001"));
            }


            Matrix worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw4 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw4 *= Matrix.CreateTranslation(new Vector3(10.0f, 2.4f, 0.0f));


            questSystem.questGivers.Add(new QuestGiver(Content.Load<Model>("lumberjack/lumberJack2"), worldw4));

            worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw4 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw4 *= Matrix.CreateTranslation(new Vector3(50.0f, 2.5f, -100.0f));

            questSystem.questGivers.Add(new QuestGiver(Content.Load<Model>("lumberjack/lumberJack"), worldw4));
            questSystem.questGivers[0].questsList.Add(new PointAtoBQuest(0, "Deliver letter", "Please deliver that letter \n to my brother blacksmith.", 1, 1, 1, 10, 1, 1, questSystem.questGivers[1]));
            questSystem.questGivers[1].questsList.Add(new DeliverQuest(1, "Repair dull chainsaw", "We need help with getting resoucers \nfor repair our saw. \nPlese bring to me 3 white fang \nand 1 gold fang", 4, 6, 5, 60, 0, 0, questSystem.questGivers[1], 1, 3, 0));
            questSystem.questGivers[0].Init();
            questSystem.questGivers[1].Init();


                //foreach(QuestGiver q in questSystem.questGivers)
                //{
                //    q.SetModelEffect(simpleEffect, true);
                //}

            trees = new GameObjects.Static.Environment(Content.Load<Model>("tres"), world3, 2);
            huntingTrees = new GameObjects.Static.Environment(Content.Load<Model>("huntingTrees"), world3, 2);
            blockade = new GameObjects.Static.Environment(Content.Load<Model>("B1"), world3, 8);
            Matrix worldb2 = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            blockade2 = new GameObjects.Static.Environment(Content.Load<Model>("B2"), worldb2, 10);
            croft = new GameObjects.Static.Environment(Content.Load<Model>("croft"), worldb2, 35);
            barrell = new GameObjects.Static.Environment(Content.Load<Model>("barrell"), worldb2, 5);



            wolf.SetModelEffect(simpleEffect, true);
            wolf2.SetModelEffect(simpleEffect, true);
            wolf3.SetModelEffect(simpleEffect, true);
            trees.SetModelEffect(simpleEffect, true);
            huntingTrees.SetModelEffect(simpleEffect, true);
            blockade.SetModelEffect(simpleEffect, true);
            blockade2.SetModelEffect(simpleEffect, true);
            croft.SetModelEffect(simpleEffect, true);
            barrell.SetModelEffect(simpleEffect, true);
         

       
            wolf.animationSystem.animation.generateTags();
            wolf.animationSystem.animation.SetEffect(simpleEffect, true);
            wolf2.animationSystem.animation.generateTags();
            wolf2.animationSystem.animation.SetEffect(simpleEffect, true);
            wolf3.animationSystem.animation.generateTags();
            wolf3.animationSystem.animation.SetEffect(simpleEffect, true);

            wataha.wolves.Add(wolf);
            wataha.wolves.Add(wolf2);
            wataha.wolves.Add(wolf3);

            PresentationParameters pp = device.PresentationParameters;
            renderTarget = new RenderTarget2D(device, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight, false, SurfaceFormat.Single, DepthFormat.Depth24, 0, RenderTargetUsage.PlatformContents);


            Matrix worldH = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            worldH *= Matrix.CreateRotationY(MathHelper.ToRadians(180));

            worldH *= Matrix.CreateTranslation(new Vector3(0, 15.0f, camera.CamPos.Z - 5));
            worldH *= Matrix.CreateScale(0.2f);


            HuntingSystem tmp = new HuntingSystem(camera, device, graphics, renderTarget, Content.Load<Model>("RabitIdle/Rabbitstand1_000001"), simpleEffect,  huntingTrees, skybox, Content);
            tmp.huntingWolf = new Wolf(Content.Load<Model>("Wolf2"), "wilk2", Content, worldH, 3.0f, camera, 0, 0, 0, "S");
            tmp.huntingWolf.SetModelEffect(simpleEffect, true);


            hud = new HUDController(spriteBatch, device, Content, 50, 0, 0, wataha, tmp);

            mainMenu = new MainMenu(spriteBatch, Content, device);

            Trace.WriteLine("LoadContentEnd");

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        float timer = 20;
        protected override void Update(GameTime gameTime)
        {
            InputSystem.oldKeybordState = InputSystem.newKeybordState;
            InputSystem.newKeybordState = Keyboard.GetState();
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            IsMouseVisible = true;

            if (!hud.huntingSystem.active)
            {
                if (gameInMainMenu)
                {
                    mainMenu.Update();

                    if (mainMenu.ExitButtonEvent())
                        Exit();
                    if (mainMenu.NewGameButtonEvent())
                    {
                        gameInMainMenu = false;
                        IsMouseVisible = false;
                        mainMenu.ifIntro = true;
                    }
                    if (mainMenu.LoadButtonEvent())
                    {
                        gameInMainMenu = false;
                        IsMouseVisible = false;
                        LoadContent();
                        LoadGame();
                    }
                }
                else if (mainMenu.ifIntro)
                {
                    mainMenu.intro.Update(gameTime);
                    if(mainMenu.intro.IntroEvent())
                    {
                        mainMenu.ifIntro = false;
                        this.LoadContent();
                    }

                    if (InputSystem.newKeybordState.IsKeyDown(Keys.Space))
                    {
                        mainMenu.ifIntro = false;
                        this.LoadContent();
                    }
                }
                else
                {
                    if (InputSystem.newKeybordState.IsKeyDown(Keys.Escape) && InputSystem.oldKeybordState.IsKeyUp(Keys.Escape) && !hud.ifPaused && !hud.ifGameOver)
                    {
                        IsMouseVisible = true;
                        hud.ifPaused = true;
                        return;
                    }

                    if (!hud.ifPaused && !hud.ifGameOver)
                    {
                        if (InputSystem.newKeybordState.IsKeyDown(Keys.E))
                        {
                            AudioSystem.playGrowl(2);
                        }

                        if (InputSystem.newKeybordState.IsKeyDown(Keys.F) && InputSystem.oldKeybordState.IsKeyUp(Keys.F) && QuestSystem.currentGiver != null && QuestSystem.currentGiver.actualQuest != QuestSystem.currentQuest)
                        {
                            hud.ifQuestPanel = true;
                        }

                        if (questSystem.ChceckNearestQuestGiver(wataha.wolves[0]))
                        {

                        }
                        else
                        {
                            hud.ifQuestPanel = false;
                        }

                        if (hud.marketPanel.CheckIfWolfIsClose(wataha.wolves[0], barrell))
                        {
                            hud.marketPanel.infoActive = true;
                        }
                        else
                        {
                            hud.marketPanel.infoActive = false;
                            hud.marketPanel.active = false;
                        }

                        if (hud.marketPanel.infoActive && InputSystem.newKeybordState.IsKeyDown(Keys.F) && InputSystem.oldKeybordState.IsKeyUp(Keys.F))
                        {
                            hud.marketPanel.active = true;
                        }

                        foreach (Wolf w in wataha.wolves)
                        {

                            colisionSystem.IsEnvironmentCollision(w, trees, wataha);
                            colisionSystem.IsEnvironmentCollision(w, blockade, wataha);
                            colisionSystem.IsEnvironmentCollision(w, blockade2, wataha);
                            colisionSystem.IsEnvironmentCollision(w, croft, wataha);
                            colisionSystem.IsEnvironmentCollision(w, barrell, wataha);
                        }
                        foreach (Animal rabit in rabits)
                        {
                            colisionSystem.IsEnvironmentCollision(rabit, trees);
                        }

                        wataha.Update(gameTime);
                        questSystem.Update(gameTime, wataha.wolves[0]);
                        foreach (Animal rabit in rabits)
                        {
                            rabit.Update(gameTime);
                        }
                    }
                    else
                    {
                        if (!hud.ifGameOver)
                        {
                            if (!hud.ifSaveInfo)
                            {
                                if (hud.ResumeButtonEvent())
                                {
                                    hud.ifPaused = false;
                                }
                                if (hud.SaveButtonEvent())
                                {
                                    SaveSystem saver = new SaveSystem(hud.wataha.wolves.ElementAt(0), hud.wataha.wolves.ElementAt(1), hud.wataha.wolves.ElementAt(2));
                                    saver.SaveGame();
                                    hud.ifSaveInfo = true;
                                }
                                if (hud.BackToMainMenuButtonEvent())
                                {
                                    hud.ifPaused = false;
                                    gameInMainMenu = true;
                                }
                                if (hud.ExitButtonEvent())
                                {
                                    Exit();
                                }
                            }
                            else
                            {
                                hud.InfoSaveOkEvent();
                            }
                        }
                        else
                        {
                           if(hud.InfoGameOverOkEvent())
                            {
                                hud.ifPaused = false;
                                hud.ifGameOver = false;
                                gameInMainMenu = true;

                            }
                        }
                    }

                    Vector3 offset = new Vector3(MathHelper.ToRadians(2.0f));
                    Vector3 randAngle = Vector3.Up + randVec3(-offset, offset);
                    Vector3 randPosition = randVec3(new Vector3(-1.5f), new Vector3(1.5f));
                    float randSpeed = (float)rand.NextDouble() * 2 + 10;

                    ps.AddParticle(randPosition, randAngle, randSpeed);
                    ps.Update();

                    hud.Update(gameTime);
                }
            }

            else
            {
                hud.huntingSystem.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);


            if (!hud.huntingSystem.active)
            {
                if (gameInMainMenu)
                {
                    mainMenu.Draw();
                }
                else if(mainMenu.ifIntro)
                {
                    mainMenu.intro.Draw();
                }
                else
                {
                    RasterizerState originalRasterizerState = graphics.GraphicsDevice.RasterizerState;
                    RasterizerState rasterizerState = new RasterizerState();
                    rasterizerState.CullMode = CullMode.None;
                    graphics.GraphicsDevice.RasterizerState = rasterizerState;

                    device.SetRenderTarget(renderTarget);
                    device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);


                    


                    foreach (Wolf w in wataha.wolves)
                    {
                        w.Draw(camera, "ShadowMap");
                    }
                    foreach (Animal rabit in rabits)
                    {
                    rabit.Draw(camera, "ShadowMap");
                    }

                    //foreach (QuestGiver q in questSystem.questGivers)
                    //{
                    //    q.Draw(camera, "ShadowMap");
                    //}
                    trees.Draw(camera, "ShadowMap");
                    blockade.Draw(camera, "ShadowMap");
                    blockade2.Draw(camera, "ShadowMap");
                    croft.Draw(camera, "ShadowMap");
                    barrell.Draw(camera, "ShadowMap");
                    device.SetRenderTarget(null);


                    foreach (Wolf w in wataha.wolves)
                    {
                        w.shadowMap = (Texture2D)renderTarget;
                    }
                    foreach (Animal rabit in rabits)
                    {
                        rabit.shadowMap = (Texture2D)renderTarget;
                    }
                    //foreach (QuestGiver q in questSystem.questGivers)
                    //{
                    //    q.shadowMap = (Texture2D)renderTarget;
                    //}
                    trees.shadowMap = (Texture2D)renderTarget;
                    blockade.shadowMap = (Texture2D)renderTarget;
                    blockade2.shadowMap = (Texture2D)renderTarget;
                    croft.shadowMap = (Texture2D)renderTarget;
                    barrell.shadowMap = (Texture2D)renderTarget;
                    device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

                    device.BlendState = BlendState.AlphaBlend;


                    foreach (Wolf w in wataha.wolves)
                    {
                        w.Draw(camera, "ShadowedScene");
                    }
                    foreach(Animal rabit in rabits)
                    {
                        rabit.Draw(camera, "ShadowedScene");
                    }

                    foreach (QuestGiver q in questSystem.questGivers)
                    {
                        q.Draw(camera, "ShadowedScene");
                    }
                    trees.Draw(camera, "ShadowedScene");
                    blockade.Draw(camera, "ShadowedScene");
                    blockade2.Draw(camera, "ShadowedScene");
                    croft.Draw(camera, "ShadowedScene");
                    barrell.Draw(camera, "ShadowedScene");
                    device.BlendState = BlendState.Opaque;
                    skybox.Draw(camera);


                    //foreach (QuestGiver q in questSystem.questGivers)
                    //{
                    //    q.shadowMap = null;
                    //}

                    foreach (Wolf w in wataha.wolves)
                    {
                        w.shadowMap = null;
                    }
                    foreach (Animal rabit in rabits)
                    {
                        rabit.shadowMap = null;
                    }
                    trees.shadowMap = null;
                    blockade.shadowMap = null;
                    blockade2.shadowMap = null;
                    croft.shadowMap = null;
                    barrell.shadowMap = null;
                    graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


                    billboardTest.Draw(camera.View, camera.Projection, wolf.cam.up, camera.right);
                    //billboardTest2.Draw(camera.View, camera.Projection, wolf.cam.up, camera.right);
                    ps.Draw(camera.View, camera.Projection, wolf.cam.up, wolf.cam.right);

                    hud.Draw();
                }

            }

            else
            {
                hud.huntingSystem.Draw();
            }

            base.Draw(gameTime);
        }

        Vector3 randVec3(Vector3 min, Vector3 max)
        {
            return new Vector3
                (min.X + (float)rand.NextDouble() * (max.X - min.X),
                min.Y + (float)rand.NextDouble() * (max.Y - min.Y),
                min.Z + (float)rand.NextDouble() * (max.Z - min.Z));
        }
        private Matrix GenerateSpawn()
        {
            Matrix world = new Matrix();
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(spawns[rand.Next(0, 6)]);

            return world;


        }

        public void GenerateRabits(Wolf wolf, Model model)
        {
            Matrix spawnPoint = GenerateSpawn();
            Dictionary<String, String> animations = new Dictionary<string, string>();
            animations.Add("Idle", "RabitIdle");
            animations.Add("Move", "RabitM");
            Animal rabit = new Animal(wolf, model, animations, Content, spawnPoint, 8, 5,"rabit");
            rabit.ajustHeight(-1.05f);
            spawnPoint = new Matrix();
            rabit.animations["Idle"].generateTags();
            rabit.animations["Move"].generateTags();
            rabit.animations["Idle"].SetEffect(simpleEffect, true);
            rabit.animations["Move"].SetEffect(simpleEffect, true);
            rabits.Add(rabit);

        }
        void GenerateVectors()
        {
            spawns.Add(new Vector3(-60f, 2f, 20f));
            spawns.Add(new Vector3(60f, 2f, -40f));
            spawns.Add(new Vector3(-30f, 2f, -50f));
            spawns.Add(new Vector3(55f, 2f, -80f));
            spawns.Add(new Vector3(45f, 2f, -90f));
            spawns.Add(new Vector3(0f, 2f, -80f));

            spawns.Add(new Vector3(-35f, 2f, -20f));
        }
    }
}
