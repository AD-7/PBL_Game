
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

        
        private GameObjects.Static.Environment trees;
        private GameObjects.Static.Environment b;
        private Effect simpleEffect;
        RenderTarget2D renderTarget;
        HUDController hud;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            //graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();
            camera = new Camera();
            hud = new HUDController(100, 0, 0);
            colisionSystem = new ColisionSystem();
            audioSystem = new AudioSystem(Content);
            
           // world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        

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
            device = GraphicsDevice;
            Content = new ContentManager(this.Services, "Content");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            simpleEffect = Content.Load<Effect>("Effects/Light");


            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            plane = new GameObjects.Static.Plane(Content.Load<Model>("plane"), world, 30);
            Matrix world3 = Matrix.CreateTranslation(new Vector3(0, 0, 0));
           
            skybox = new Skybox("Skyboxes/SkyBox/SkyBox", Content);
            hud.font30 = Content.Load<SpriteFont>("Fonts/font1");
            hud.pictures.Add(Content.Load<Texture2D>("Pictures/panel"));
            hud.pictures.Add(Content.Load<Texture2D>("Pictures/meat"));
            hud.pictures.Add(Content.Load<Texture2D>("Pictures/goldFangs"));
            hud.pictures.Add(Content.Load<Texture2D>("Pictures/whiteFang"));

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
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;


            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.E))
            {
                audioSystem.playGrowl(2);
            }


            if (Keyboard.GetState().IsKeyDown(Keys.F) && currentGiver != null)
            {
                Debug.WriteLine("test");
            }

            ChceckNearestQuestGiver();


            colisionSystem.IsCollisionTerrain(wolf.collider, plane.collider);
            colisionSystem.IsEnvironmentCollision(wolf, trees);
            colisionSystem.IsEnvironmentCollision(wolf, b);
          

             wolf.Update();


         
            

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
          
            graphics.GraphicsDevice.Clear(Color.Black);
         

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

            Vector3 forward = camera.CamTarget - camera.CamPos;
            Vector3 side = Vector3.Cross(forward, Vector3.Up);
            Vector3 up = Vector3.Cross(forward, side);
            Vector3 right = Vector3.Cross(forward, up);
        
              hud.Draw(spriteBatch,device);
               

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
