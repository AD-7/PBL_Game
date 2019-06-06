﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameSystem;
using Wataha.GameSystem.Animation;

namespace Wataha.GameObjects.Movable
{
    public class Animal : GameObject
    {
        public int meat;
        float dirX, dirZ, angle;

        public bool active = true;
        public bool ifColisionTerrain;
        bool ifInTrouble = false;
        public float colliderSize;
        public Vector3 position;
        public float speedFactor;
        public AnimationSystem animationSystem;

        float animationOffset = 0;
        Random rand = new Random();
        Wolf wolf;
        //public Animal(Model model, String ModelName, ContentManager contentManager, Matrix world, float colliderSize, Camera cam) : base(world, model)
        //{
        //    this.cam = cam;
        //    ifColisionTerrain = false;
        //    position = world.Translation;
        //    angle = 180;
        //    collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
        //                                new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
        //    this.colliderSize = colliderSize;
        //    speedFactor = 100;
        //    animationOffset = (float)rand.NextDouble() * 10;
        //}

        //ten kontruktor jest ok
        public Animal(Wolf wolf, Model model, Matrix world, float colliderSize, int meat) : base(world, model)
        {
            this.wolf = wolf;
            this.meat = meat;
            ifColisionTerrain = false;
            position = world.Translation;
            position = position + new Vector3(-8, -2.0f, -22);
            angle = 108;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));

            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);

            }
            this.colliderSize = colliderSize;
            speedFactor = 6;
        }
        public Animal(Wolf  wolf,Model model, String ModelName, ContentManager contentManager, Matrix world, float colliderSize, int meat) : base(world, model)
        {
            this.wolf = wolf;
            this.meat = meat;
            Animation animation = new Animation(contentManager, ModelName);
            animationSystem = new AnimationSystem(animation, this);
            ifColisionTerrain = false;
            position = world.Translation;
            position = position + new Vector3(0, -1f, 0);
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            this.colliderSize = colliderSize;
            speedFactor = 100;
            animationOffset = (float)rand.NextDouble() * 10;

        }

        public void Draw(Camera camera, string technique)
        {
            //   base.DrawModel(model, camera.View, camera.Projection)
            base.Draw(camera, technique);
        }

        float time = 0;
        public override void Update(GameTime gameTime)
        {
            time += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
            //float animationFactor = 0f;
            //float animationFactor2 = (float)Math.Sin(time + animationOffset) * 5;

            if (time >= 3)
            {
                angle += rand.Next(10, 90);
                time = 0;
            }

            dirX = (float)Math.Sin(angle);
            dirZ = (float)Math.Cos(angle);

            if (!ifColisionTerrain)
            {
                position += new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
            }

            CheckSecurity();
            animationSystem.Update(gameTime);



            world = Matrix.CreateRotationX(MathHelper.ToRadians(-90)) * Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);// * Matrix.CreateFromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(-90)); ;

            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
            new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);

            }
            sphere = BoundingSphere.CreateFromBoundingBox(collider);


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
            angle -= 90;
            ifColisionTerrain = false;

        }

        float oldDistance;
        public void CheckSecurity()
        {
            float distance = Vector3.Distance(this.position, wolf.position);

            if (distance < 15 && oldDistance >= 15)
            {
                if (!ifColisionTerrain)
                    angle = -wolf.angle;
                else
                    angle =- wolf.angle / 2;

                speedFactor = 4;
            }
            else if (distance < 15 && oldDistance < 15)
            {
                speedFactor = 5;
            }
            else 
            {
                speedFactor = 12;
            }


            oldDistance = distance;
        }


    }
}
