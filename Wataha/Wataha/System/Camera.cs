using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem
{
    public class Camera
    {
        public Vector3 CamPos;
        public Vector3 CamTarget;
        public Matrix Projection;
        public Matrix View;
        public float maxDist;

        public Camera()
        {
             CamPos = new Vector3(2, 10, 30);
             CamTarget = new Vector3(0, 0, -10);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), 800 / 480f, 0.1f, 220f);
            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.Up);
            maxDist = Math.Abs(CamPos.X - CamTarget.X);

        }

        public void Update()
        {
            
            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.Up);
        }

        public void CameraUpdate(Matrix playerMatrix)
        {
            CamPos = playerMatrix.Translation + (playerMatrix.Up * 20) +
                                    (playerMatrix.Backward * 5);
            CamTarget = playerMatrix.Translation;

            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.Up); 
        }

        public void CamMoveLeft(float value)
        {
         
                    CamPos.X += value/2;
                    CamTarget.X -= value;
             
        }
        public void CamMoveRight(float value)
        {
          
                    CamPos.X -= value;
                    CamTarget.X += value;
              
            
        }
        public void CamMoveForward(float value)
        {
           
              CamPos.Z -= value;
            CamTarget.Z -= value;
     
            
        }
        public void CamMoveBack(float value)
        {
      
            CamPos.Z += value;
            CamTarget.Z += value;
           
            
        }


    }
}
