using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Movable
{
    public class Animal : GameObject
    {
        public int meat = 10;
        public float walkSpeed = 10;
        float dirX, dirZ, angle;


        public Camera cam;
        public bool ifColisionTerrain;
        public BoundingBox collider;
        public float colliderSize;
        public Vector3 LastMove;
        public Vector3 position;
        public float speedFactor;

        float animationOffset = 0;
        Random rand = new Random();
        Wolf wolf;
        public Animal(Model model, String ModelName, ContentManager contentManager, Matrix world, float colliderSize, Camera cam, int strength, int resistance, int speed, string name) : base(world, model)
        {
            this.cam = cam;
            ifColisionTerrain = false;
            position = world.Translation;
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            this.colliderSize = colliderSize;
            speedFactor = 100;
            animationOffset = (float)rand.NextDouble() * 10;
        }
        public Animal(Wolf wolf, Model model, Matrix world, float colliderSize, Camera cam, int strength, int resistance, int speed, string name) : base(world, model)
        {
            this.wolf = wolf;
            this.cam = cam;
            ifColisionTerrain = false;
            position = world.Translation;
            position = position + new Vector3(-20, -1.6f, -20);
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            this.colliderSize = colliderSize;
            speedFactor = 100;
        }

        public void Draw(Camera camera, string technique)
        {
            //   base.DrawModel(model, camera.View, camera.Projection)
            base.Draw(camera, technique);
        }

        float time = 0;
        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalSeconds;
            float animationFactor = 0f;
            float animationFactor2 = (float)Math.Sin(time + animationOffset) * 5;


            angle += (float) (Math.Sin(time)*(0.02f + rand.NextDouble()*0.01f));
            Vector3 div = position - wolf.position;
            float d = (float)Math.Sqrt(div.X * div.X + div.Z * div.Z);
            dirX = (float)Math.Sin(angle) + 100 / (1 + d) * div.X / d; 
            dirZ = (float)Math.Cos(angle) + 100 / (1 + d) * div.Z / d;

            Boolean shouldAnimate = true;
            if (!ifColisionTerrain)
            {
                position += new Vector3((float)(1+Math.Sin(time))*dirX/35f, 0, (float)(1 + Math.Sin(time))*dirZ /35f);

            }

            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90)) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);// * Matrix.CreateFromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(-90)); ;





            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
            new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));

            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);

            }


            base.Update(gameTime);
        }

        public void ProccedCollisionTree()
        {


            position -= new Vector3(dirX / speedFactor, 0, -dirZ / speedFactor);
            angle -= 0.02f;




            ifColisionTerrain = false;
        }
        public void ProccedCollisionBuilding()
        {
            position -= new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
            ifColisionTerrain = false;

        }
    }
}
