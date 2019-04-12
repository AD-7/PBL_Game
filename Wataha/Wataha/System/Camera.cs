using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
    public class Camera
    {
        public Vector3 CamPos;
        public Vector3 CamTarget;
        public Matrix Projection;
        public Matrix View;
        public bool blocked = false;
        public float maxDist;
     //   public Matrix StaticObjectsView;
        public Camera()
        {
             CamPos = new Vector3(2, 10, 30);
             CamTarget = new Vector3(0, 0, -5);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), 800 / 480f, 0.1f, 100f);
            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.Up);
            maxDist = Math.Abs(CamPos.X - CamTarget.X);
           // StaticObjectsView = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.UnitY);
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
            if (!blocked)
            {
            
             
                    CamPos.X += value/2;
                    CamTarget.X -= value;
         
                    
                
            
            }
             
        }
        public void CamMoveRight(float value)
        {
            if (!blocked)
            {
               
                    CamPos.X -= value;
                    CamTarget.X += value;
               
                   
                
                    
           
            }
            
        }
        public void CamMoveForward(float value)
        {
            if (!blocked)
            {
              CamPos.Z -= value;
            CamTarget.Z -= value;
            }
            
        }
        public void CamMoveBack(float value)
        {
            if (!blocked)
            {
            CamPos.Z += value;
            CamTarget.Z += value;
            }
            
        }


    }
}
