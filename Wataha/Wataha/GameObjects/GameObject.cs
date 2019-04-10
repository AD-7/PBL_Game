using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Wataha.GameObjects
{
    class GameObject
    {
        public Matrix world;
        
        public Model model;



        public virtual  void Draw()
        {
            
        }

        public virtual void Update()
        {

          
        }

        public void RotateY(float radians)
        {
            world *= Matrix.CreateRotationY(radians);
        }
        public void RotateX(float radians)
        {
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(radians));
        }
        public void RotateZ(float radians)
        {
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(radians));
        }
        public void Translate(Vector3 vector)
        {
            world *= Matrix.CreateTranslation(vector);
        } 
        public void Scale(float scale)
        {
            world *= Matrix.CreateScale(scale);
        }

        public void DrawModel(Model model,  Matrix view, Matrix projection)
        {
          
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                 
                 

                   effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                
                   
                }
              
                mesh.Draw();
            }
        }
    }
}
