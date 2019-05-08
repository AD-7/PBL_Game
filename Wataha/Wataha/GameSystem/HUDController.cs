using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem
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

        int screenWidth, screenWidthOld;
        int screenHeight, screenHeightOld;
        int stringOffsetWidth, stringOffsetHeight;
      

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

            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            screenWidthOld = 0;
            screenHeightOld = 0;

        }

        public void Update()
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recResources.X = 0;
                recResources.Y =  0;
                recResources.Height = screenHeight / 10;
                recResources.Width = screenWidth / 2;

                stringOffsetWidth = (recResources.Width / 100);
                stringOffsetHeight = (recResources.Height / 100); 

                recMeal.X = recResources.Width/60;
                recMeal.Y = (recResources.Height / 5) * 2;
                recMeal.Height =recResources.Height /4 ;
                recMeal.Width = recResources.Width / 25;

                recWhiteFang.X =  recResources.Width / 4 ;
                recWhiteFang.Y = recMeal.Y ;
                recWhiteFang.Height = recMeal.Width;
                recWhiteFang.Width = recMeal.Height;

                recGoldFang.X = (recResources.Width / 100) * 58 ;
                recGoldFang.Y = recMeal.Y ;
                recGoldFang.Height = recMeal.Width;
                recGoldFang.Width = recMeal.Height;

                recPausePanel.X = screenWidth / 3;
                recPausePanel.Y = (screenHeight/100) * 5 ;
                recPausePanel.Height =(screenHeight /10) * 9 ;
                recPausePanel.Width = screenWidth / 3;


                recResumeButton.X = recPausePanel.X +  recPausePanel.Width /4;
                recResumeButton.Y = recPausePanel.Y + recPausePanel.Height / 4;
                recResumeButton.Width = recPausePanel.Width / 2;
                recResumeButton.Height = recPausePanel.Height / 10;

                recBackToMainMenuButton.X = recResumeButton.X;
                recBackToMainMenuButton.Y = recResumeButton.Y + recPausePanel.Height / 6;
                recBackToMainMenuButton.Width = recPausePanel.Width / 2;
                recBackToMainMenuButton.Height = recPausePanel.Height / 10;

                recExitButton.X = recResumeButton.X;
                recExitButton.Y = recBackToMainMenuButton.Y + recPausePanel.Height / 4;
                recExitButton.Width = recPausePanel.Width / 2;
                recExitButton.Height = recPausePanel.Height / 10;
            }

            UpdateCursorPosition();

            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;

            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            

        }

        public void  Draw()
        {
            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            
            spriteBatch.Draw(pictures[0],recResources,Color.White);   // rectangle to display resources
           

            spriteBatch.Draw(pictures[1], recMeal, Color.White);   // meat picture
            spriteBatch.DrawString(font30, meat.ToString(), new Vector2(recMeal.X + stringOffsetWidth * 13 , stringOffsetHeight * 33), Color.Red);

            spriteBatch.Draw(pictures[3], recWhiteFang,Color.White);     //whitefangs picture
            spriteBatch.DrawString(font30, white_fangs.ToString(), new Vector2(recWhiteFang.X + stringOffsetWidth *19 , stringOffsetHeight * 33), Color.White);

            spriteBatch.Draw(pictures[2], recGoldFang, Color.White);     //goldfangs picture
            spriteBatch.DrawString(font30, gold_fangs.ToString(), new Vector2(recGoldFang.X + stringOffsetWidth *18 ,stringOffsetHeight * 33), Color.Gold);

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
