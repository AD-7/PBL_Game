using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Static
{
    public class Reflekt: GameObject
    {

        private Effect effect;
        private TextureCube skyboxTexture;


        public Reflekt(Matrix world, Model model, ContentManager Content, TextureCube cube) : base(world, model)
        {

            skyboxTexture = cube;
            this.model = model;
            effect = Content.Load<Effect>("Effects/refle");
        }



        public void DrawModelWithEffect(Matrix view, Matrix projection, Camera camera)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = effect;
                    effect.Parameters["World"].SetValue(world * mesh.ParentBone.Transform);
                    effect.Parameters["View"].SetValue(view);
                    effect.Parameters["Projection"].SetValue(projection);
                    effect.Parameters["SkyboxTexture"].SetValue(skyboxTexture);
                    effect.Parameters["CameraPosition"].SetValue(camera.CamPos);
                    effect.Parameters["WorldInverseTranspose"].SetValue(
                                            Matrix.Transpose(Matrix.Invert(world * mesh.ParentBone.Transform)));
                }
                mesh.Draw();
            }
        }
    }
}
