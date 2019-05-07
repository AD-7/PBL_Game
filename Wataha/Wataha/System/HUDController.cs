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
        Rectangle recPausePanel;
        Rectangle recResources, recMeal, recWhiteFang, recGoldFang;
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


            recResources.X = device.Viewport.X + 5;
            recResources.Y = device.Viewport.Y + 7;
            recResources.Height = device.Viewport.Height / 8;
            recResources.Width = device.Viewport.Width / 2;

            recMeal.X = recResources.X  +5 ;
            recMeal.Y = recResources.Y  +27 ;
            recMeal.Height = pictures[1].Height;
            recMeal.Width = pictures[1].Width;

            recWhiteFang.X = recResources.X + 250;
            recWhiteFang.Y = recResources.Y + 20;
            recWhiteFang.Height = pictures[3].Height;
            recWhiteFang.Width = pictures[3].Width;

            recGoldFang.X = recResources.X + 530;
            recGoldFang.Y = recResources.Y + 20;
            recGoldFang.Height = pictures[2].Height;
            recGoldFang.Width = pictures[2].Width;

            recPausePanel.X = device.Viewport.Width / 3;
            recPausePanel.Y = 25;
            recPausePanel.Height =  device.Viewport.Height - 80;
            recPausePanel.Width =  device.Viewport.Width / 3;


            recResumeButton.X = recPausePanel.X + recPausePanel.Width /4;
            recResumeButton.Y = recPausePanel.Height/4;
            recResumeButton.Width = pictures[5].Width;
            recResumeButton.Height = pictures[5].Height;

             recBackToMainMenuButton.X = recPausePanel.X + recPausePanel.Width / 4 - recResumeButton.Width/8;
            recBackToMainMenuButton.Y = recResumeButton.Y + 120;
            recBackToMainMenuButton.Width = pictures[6].Width;
            recBackToMainMenuButton.Height = pictures[6].Height;

            recExitButton.X = recPausePanel.X + recPausePanel.Width / 4 - recResumeButton.Width / 7;
            recExitButton.Y = recPausePanel.Height - 100;
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
            
            spriteBatch.Draw(pictures[0],recResources,Color.White);   // rectangle to display resources
           

            spriteBatch.Draw(pictures[1], recMeal, Color.White);   // meat picture
            spriteBatch.DrawString(font30, meat.ToString(), new Vector2(recMeal.X+120, recMeal.Y - 2), Color.Red);

            spriteBatch.Draw(pictures[3], recWhiteFang,Color.White);     //whitefangs picture
            spriteBatch.DrawString(font30, white_fangs.ToString(), new Vector2(recWhiteFang.X +150, recWhiteFang.Y +6), Color.White);

            spriteBatch.Draw(pictures[2], recGoldFang, Color.White);     //goldfangs picture
            spriteBatch.DrawString(font30, gold_fangs.ToString(), new Vector2(recGoldFang.X + 143, recGoldFang.Y +6 ), Color.Gold);

            if (ifPaused)
            {
                 spriteBatch.Draw(pictures[4], recPausePanel, Color.White);
          
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
