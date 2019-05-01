using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Static
{
    public class Environment : GameObject
    {
       public List<BoundingBox> colliders;
        Wolf wolf;
    

        public Environment(Model model,Matrix world, float colliderSize) : base(world,model)
        {
           
            colliders = new List<BoundingBox>();

            foreach(ModelMesh mesh in model.Meshes)
            {
                float houseSize = colliderSize * 4.5f;
                if (mesh.Name.Contains("House"))
                {
                    BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - (houseSize+1) , mesh.BoundingSphere.Center.Y - houseSize, mesh.BoundingSphere.Center.Z - (houseSize+1) ),
              new Vector3(mesh.BoundingSphere.Center.X + (houseSize+1) , mesh.BoundingSphere.Center.Y + houseSize, mesh.BoundingSphere.Center.Z + (houseSize+1) ));
                    colliders.Add(box);
                }
                else if (mesh.Name.Contains("Tree"))
                {
                    
                    BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - colliderSize / 1.5f, mesh.BoundingSphere.Center.Y - colliderSize*3, mesh.BoundingSphere.Center.Z - colliderSize / 1.5f),
                new Vector3(mesh.BoundingSphere.Center.X + colliderSize / 1.5f, mesh.BoundingSphere.Center.Y + colliderSize / 2, mesh.BoundingSphere.Center.Z + colliderSize / 1.5f));
                    colliders.Add(box);
                }
                else if (mesh.Name.Contains("DeadTree"))
                {

                    BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - colliderSize / 2.0f, mesh.BoundingSphere.Center.Y - colliderSize * 3, mesh.BoundingSphere.Center.Z - colliderSize / 2.0f),
                new Vector3(mesh.BoundingSphere.Center.X + colliderSize / 2.0f, mesh.BoundingSphere.Center.Y + colliderSize / 2, mesh.BoundingSphere.Center.Z + colliderSize / 2.0f));
                    colliders.Add(box);
                }
                else if (mesh.Name.Contains("well"))
                {
                    
                    BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - colliderSize/1.8f , mesh.BoundingSphere.Center.Y - colliderSize * 3, mesh.BoundingSphere.Center.Z - colliderSize/1.8f ),
                new Vector3(mesh.BoundingSphere.Center.X + colliderSize /1.8f, mesh.BoundingSphere.Center.Y + colliderSize / 2, mesh.BoundingSphere.Center.Z + colliderSize/1.8f ));
                    colliders.Add(box);
                }
                else
                {
                BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - colliderSize / 2, mesh.BoundingSphere.Center.Y -colliderSize, mesh.BoundingSphere.Center.Z - colliderSize / 2),
                new Vector3(mesh.BoundingSphere.Center.X + colliderSize / 2, mesh.BoundingSphere.Center.Y + colliderSize/2, mesh.BoundingSphere.Center.Z + colliderSize / 2));
                    colliders.Add(box);
                }
               
            }
            
        }

        public override void Draw(Camera camera, string technique)
        {
            //foreach(ModelMesh mesh in model.Meshes)
            //{
                
                    base.Draw(camera, technique);
               
            //}
           
        }

        public void UpdateEnv(Wolf wolf)
        {
            this.wolf = wolf;
        }
    }
}
