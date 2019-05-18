using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Static;

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

        public bool active = false;
        public List<Animal> animals;
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


        public void Update(GameTime gameTime)
        {
            foreach (Wolf w in huntingWataha.wolves)
            {
                colisionSystem.IsEnvironmentCollision(w, trees, huntingWataha);
            }


                huntingWataha.Update(gameTime);


        }

        public void Draw()
        {

            RasterizerState originalRasterizerState = graphics.GraphicsDevice.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = rasterizerState;


            device.SetRenderTarget(renderTarget);
            device.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, Color.Black, 1.0f, 0);

            plane.Draw(camera, "ShadowMap");
            trees.Draw(camera, "ShadowMap");
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowMap");
            }


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
            trees.Draw(camera, "ShadowedScene");
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.Draw(camera, "ShadowedScene");
            }


            device.BlendState = BlendState.Opaque;

            skybox.Draw(camera);

            plane.shadowMap = null;
            trees.shadowMap = null;
            foreach (Wolf w in huntingWataha.wolves)
            {
                w.shadowMap = null;
            }



            graphics.GraphicsDevice.RasterizerState = originalRasterizerState;


        }



    }
}
