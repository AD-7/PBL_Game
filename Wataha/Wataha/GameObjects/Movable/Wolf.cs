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

        public Wolf(Model model, Matrix world,float colliderSize,Camera cam)
        {
            base.model = model;
            base.world = world;
            this.cam = cam;
            ifColisionTerrain = false;
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

            if (!ifColisionTerrain)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    Translate(new Vector3(0, 0, -0.1f));
                    LastMove = new Vector3(0, 0, -0.1f);
                    cam.CamMoveForward(0.1f);

                }

                if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    Translate(new Vector3(0, 0, 0.1f));
                    LastMove = new Vector3(0, 0, 0.1f);
                    cam.CamMoveBack(0.1f);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) )
                {
                      Translate(new Vector3(-0.1f, 0, 0));
                 //   RotateY(0.5f);
                    LastMove = new Vector3(-0.1f, 0, 0);
                    cam.CamMoveLeft(0.1f);
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) )
                {
                    Translate(new Vector3(0.1f, 0, 0));
                //  RotateY(-0.5f);
                    LastMove = new Vector3(0.1f, 0, 0);
                    cam.CamMoveRight(0.1f);
                }
            }
            else
            {
                Translate(-LastMove);
                LastMove = new Vector3(0, 0, 0);
                ifColisionTerrain = false;
                cam.blocked = false;
            }
           
         

            cam.Update();
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize / 2, world.Translation.Y - colliderSize / 2, world.Translation.Z - colliderSize / 2),
            new Vector3(world.Translation.X + colliderSize / 2, world.Translation.Y + colliderSize / 2, world.Translation.Z + colliderSize / 2));





        }
    }
}
