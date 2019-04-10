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
     


        public Wolf(Model model, Matrix world,Camera cam)
        {
            base.model = model;
            base.world = world;
            this.cam = cam;
         
        }



        public override void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
            Translate(new Vector3(0,0, -0.1f));
                cam.CamMoveForward(0.1f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Translate(new Vector3(0, 0, 0.1f));
                cam.CamMoveBack(0.1f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
            Translate(new Vector3(-0.1f, 0, 0));
                cam.CamMoveLeft(0.1f);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
               Translate(new Vector3(0.1f, 0, 0));
                cam.CamMoveRight(0.1f);
            }
            cam.Update();


        }
    }
}
