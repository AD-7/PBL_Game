﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Wataha.GameObjects.Static
{
    class Environment : GameObject
    {
       public List<BoundingBox> colliders;

        public Environment(Model model,Matrix world, float colliderSize)
        {
            base.model = model;
            base.world = world;
            colliders = new List<BoundingBox>();

            foreach(ModelMesh mesh in model.Meshes)
            {
                BoundingBox box = new BoundingBox(new Vector3(mesh.BoundingSphere.Center.X - colliderSize/2, mesh.BoundingSphere.Center.Y - colliderSize , mesh.BoundingSphere.Center.Z - colliderSize/2  ),
               new Vector3(mesh.BoundingSphere.Center.X + colliderSize / 2, mesh.BoundingSphere.Center.Y + colliderSize, mesh.BoundingSphere.Center.Z + colliderSize / 2));
                colliders.Add(box);
            }
            
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}