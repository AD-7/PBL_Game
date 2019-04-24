using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameSystem;
using System;

namespace Wataha.GameObjects
{
    public class GameObject
    {
        public Matrix world;
        public Model model;
       

        public virtual  void Draw()
        {
            
        }

        public virtual void Draw(Camera camera)
        {
            DrawModel(model, camera);
        }

        public virtual void Update()
        {

          
        }

        public void RotateY(float degrees)
        {
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(degrees));
        }
        public void RotateX(float degrees)
        {
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(degrees));
        }
        public void RotateZ(float degrees)
        {
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(degrees));
        }
        public void Translate(Vector3 vector)
        {
            world *= Matrix.CreateTranslation(vector);
        } 
        public void Scale(float scale)
        {
            world *= Matrix.CreateScale(scale);
        }
        public void Forward(Vector3 vector)
        {
          
        }

        public void DrawModel(Model model, Camera camera)
        {
          
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                
                   effect.World = world;
                    effect.View = camera.View;
                    effect.Projection = camera.Projection;
                
                   
                }

                if (Vector3.Distance(mesh.BoundingSphere.Center, camera.CamTarget) < 100.0f  || mesh.Name.Contains("Plane"))
                     mesh.Draw();
            }
        }
    }
}
