using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Wataha.GameObjects;
using Wataha.GameObjects.Materials;
using Wataha.GameSystem;

namespace Wataha.GameSystem
{
    public class PrelightingRenderer
    {
        RenderTarget2D depthTarg;
        RenderTarget2D normalTarg;
        RenderTarget2D lightTarg;

        Effect depthNormalEffect;
        Effect lightingEffect;

        Model lightMesh;

        public List<GameObject> Models { get; set; }
        public List<PPPointLight> Lights { get; set; }
        public Camera camera { get; set; }

        
        GraphicsDevice graphicsDevice;
        int viewWidth = 0, viewHeight = 0;

        public PrelightingRenderer(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            viewWidth = graphicsDevice.Viewport.Width;
            viewHeight = graphicsDevice.Viewport.Height;
            Models = new List<GameObject>();
            depthTarg = new RenderTarget2D(graphicsDevice, viewWidth, viewHeight, false, SurfaceFormat.Single, DepthFormat.Depth24);
            normalTarg = new RenderTarget2D(graphicsDevice, viewWidth, viewHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
            lightTarg = new RenderTarget2D(graphicsDevice, viewWidth, viewHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);

            depthNormalEffect = Content.Load<Effect>("Effects/PPDepthNormal");
            lightingEffect = Content.Load<Effect>("Effects/LightMap");

            lightingEffect.Parameters["viewportWidth"].SetValue((float)viewWidth);
            lightingEffect.Parameters["viewportHeight"].SetValue((float)viewHeight);

            lightMesh = Content.Load<Model>("PPLightMesh");
            lightMesh.Meshes[0].MeshParts[0].Effect = lightingEffect;
            this.graphicsDevice = graphicsDevice;
        }


        public void Draw()
        {
            drawDepthNormalMap();
            drawLightMap();
            prepareMainPass();
        }

        void drawDepthNormalMap()
        {
            graphicsDevice.SetRenderTargets(normalTarg, depthTarg);
            graphicsDevice.Clear(Color.White);
            foreach(GameObject model in Models)
            {
                model.CacheEffects();
           //     model.SetModelEffect(depthNormalEffect, false);
                //model.Draw(camera);
                model.RestoreEffects();
            }

            graphicsDevice.SetRenderTargets(null);

        }

        void drawLightMap()
        {
            lightingEffect.Parameters["DepthTexture"].SetValue(depthTarg);
            lightingEffect.Parameters["NormalTexture"].SetValue(normalTarg);

            Matrix viewProjection = camera.View * camera.Projection;

            Matrix invViewProjection = Matrix.Invert(viewProjection);
            lightingEffect.Parameters["InvViewProjection"].SetValue(invViewProjection);

            graphicsDevice.SetRenderTarget(lightTarg);
            graphicsDevice.Clear(Color.Black);

            graphicsDevice.BlendState = BlendState.Additive;
            graphicsDevice.DepthStencilState = DepthStencilState.None;

            foreach(PPPointLight light in Lights)
            {
                light.SetEffectParameters(lightingEffect);

                Matrix wvp = (Matrix.CreateScale(light.Attenuation)
                    * Matrix.CreateTranslation(light.Position)) * viewProjection;
                lightingEffect.Parameters["WorldViewProjection"].SetValue(wvp);

                float dist = Vector3.Distance(camera.CamPos, light.Position);
                if (dist < light.Attenuation)
                    graphicsDevice.RasterizerState = RasterizerState.CullClockwise;

                lightMesh.Meshes[0].Draw();

                graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;

               
            }
            graphicsDevice.BlendState = BlendState.Opaque;
            graphicsDevice.DepthStencilState = DepthStencilState.Default;

            graphicsDevice.SetRenderTarget(null);

        }
        void prepareMainPass()
        {
            foreach(GameObject o in Models)
                foreach(ModelMesh mesh in o.model.Meshes)
                    foreach(ModelMeshPart part in mesh.MeshParts)
                    {
                        if (part.Effect.Parameters["LightTexture"] != null)
                            part.Effect.Parameters["LightTexture"].SetValue(lightTarg);

                        if (part.Effect.Parameters["viewportWidth"] != null)
                            part.Effect.Parameters["viewportWidth"].SetValue((float)viewWidth);

                        if (part.Effect.Parameters["viewportHeight"] != null)
                            part.Effect.Parameters["viewportHeight"].SetValue((float)viewHeight);
                    }
        }

    }
}
