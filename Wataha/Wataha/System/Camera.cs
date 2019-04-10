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
     //   public Matrix StaticObjectsView;
        public Camera()
        {
             CamPos = new Vector3(0, 10, 35);
             CamTarget = new Vector3(0, -3, -5);
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(30), 800 / 480f, 0.1f, 100f);
            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.UnitY);
           // StaticObjectsView = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.UnitY);
        }

        public void Update()
        {
            View = Matrix.CreateLookAt(CamPos, CamTarget, Vector3.UnitY);
        }

        public void CamMoveLeft(float value)
        {
            CamPos.X -= value;
            CamTarget.X -= value;
        }
        public void CamMoveRight(float value)
        {
            CamPos.X += value;
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
