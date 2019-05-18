using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem.Interfejs
{
    public class HUDHunting
    {

        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager Content;
        public Rectangle Cursor;
        public int screenWidth, screenHeight, screenWidthOld, screenHeightOld;

        Texture2D infoHuntingWindow;
        Texture2D yesButton;


        Rectangle recInfoHuntingWindow;
        Rectangle recYesButton;


        Color yesButtonColor = Color.Gray;

        public bool ifInfoHuntingWindow = true;

        public HUDHunting(SpriteBatch spriteBatch, GraphicsDevice device, ContentManager content, Rectangle cursor)
        {
            this.spriteBatch = spriteBatch;
            this.device = device;
            Content = content;
            Cursor = cursor;
            screenHeight = device.Viewport.Height;
            screenWidth = device.Viewport.Width;
            screenWidthOld = 0;
            screenHeightOld = 0;

            infoHuntingWindow = Content.Load<Texture2D>("Pictures/Hunting/infoHuntingWindow");
            yesButton = Content.Load<Texture2D>("Pictures/Hunting/yesButton");


        }

        public void Update()
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recInfoHuntingWindow.X = screenWidth / 4;
                recInfoHuntingWindow.Y = screenHeight / 4;
                recInfoHuntingWindow.Width = screenWidth / 2;
                recInfoHuntingWindow.Height = screenHeight / 2;

                recYesButton.X = recInfoHuntingWindow.X + (recInfoHuntingWindow.Width / 100) * 45;
                recYesButton.Y = recInfoHuntingWindow.Y + recInfoHuntingWindow.Height - recInfoHuntingWindow.Height / 8;
                recYesButton.Width = recInfoHuntingWindow.Width / 10;
                recYesButton.Height = recInfoHuntingWindow.Height / 12;
            }
            UpdateCursorPosition();
            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;
            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            yesButtonEvent();

        }

        public void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (ifInfoHuntingWindow)
            {
                spriteBatch.Draw(infoHuntingWindow, recInfoHuntingWindow, Color.White);
                spriteBatch.Draw(yesButton, recYesButton, yesButtonColor);
            }


            spriteBatch.End();
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }


        public bool yesButtonEvent()
        {
            if (recYesButton.Intersects(Cursor))
            {
                yesButtonColor = Color.White;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    ifInfoHuntingWindow = false;
                    return true;

                }
                return false;
            }
            yesButtonColor = Color.Gray;
            return false;
        }





        private void UpdateCursorPosition()
        {
            InputSystem.mouseStateOld = InputSystem.mouseState;

            /* Update Cursor position by Mouse */
            InputSystem.mouseState = Mouse.GetState();


            Cursor.X = InputSystem.mouseState.X; Cursor.Y = InputSystem.mouseState.Y;
        }

    }
}
