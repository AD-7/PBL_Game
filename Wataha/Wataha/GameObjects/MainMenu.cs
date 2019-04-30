using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Wataha.GameObjects
{
    public class MainMenu
    {
        private SpriteBatch spriteBatch;
        private Texture2D PlayButtonTexture;
        private Texture2D CloseButtonTexture;
        private Texture2D BGTexture;

        public float ScreenWidth;
        public float ScreenHeight;


        Rectangle recPlayButton;
        Rectangle recCloseButton;
        Rectangle BG;

        Color PlayButtonColor = Color.White;
        Color CloseButtonColor = Color.White;
        Color BGColor = Color.White;

        public MainMenu(SpriteBatch spriteBatch, ContentManager content)
        {
            this.spriteBatch = spriteBatch;

            PlayButtonTexture = content.Load<Texture2D>("MainMenu/start");
            CloseButtonTexture = content.Load<Texture2D>("MainMenu/close");
            BGTexture = content.Load<Texture2D>("MainMenu/bg");

        }

        public void Update()
        {
            BG.X = 0;
            BG.Y = 0;

            BG.Width = (int)ScreenWidth;
            BG.Height = (int)ScreenHeight;

            recPlayButton.X = (int)ScreenWidth / 2 - recPlayButton.Size.X / 2;
            recPlayButton.Y = (int)ScreenHeight / 4 - recPlayButton.Size.Y / 2;

            recPlayButton.Height = (int)ScreenHeight / 6;
            recPlayButton.Width = (int)ScreenWidth / 2;

            recCloseButton.X = (int)ScreenWidth / 2 - recCloseButton.Size.X / 2;
            recCloseButton.Y = (int)ScreenHeight / 4 + (int)ScreenHeight / 3 +  recCloseButton.Size.Y / 2;

            recCloseButton.Height = (int)ScreenHeight / 6;
            recCloseButton.Width = (int)ScreenWidth / 2;

            UpdateCursorPosition();

        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BGTexture, BG, Color.White);
            spriteBatch.Draw(PlayButtonTexture, recPlayButton, PlayButtonColor);
            spriteBatch.Draw(CloseButtonTexture, recCloseButton, CloseButtonColor);
            spriteBatch.End();
        }

        MouseState mouseState;
        Rectangle Cursor;
        private void UpdateCursorPosition()
        {
            /* Update Cursor position by Mouse */
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X; Cursor.Y = mouseState.Y;
        }

        public bool PlayButtonsEvents()
        {
            if ((recPlayButton.Intersects(Cursor)))
            {
                PlayButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                   return true;
                return false;
            }
            else
                PlayButtonColor = Color.White;
           return false;
        }

        public bool ExitButtonsEvents()
        {
            if ((recCloseButton.Intersects(Cursor)))
            {
                CloseButtonColor = Color.Green;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    return true;
                return false;
            }
            else
                CloseButtonColor = Color.White;
            return false;
        }


    }
}
