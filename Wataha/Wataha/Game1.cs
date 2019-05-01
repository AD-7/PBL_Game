
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameSystem;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Interable;
using System.Collections.Generic;
using System.Diagnostics;
using Wataha.System;
using Wataha.System.ParticleSystem;
using System;
using Wataha.GameObjects;

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
        private Wolf wolf;
        private List<QuestGiver> questGivers;
        private QuestGiver currentGiver;
        Skybox skybox;

        private Matrix world;
        private Camera camera;
        private ColisionSystem colisionSystem;
        private AudioSystem audioSystem;
        private ParticleSystem ps;
        
        private GameObjects.Static.Environment trees;
        private GameObjects.Static.Environment b;
        private Effect simpleEffect;
        RenderTarget2D renderTarget;
        HUDController hud;

        private bool gameInMainMenu = true;
        private MainMenu mainMenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //IsMouseVisible = true;
            Window.AllowUserResizing = true;

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
       //     graphics.IsFullScreen = true;
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
           
            questGivers = new List<QuestGiver>();
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
          
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
       //   graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();

            device = GraphicsDevice;
            Content = new ContentManager(this.Services, "Content");
            camera = new Camera();
            colisionSystem = new ColisionSystem();
            audioSystem = new AudioSystem(Content);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            simpleEffect = Content.Load<Effect>("Effects/Light");
            ps = new ParticleSystem(GraphicsDevice, Content, Content.Load<Texture2D>("Pictures/fire2"), 400, new Vector2(0.0001f, 0.00001f), 0.3f, Vector3.Zero, 0.1f);


            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            plane = new GameObjects.Static.Plane(Content.Load<Model>("plane"), world, 30);
            Matrix world3 = Matrix.CreateTranslation(new Vector3(0, 0, 0));
           
            skybox = new Skybox("Skyboxes/SkyBox/SkyBox", Content);
           

            //world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            //world = world * Matrix.CreateScale(1f);
            //world = world * Matrix.CreateTranslation(new Vector3(10, 0, 0));
            //questGivers.Add(new QuestGiver(Content.Load<Model>("wolf"), world));



            Matrix world2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world2 *= Matrix.CreateTranslation(new Vector3(0, 5.0f, camera.CamPos.Z - 10));
            world2 *= Matrix.CreateScale(0.5f);
            wolf = new Wolf(Content.Load<Model>("Wolf"), world2, 3.0f, camera);
            trees = new GameObjects.Static.Environment(Content.Load<Model>("tres"),world3, 2);
            b = new GameObjects.Static.Environment(Content.Load<Model>("B1"), world3, 8);

            wolf.SetModelEffect(simpleEffect, true);
            trees.SetModelEffect(simpleEffect, true);
            b.SetModelEffect(simpleEffect, true);
            plane.SetModelEffect(simpleEffect, true);

            PresentationParameters pp = device.PresentationParameters;
            renderTarget = new RenderTarget2D(device, pp.BackBufferWidth, pp.BackBufferHeight, true, device.DisplayMode.Format, DepthFormat.Depth24);


            hud = new HUDController(spriteBatch, device, Content, 100, 0, 0);
            mainMenu = new MainMenu(spriteBatch, Content);

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
            KeyboardState newState = Keyboard.GetState();
            KeyboardState oldState = new KeyboardState(); 
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            hud.ScreenHeight = GraphicsDevice.Viewport.Height;
            hud.ScreenWidth = GraphicsDevice.Viewport.Width;

            if (gameInMainMenu)
            {
                IsMouseVisible = true;
                mainMenu.ScreenWidth = GraphicsDevice.Viewport.Width;
                mainMenu.ScreenHeight = GraphicsDevice.Viewport.Height;
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
               
              
                  if (newState.IsKeyDown(Keys.Escape) && oldState.IsKeyUp(Keys.Escape) && !hud.ifPaused)
                       {
                    IsMouseVisible = true;
                           hud.ifPaused = true;
                           return;
                      }



                if (!hud.ifPaused)
                {
                    if (newState.IsKeyDown(Keys.LeftControl))
                    {
                        IsMouseVisible = true;
                    }
                    else
                    {
                        IsMouseVisible = false;
                    }

                    if (newState.IsKeyDown(Keys.E))
                    {
                        audioSystem.playGrowl(2);
                    }


                    if (newState.IsKeyDown(Keys.F) && currentGiver != null)
                    {
                        Debug.WriteLine("test");
                    }

                    ChceckNearestQuestGiver();


                    colisionSystem.IsCollisionTerrain(wolf.collider, plane.collider);
                    colisionSystem.IsEnvironmentCollision(wolf, trees);
                    colisionSystem.IsEnvironmentCollision(wolf, b);

                  



                        wolf.Update();


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
                oldState = newState;
                base.Update(gameTime);

            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
          
            graphics.GraphicsDevice.Clear(Color.Black);
         
            if(gameInMainMenu)
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
            wolf.Draw(camera, "ShadowMap");
            
            trees.Draw(camera, "ShadowMap");
            b.Draw(camera, "ShadowMap");
            

            device.SetRenderTarget(null);
            plane.shadowMap = (Texture2D)renderTarget;
            wolf.shadowMap = (Texture2D)renderTarget;
            trees.shadowMap = (Texture2D)renderTarget;
            b.shadowMap = (Texture2D)renderTarget;


            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            device.BlendState = BlendState.AlphaBlend;

            plane.Draw(camera, "ShadowedScene");
            wolf.Draw(camera, "ShadowedScene");

            //questGivers[0].Draw(camera);
            trees.Draw(camera, "ShadowedScene");
            b.Draw(camera, "ShadowedScene");
            device.BlendState = BlendState.Opaque;
            skybox.Draw(camera);

            plane.shadowMap = null;
            wolf.shadowMap = null;
            trees.shadowMap = null;
            b.shadowMap = null;
            graphics.GraphicsDevice.RasterizerState = originalRasterizerState;

         


              hud.Draw();

              ps.Draw(camera.View, camera.Projection, wolf.cam.up, wolf.cam.right);



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
