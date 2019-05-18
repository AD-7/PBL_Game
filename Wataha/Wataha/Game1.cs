
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
using Wataha.GameObjects;
using Wataha.GameSystem.Interfejs;

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
        private GameObjects.Static.Plane plane;
        private Wolf wolf, wolf2, wolf3;
        private Animal rabit;
        private Wataha.GameObjects.Movable.Wataha wataha;
        private List<QuestGiver> questGivers;
        private QuestGiver currentGiver;
        Skybox skybox;

        private Matrix world;
        private Camera camera;
        private ColisionSystem colisionSystem;
        private AudioSystem audioSystem;
        private ParticleSystem ps;

        private GameObjects.Static.Environment trees, huntingTrees;
        private GameObjects.Static.Environment b;
        private Effect simpleEffect;
        RenderTarget2D renderTarget;
        HUDController hud;

        private bool gameInMainMenu = true;
        private MainMenu mainMenu;

        BillboardSystem billboardTest;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            //graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();




        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        protected override void Initialize()
        {

            //questGivers = new List<QuestGiver>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            device = GraphicsDevice;


            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = device.DisplayMode.Height;
            graphics.PreferredBackBufferWidth = device.DisplayMode.Width;
            //   graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            Content = new ContentManager(this.Services, "Content");
            camera = new Camera();
            colisionSystem = new ColisionSystem();
            audioSystem = new AudioSystem(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            simpleEffect = Content.Load<Effect>("Effects/Light");
            ps = new ParticleSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/fire2"), 400, new Vector2(0.0001f, 0.00001f), 0.3f, Vector3.Zero, 0.1f);
            Vector3[] positions = new Vector3[1];
            positions[0] = new Vector3(0, 3, 0);


            billboardTest = new BillboardSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/questionMark"), new Vector2(0.001f), positions);


            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            plane = new GameObjects.Static.Plane(Content.Load<Model>("plane"), world, 30);
            Matrix world3 = Matrix.CreateTranslation(new Vector3(0, 0, 0));

            skybox = new Skybox("Skyboxes/skybox", Content);




            wataha = new GameObjects.Movable.Wataha(camera);

            Matrix world2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            world2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));

            world2 *= Matrix.CreateTranslation(new Vector3(0, 15.0f, camera.CamPos.Z - 5));
            world2 *= Matrix.CreateScale(0.2f);

            Matrix worldw2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw2 *= Matrix.CreateTranslation(new Vector3(14, 15.0f, camera.CamPos.Z - 12));
            worldw2 *= Matrix.CreateScale(0.2f);
            Matrix worldw3 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            worldw3 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            worldw3 *= Matrix.CreateTranslation(new Vector3(-10, 15.0f, camera.CamPos.Z - 7));
            worldw3 *= Matrix.CreateScale(0.2f);

            wolf = new Wolf(Content.Load<Model>("Wolf"), "wilk2", Content, world2, 3.0f, camera, 12, 10, 10, "Kimiko");
            wolf2 = new Wolf(Content.Load<Model>("Wolf2"), "wilk2", Content, worldw2, 3.0f, camera, 10, 8, 11, "Yua");
            wolf3 = new Wolf(Content.Load<Model>("Wolf3"), "wilk2", Content, worldw3, 3.0f, camera, 9, 9, 8, "Hatsu");
            rabit = new Animal(wolf, Content.Load<Model>("Wolf"), world2, 3.0f, camera, 1, 1, 1, "krol");
            rabit.SetModelEffect(simpleEffect, true);


            trees = new GameObjects.Static.Environment(Content.Load<Model>("tres"), world3, 2);
            huntingTrees = new GameObjects.Static.Environment(Content.Load<Model>("huntingTrees"), world3, 2);
            b = new GameObjects.Static.Environment(Content.Load<Model>("B1"), world3, 8);



            wolf.SetModelEffect(simpleEffect, true);
            wolf2.SetModelEffect(simpleEffect, true);
            wolf3.SetModelEffect(simpleEffect, true);
            trees.SetModelEffect(simpleEffect, true);
            huntingTrees.SetModelEffect(simpleEffect, true);
            b.SetModelEffect(simpleEffect, true);
            plane.SetModelEffect(simpleEffect, true);

            wolf2.texture = Content.Load<Texture2D>("textures/textureW");
            wolf2.SetTexture();
            wolf3.texture = Content.Load<Texture2D>("textures/textureW2");
            wolf3.SetTexture();
            wataha.wolves.Add(wolf);
            wataha.wolves.Add(wolf2);
            wataha.wolves.Add(wolf3);

            PresentationParameters pp = device.PresentationParameters;
            renderTarget = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, true, device.DisplayMode.Format, DepthFormat.Depth24);


            Matrix worldH = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            worldH *= Matrix.CreateRotationY(MathHelper.ToRadians(180));

            worldH *= Matrix.CreateTranslation(new Vector3(0, 15.0f, camera.CamPos.Z - 5));
            worldH *= Matrix.CreateScale(0.2f);


            HuntingSystem tmp = new HuntingSystem(camera, device, graphics, renderTarget, plane, huntingTrees, skybox);
            tmp.huntingWolf = new Wolf(Content.Load<Model>("Wolf"), "wilk2", Content, worldH, 3.0f, camera, 0, 0, 0, "S");
            tmp.huntingWolf.SetModelEffect(simpleEffect, true);
      
            hud = new HUDController(spriteBatch, device, Content, 100, 0, 0, wataha, tmp);



            mainMenu = new MainMenu(spriteBatch, Content, device);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 

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

                    if (mainMenu.ExitButtonsEvents())
                        Exit();
                    if (mainMenu.PlayButtonsEvents())
                    {
                        gameInMainMenu = false;
                        IsMouseVisible = false;
                        this.LoadContent();
                    }


                }
                else
                {



                    if (InputSystem.newKeybordState.IsKeyDown(Keys.Escape) && InputSystem.oldKeybordState.IsKeyUp(Keys.Escape) && !hud.ifPaused)
                    {
                        IsMouseVisible = true;
                        hud.ifPaused = true;
                        return;
                    }



                    if (!hud.ifPaused)
                    {


                        if (InputSystem.newKeybordState.IsKeyDown(Keys.E))
                        {
                            audioSystem.playGrowl(2);
                        }


                        if (InputSystem.newKeybordState.IsKeyDown(Keys.F) && currentGiver != null)
                        {
                            Debug.WriteLine("test");
                        }

                        //ChceckNearestQuestGiver();

                        foreach (Wolf w in wataha.wolves)
                        {
                            colisionSystem.IsCollisionTerrain(w.collider, plane.collider);
                            colisionSystem.IsEnvironmentCollision(w, trees, wataha);
                            colisionSystem.IsEnvironmentCollision(w, b, wataha);

                        }
                        colisionSystem.IsCollisionTerrain(rabit.collider, plane.collider);

                        wataha.Update(gameTime);

                        rabit.Update(gameTime);

                    }
                    else
                    {

                        if (hud.ResumeButtonEvent())
                        {
                            hud.ifPaused = false;
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

                    Vector3 offset = new Vector3(MathHelper.ToRadians(2.0f));
                    Vector3 randAngle = Vector3.Up + randVec3(-offset, offset);
                    Vector3 randPosition = randVec3(new Vector3(-1.5f), new Vector3(1.5f));
                    float randSpeed = (float)rand.NextDouble() * 2 + 10;

                    ps.AddParticle(randPosition, randAngle, randSpeed);
                    ps.Update();


                    hud.Update();

                }


            }
            else
            {
                hud.huntingSystem.Update(gameTime);
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            graphics.GraphicsDevice.Clear(Color.Black);



            if (!hud.huntingSystem.active)
            {
                if (gameInMainMenu)
                {
                    mainMenu.Draw();
                }
                else
                {


                    RasterizerState originalRasterizerState = graphics.GraphicsDevice.RasterizerState;
                    RasterizerState rasterizerState = new RasterizerState();
                    rasterizerState.CullMode = CullMode.None;
                    graphics.GraphicsDevice.RasterizerState = rasterizerState;

                    device.SetRenderTarget(renderTarget);
                    device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);




                    plane.Draw(camera, "ShadowMap");
                    //questGivers[0].Draw(camera);
                    foreach (Wolf w in wataha.wolves)
                    {
                        w.Draw(camera, "ShadowMap");
                    }
                    rabit.Draw(camera, "ShadowMap");

                    trees.Draw(camera, "ShadowMap");
                    b.Draw(camera, "ShadowMap");


                    device.SetRenderTarget(null);
                    plane.shadowMap = (Texture2D)renderTarget;
                    foreach (Wolf w in wataha.wolves)
                    {
                        w.shadowMap = (Texture2D)renderTarget;
                    }

                    trees.shadowMap = (Texture2D)renderTarget;
                    b.shadowMap = (Texture2D)renderTarget;


                    device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

                    device.BlendState = BlendState.AlphaBlend;

                    plane.Draw(camera, "ShadowedScene");
                    foreach (Wolf w in wataha.wolves)
                    {
                        w.Draw(camera, "ShadowedScene");
                    }
                    rabit.Draw(camera, "ShadowedScene");

                    //questGivers[0].Draw(camera);
                    trees.Draw(camera, "ShadowedScene");
                    b.Draw(camera, "ShadowedScene");
                    device.BlendState = BlendState.Opaque;
                    skybox.Draw(camera);

                    plane.shadowMap = null;
                    foreach (Wolf w in wataha.wolves)
                    {
                        w.shadowMap = null;
                    }

                    trees.shadowMap = null;
                    b.shadowMap = null;
                    graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


                    billboardTest.Draw(camera.View, camera.Projection, wolf.cam.up, camera.right);
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







        public void ChceckNearestQuestGiver()
        {
            foreach (QuestGiver giver in questGivers)
            {
                if (Vector3.Distance(wolf.world.Translation, giver.world.Translation) < 3.0f)
                {
                    currentGiver = giver;
                    return;
                }
                else
                {
                    currentGiver = null;
                }

            }
        }
    }
}
