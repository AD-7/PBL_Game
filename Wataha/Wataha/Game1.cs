using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Wataha.GameObjects;
using Wataha.GameObjects.Static;
using Wataha.System;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Interable;
using System.Collections.Generic;
using System.Diagnostics;

namespace Wataha
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameObjects.Static.Plane plane;
        private Wolf wolf;
        private List<QuestGiver> questGivers;
        private QuestGiver currentGiver;
        Skybox skybox;

        private Matrix world;
        private Camera camera;
        private ColisionSystem colisionSystem;
        private GameObjects.Static.Environment trees;

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            //graphics.IsFullScreen = true;
            graphics.GraphicsProfile = GraphicsProfile.HiDef;
            graphics.ApplyChanges();
            camera = new Camera();

            colisionSystem = new ColisionSystem();
            world = Matrix.CreateTranslation(new Vector3(0, 0, 0));


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
            // TODO: Add your initialization logic here
            questGivers = new List<QuestGiver>();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            Content = new ContentManager(this.Services, "Content");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            

            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            plane = new GameObjects.Static.Plane(Content.Load<Model>("plane"), world, 30);
            Matrix world3 = Matrix.CreateTranslation(new Vector3(0, 0, 0));
            trees = new GameObjects.Static.Environment(Content.Load<Model>("tres"), world3, 1);
            skybox = new Skybox("Skyboxes/SkyBox/SkyBox", Content);


            //world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            //world = world * Matrix.CreateScale(1f);
            //world = world * Matrix.CreateTranslation(new Vector3(10, 0, 0));
            //questGivers.Add(new QuestGiver(Content.Load<Model>("wolf"), world));



            Matrix world2 = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world2 *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world2 *= Matrix.CreateTranslation(new Vector3(0, 5.0f, camera.CamPos.Z - 10));
            world2 *= Matrix.CreateScale(0.5f);
            wolf = new Wolf(Content.Load<Model>("wolf"), world2, 4, camera);

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

            if (Keyboard.GetState().IsKeyDown(Keys.F) && currentGiver != null)
            {
                Debug.WriteLine("test");
            }

            ChceckNearestQuestGiver();


            if (colisionSystem.IsCollisionTerrain(wolf.collider, plane.collider) || colisionSystem.IsTreeCollision(wolf.collider, trees.colliders))
            {
                wolf.ifColisionTerrain = true;
            }





            wolf.Update();

            base.Update(gameTime);

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);


            plane.Draw(camera);
            //questGivers[0].Draw(camera);
            trees.Draw(camera);
            wolf.Draw(camera);

             RasterizerState originalRasterizerState = graphics.GraphicsDevice.RasterizerState;
             RasterizerState rasterizerState = new RasterizerState();
             rasterizerState.CullMode = CullMode.None;
             graphics.GraphicsDevice.RasterizerState = rasterizerState;

             skybox.Draw(camera);

             graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


            base.Draw(gameTime);
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
