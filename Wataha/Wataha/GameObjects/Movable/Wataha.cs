using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Movable
{
    public  class Wataha 
    {

        public Camera cam;
        public List<Wolf> wolves;


        public Wataha(Camera cam) 
        {
            wolves = new List<Wolf>();
            this.cam = cam;
        }

        public void Update()
        {
            foreach(Wolf wolf in wolves)
            {
                
                wolf.Update();


            }

            Matrix pom = Matrix.CreateRotationX(MathHelper.ToRadians(-90));

            float avgAngle =0;
            Vector3 avgPosition=Vector3.Zero;

            foreach (Wolf wolf in wolves)
            {
                avgAngle += wolf.angle;
                avgPosition += wolf.position;
            }
            avgAngle /= 2;
            avgPosition /= 2;

            pom *= Matrix.CreateRotationY(avgAngle) * Matrix.CreateTranslation(avgPosition);

            cam.CameraUpdate(pom);
        }

        public void Draw(EffectTechnique technique)
        {
          
        }


    }
}
