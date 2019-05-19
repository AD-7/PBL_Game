using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameSystem;
using Wataha.GameSystem.Animation;
using WatahaSkinnedModel;

namespace Wataha.GameObjects.Movable
{
    public class Wolf : GameObject
    {
        public Camera cam;
        public bool ifColisionTerrain;
        public BoundingBox collider;
        public float colliderSize;
        public Vector3 LastMove;
        public Vector3 position;
        public float angle;
        public float dirX, dirZ, speedFactor;


        public string Name;
        public int strength;
        public int resistance;
        public int speed;
        public int energy = 100;

        float energyRecoverTime = 5.0f;
        public AnimationPlayer animationPlayer;
        public AnimationSystem animationSystem;
        float animationOffset = 0;
        Random rand;

        public Wolf(Model model, String ModelName, ContentManager contentManager, Matrix world, float colliderSize, Camera cam, int strength, int resistance, int speed, string name) : base(world, model)
        {
            rand = new Random(strength);
            this.Name = name;
            this.strength = strength;
            this.resistance = resistance;
            this.speed = speed;
            Animation animation = new Animation(contentManager, ModelName);
            animationSystem = new AnimationSystem(animation, this);
            this.cam = cam;
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
        public Wolf(Model model, Matrix world, float colliderSize, Camera cam, int strength, int resistance, int speed, string name) : base(world, model)
        {
            rand = new Random();
            this.Name = name;
            this.strength = strength;
            this.resistance = resistance;
            this.speed = speed;


            this.cam = cam;
            ifColisionTerrain = false;
            position = world.Translation;
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
            time += (float)gameTime.ElapsedGameTime.TotalSeconds + (float)rand.NextDouble() * 0.01f;
            if (energy < 100)
            {
                energyRecoverTime -= (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000;               //energy recover
                if (energyRecoverTime <= 0)
                {
                    energyRecoverTime = 5;
                    energy += 1;
                }
            }
            else
            {
                energy = 100;
                energyRecoverTime = 5;
            }




            float animationFactor = 0f;
            float animationFactor2 = (float)(0.5f + Math.Sin(time + animationOffset)) * 5;

            dirX = (float)Math.Sin(angle);
            dirZ = (float)Math.Cos(angle);

            Boolean shouldAnimate = true;
            if (!ifColisionTerrain)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        speedFactor = 3f;
                        animationFactor = 20f + animationFactor2;
                        position += new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
                    }
                    else
                    {
                        speedFactor *= 1 + (float)gameTime.ElapsedGameTime.TotalSeconds * 6;
                        if (speedFactor < 50)
                            position += new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
                        shouldAnimate = false;
                    }

                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        angle += 0.05f;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        angle -= 0.05f;
                    }
                }
                else
                {

                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        speedFactor = 4;
                        animationFactor = 10f + animationFactor2;
                        position += new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
                    }
                    else
                    {
                        shouldAnimate = false;
                        speedFactor *= 1f + (float)gameTime.ElapsedGameTime.TotalSeconds * 6;
                        if (speedFactor < 50)
                            position += new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.A))
                    {
                        angle += 0.05f;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        angle -= 0.05f;
                    }
                }

            }
            if (animationSystem != null && !shouldAnimate)
            {
                animationSystem.animation.frameSpeed = 0.02f * (1f + animationSystem.animation.CurrentFrame * 2f / animationSystem.animation.NumberOfFrames);
                if (animationSystem.animation.CurrentFrame == 0) animationSystem.Stop();
                animationSystem.Update(gameTime);
            }

            if (animationSystem != null && shouldAnimate)
            {
                animationSystem.animation.frameSpeed = 0.2f / animationFactor;
                animationSystem.Update(gameTime);
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
