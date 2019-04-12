using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.System;

namespace Wataha.GameObjects.Movable
{
     class Wolf : GameObject
    {
        public Camera cam;
        public bool ifColisionTerrain;
        public BoundingBox collider;
        public float colliderSize;
        public Vector3 LastMove;
        public Vector3 position;
        public float angle;

        public int strength = 10;
        public int resistance = 10;
        public int speed = 10;
        public int energy = 100;
        float energyRecoverTime = 5.0f;


        public Wolf(Model model, Matrix world,float colliderSize,Camera cam)
        {
            base.model = model;
            base.world = world;
            this.cam = cam;
            ifColisionTerrain = false;
            position = world.Translation;
            angle = 180;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize/2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
                                        new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));
            this.colliderSize = colliderSize;
        }

        public void Draw(Camera camera)
        {
         //   base.DrawModel(model, camera.View, camera.Projection)
            base.Draw(camera);
        }

        public override void Update()
        {
            float dirX = (float)Math.Sin(angle);
            float dirZ = (float)Math.Cos(angle);

            if (!ifColisionTerrain)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
             {
                    //Translate(new Vector3(0, 0, -0.1f));
                    //LastMove = new Vector3(0, 0, -0.1f);
                    //  cam.CamMoveForward(0.1f);
                    position += new Vector3(dirX/4, 0,dirZ/4);
               cam.CamPos += new Vector3(dirX /4, 0, dirZ / 4);
                    cam.CamTarget += new Vector3(dirX / 4, 0, dirZ / 4);
                } 
                if (Keyboard.GetState().IsKeyDown(Keys.A) )
                {
                        //  Translate(new Vector3(-0.1f, 0, 0));            
                        //LastMove = new Vector3(-0.1f, 0, 0);
                        //cam.CamMoveLeft(0.2f);
                        angle += 0.05f;
                        
                  //     cam.CamPos -= new Vector3(dirX /2 , 0, dirZ/2);
                  //     cam.CamTarget += new Vector3(dirX/2 , 0, dirZ/2);

                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) )
                {
                    //Translate(new Vector3(0.1f, 0, 0));
                    //LastMove = new Vector3(0.1f, 0, 0);
                   
                    angle -= 0.05f;
                   // cam.CamMoveRight(0.2f);
                    //     cam.CamPos -= new Vector3(dirX / 2, 0, dirZ/2);
                    //   cam.CamTarget += new Vector3(dirX /2, 0, dirZ/2);

                }

            }
            else
            {
                //Translate(-LastMove);
                //LastMove = new Vector3(0, 0, 0);
                ifColisionTerrain = false;
                cam.blocked = false;
            }
            // *
            world =  Matrix.CreateRotationX(MathHelper.ToRadians(-90)) *Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);// * Matrix.CreateFromAxisAngle(Vector3.UnitY, MathHelper.ToRadians(-90)); ;

            cam.CameraUpdate(world);

          
           // cam.Update();
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
            new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));





        }
    }
}
