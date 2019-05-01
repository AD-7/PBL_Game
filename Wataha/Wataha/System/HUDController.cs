using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
    public class HUDController
    {
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager Content;
        public float ScreenWidth;
        public float ScreenHeight;
        MouseState mouseState;
        Rectangle Cursor;


        public int meat;
        public int white_fangs;
        public int gold_fangs;

        public SpriteFont font30;
        public List<Texture2D> pictures;
        Rectangle recResumeButton;
        Rectangle recBackToMainMenuButton;
        Rectangle recExitButton;
        Color resumeButtonColor = Color.White;
        Color backToMainMenuButtonColor = Color.White;
        Color exitButtonColor = Color.White;
        public bool ifPaused = false;
      

        public HUDController(SpriteBatch batch, GraphicsDevice device, ContentManager manager, int meat, int white_fangs, int gold_fangs)
        {
            this.device = device;
            this.spriteBatch = batch;
            this.Content = manager;
            this.meat = meat;
            this.white_fangs = white_fangs;
            this.gold_fangs = gold_fangs;
            pictures = new List<Texture2D>();

            font30 = Content.Load<SpriteFont>("Fonts/font1");
            pictures.Add(Content.Load<Texture2D>("Pictures/panel"));
            pictures.Add(Content.Load<Texture2D>("Pictures/meat"));
            pictures.Add(Content.Load<Texture2D>("Pictures/goldFangs"));
            pictures.Add(Content.Load<Texture2D>("Pictures/whiteFang"));
            pictures.Add(Content.Load<Texture2D>("Pictures/pauseScreen"));
            pictures.Add(Content.Load<Texture2D>("Pictures/resumeButton"));
            pictures.Add(Content.Load<Texture2D>("Pictures/backToMainMenu"));
            pictures.Add(Content.Load<Texture2D>("Pictures/exitButton"));


            recResumeButton.X = 770;
            recResumeButton.Y = 280;
            recResumeButton.Width = pictures[5].Width;
            recResumeButton.Height = pictures[5].Height;

             recBackToMainMenuButton.X = 730;
            recBackToMainMenuButton.Y = 380;
            recBackToMainMenuButton.Width = pictures[6].Width;
            recBackToMainMenuButton.Height = pictures[6].Height;

            recExitButton.X = 730;
            recExitButton.Y = 780;
            recExitButton.Width = pictures[7].Width;
            recExitButton.Height = pictures[7].Height;
        }

        public void Update()
        {

            UpdateCursorPosition();


        }

        public void  Draw()
        {
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            spriteBatch.Draw(pictures[0], new Vector2(10,5));   // rectangle to display resources


            spriteBatch.Draw(pictures[1], new Vector2(15,35));   // meat picture
            spriteBatch.DrawString(font30, meat.ToString(), new Vector2(130, 33), Color.Red);

            spriteBatch.Draw(pictures[3], new Vector2(250, 28));     //whitefangs picture
            spriteBatch.DrawString(font30, white_fangs.ToString(), new Vector2(410, 33), Color.White);

            spriteBatch.Draw(pictures[2], new Vector2(530, 28));     //goldfangs picture
            spriteBatch.DrawString(font30, gold_fangs.ToString(), new Vector2(680, 33), Color.Gold);

            if (ifPaused)
            {
                 spriteBatch.Draw(pictures[4], new Vector2(700, 100));
          
                spriteBatch.Draw(pictures[5], recResumeButton, resumeButtonColor);
                spriteBatch.Draw(pictures[6], recBackToMainMenuButton, backToMainMenuButtonColor);
                spriteBatch.Draw(pictures[7], recExitButton, exitButtonColor);
            }
            
            
            spriteBatch.End();

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }


        public bool ResumeButtonEvent()
        {
            if ((recResumeButton.Intersects(Cursor)))
            {
                resumeButtonColor = Color.Red;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    return true;
                return false;
            }
            else
                resumeButtonColor = Color.White;
            return false;
        }

        public bool BackToMainMenuButtonEvent()
        {
            if ((recBackToMainMenuButton.Intersects(Cursor)))
            {
                backToMainMenuButtonColor= Color.Red;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    return true;
                return false;
            }
            else
               backToMainMenuButtonColor = Color.White;
            return false;
        }

        public bool ExitButtonEvent()
        {
            if ((recExitButton.Intersects(Cursor)))
            {
                exitButtonColor = Color.Red;
                if (mouseState.LeftButton == ButtonState.Pressed)
                    return true;
                return false;
            }
            else
               exitButtonColor = Color.White;
            return false;
        }

        private void UpdateCursorPosition()
        {
            /* Update Cursor position by Mouse */
            mouseState = Mouse.GetState();
            Cursor.X = mouseState.X; Cursor.Y = mouseState.Y;
        }
    }
}
