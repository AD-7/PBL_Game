using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
   public class Camera
    {
            GraphicsDevice graphicsDevice;

            // Let's start at X = 0 so we're looking at things head-on
            Vector3 position = new Vector3(0, 20, 10);

            float angle;

            public Matrix ViewMatrix
            {
                get
                {
                    var lookAtVector = new Vector3(0, -1, -.5f);
                    var rotationMatrix = Matrix.CreateRotationZ(angle);
                    lookAtVector = Vector3.Transform(lookAtVector, rotationMatrix);
                    lookAtVector += position;

                    var upVector = Vector3.UnitZ;

                    return Matrix.CreateLookAt(
                        position, lookAtVector, upVector);
                }
            }

            public Matrix ProjectionMatrix
            {
                get
                {
                    float fieldOfView = Microsoft.Xna.Framework.MathHelper.PiOver4;
                    float nearClipPlane = 1;
                    float farClipPlane = 200;
                    float aspectRatio = graphicsDevice.Viewport.Width / (float)graphicsDevice.Viewport.Height;

                    return Matrix.CreatePerspectiveFieldOfView(
                        fieldOfView, aspectRatio, nearClipPlane, farClipPlane);
                }
            }

            public Camera(GraphicsDevice graphicsDevice)
            {
                this.graphicsDevice = graphicsDevice;
            }

        public void Updated()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                position.X += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                position.X -= 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                position.Y += 10;
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
                position.Y -= 10;
        }

    }
    }
