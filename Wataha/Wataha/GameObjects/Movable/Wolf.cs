using Microsoft.Kinect;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SkinnedModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameSystem;

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
       public  float dirX,dirZ, speedFactor;
        
        public int strength = 10;
        public int resistance = 10;
        public int speed = 10;
        public int energy = 100;
        float energyRecoverTime = 5.0f;
        public AnimationPlayer animationPlayer;
        public KinectSensor kinectSensor;
        Skeleton[] skeletonData;


        //public Wolf(String animationName, Game game, Matrix world, float colliderSize, Camera cam) : base(world)
        //{
        //    //base.model = model;

        //    animationPlayer = new AnimationPlayer(game, animationName);
        //    kinectSensor = KinectSensor.KinectSensors[0];
        //    kinectSensor.SkeletonStream.Enable();
        //    kinectSensor.Start();

        //    skeletonData = new Skeleton[kinectSensor.SkeletonStream.FrameSkeletonArrayLength];
        //    kinectSensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(KinectAllFramesReady);

         
        //    this.cam = cam;
        //    ifColisionTerrain = false;
        //    position = world.Translation;
        //    angle = 180;
        //    collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize/2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
        //                                new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
        //    this.colliderSize = colliderSize;
            
        //}
        public Wolf(Model model, Matrix world, float colliderSize, Camera cam) : base(world,model)
        {
          
            this.cam = cam;
            ifColisionTerrain = false;
            position = world.Translation;
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            this.colliderSize = colliderSize;
        }

        public void Draw(Camera camera,string technique)
        {
         //   base.DrawModel(model, camera.View, camera.Projection)
            base.Draw(camera,technique);
        }

        public override void Update()
        {
            dirX = (float)Math.Sin(angle);
            dirZ = (float)Math.Cos(angle);
          

           if (!ifColisionTerrain)
           {
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift))
                {
                    speedFactor = 2.5f;
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
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
                else
                {
                    speedFactor = 4;
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
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
            //else
            //{
               
            //}
            // *
            world =  Matrix.CreateRotationX(MathHelper.ToRadians(-90)) *Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);// * Matrix.CreateFromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(-90)); ;

         //   cam.CameraUpdate(world);

          

            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
            new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));

            foreach (ModelMesh mesh in model.Meshes)
            {
                mesh.BoundingSphere = BoundingSphere.CreateFromBoundingBox(collider);
               
            }


            base.Update();
            }

        public void ProccedCollisionTree()
        {
            //  position = Vector3.Lerp(position, position - new Vector3(dirX / 4, 0, dirZ / 4), 5);
         
                position -= new Vector3(dirX / speedFactor, 0, -dirZ / speedFactor);
            angle -= 0.02f;


          

            ifColisionTerrain = false;
        }
        public void ProccedCollisionBuilding()
        {
            position -= new Vector3(dirX / speedFactor, 0, dirZ / speedFactor);
            ifColisionTerrain = false;

        }

        public void KinectAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                if (skeletonFrame != null)
                {
                    skeletonFrame.CopySkeletonDataTo(skeletonData);
                    if (skeletonData[0] != null && skeletonData[0].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        animationPlayer.Update(skeletonData[0], Matrix.Identity);
                        //animationPlayer.Draw(cameraArc, cameraDistance, cameraRotation);

                    }
                }
            }
        }
    }
    
}
