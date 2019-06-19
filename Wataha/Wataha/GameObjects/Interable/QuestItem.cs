using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class QuestItem : InterableObject
    {

        public float angle;
        public Vector3 position;
        public float colliderSize;


        public QuestItem(Matrix world, Model model, float colliderSize) : base(world, model)
        {
            this.colliderSize = colliderSize;
            angle = 180;
            position = world.Translation;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));

            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90)) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);

            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);
            }

        }

        public override void Draw(Camera camera, string technique)
        {
            //foreach(ModelMesh mesh in model.Meshes)
            //{

            base.Draw(camera, technique);

            //}

        }
    }
}
