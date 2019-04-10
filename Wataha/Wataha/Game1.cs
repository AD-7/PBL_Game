using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameObjects;
using Wataha.GameObjects.Static;
using Wataha.System;
using Wataha.GameObjects.Movable;

namespace Wataha
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


       


        private Forest example_forest;
        private Wolf wolf;
        private Matrix world;
        private Camera camera;

       
        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            camera = new Camera();
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
            world = world * Matrix.CreateScale(1f);
            world = world * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            example_forest = new Forest(Content.Load<Model>("terrain"), world);

            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90));
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(180));
            world *= Matrix.CreateTranslation(new Vector3(0, 2.5f,camera.CamPos.Z-10));
            world *= Matrix.CreateScale(0.7f);
            wolf = new Wolf(Content.Load<Model>("Wolf"),world,camera);
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



            // example_forest.RotateY(delta);

              world = Matrix.CreateTranslation(new Vector3(0, 0, 0));

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

          

          
            example_forest.DrawModel(example_forest.model, camera.View, camera.Projection);
            wolf.DrawModel(wolf.model, camera.View, camera.Projection);
            base.Draw(gameTime);
        }
    }
}
