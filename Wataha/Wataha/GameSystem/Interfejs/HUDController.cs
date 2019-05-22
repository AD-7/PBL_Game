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
    public class HUDController
    {
        public HuntingSystem huntingSystem;

        GameObjects.Movable.Wataha wataha;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager Content;
        Rectangle Cursor;
        WolfPanel wolfPanel;
        public ActualQuestPanel actualQuestPanel;
        public QuestPanel QuestPanel;

        public int meat;
        public int white_fangs;
        public int gold_fangs;

        public SpriteFont font30, broadwayFont;
        public List<Texture2D> pictures;
        Rectangle recResumeButton;
        Rectangle recBackToMainMenuButton;
        Rectangle recExitButton;
        Rectangle recPausePanel, recButtonPanel;
        Rectangle recResources, recMeal, recWhiteFang, recGoldFang;
        Rectangle recButtonWolf1Set, recButtonWolf2Set, recButtonWolf3Set;
        Rectangle recActualQuestButton;

        Color resumeButtonColor = Color.White;
        Color backToMainMenuButtonColor = Color.White;
        Color exitButtonColor = Color.White;
        Color Wolf1ButtonSetColor = Color.White, Wolf3ButtonSetColor = Color.White, Wolf2ButtonSetColor = Color.White;
        Color actualQuestButtonColor = Color.Gray;
        public bool ifPaused = false;
        public bool ifWolfPanel = false;
        public bool ifActualQuestPanel = false;
        public bool ifQuestPanel = false;
       

        int screenWidth, screenWidthOld;
        int screenHeight, screenHeightOld;
        int stringOffsetWidth, stringOffsetHeight;

        string actualNameOfWolfPanel = "";

        public HUDController(SpriteBatch batch, GraphicsDevice device, ContentManager manager, int meat, int white_fangs, int gold_fangs, Wataha.GameObjects.Movable.Wataha wataha, HuntingSystem hs)
        {

            this.huntingSystem = hs;
            this.wataha = wataha;
            this.device = device;
            this.spriteBatch = batch;
            this.Content = manager;
            this.meat = meat;
            this.white_fangs = white_fangs;
            this.gold_fangs = gold_fangs;
            pictures = new List<Texture2D>();


            font30 = Content.Load<SpriteFont>("Fonts/font1");
            broadwayFont = Content.Load<SpriteFont>("Fonts/Broadway");

            pictures.Add(Content.Load<Texture2D>("Pictures/panel"));
            pictures.Add(Content.Load<Texture2D>("Pictures/meat"));
            pictures.Add(Content.Load<Texture2D>("Pictures/goldFangs"));
            pictures.Add(Content.Load<Texture2D>("Pictures/whiteFang"));
            pictures.Add(Content.Load<Texture2D>("Pictures/pauseScreen"));
            pictures.Add(Content.Load<Texture2D>("Pictures/resumeButton")); // 5
            pictures.Add(Content.Load<Texture2D>("Pictures/backToMainMenu"));
            pictures.Add(Content.Load<Texture2D>("Pictures/exitButton"));
            pictures.Add(Content.Load<Texture2D>("Pictures/rectangleForButtons"));
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto"));
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto2"));  //10
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto3"));
            pictures.Add(Content.Load<Texture2D>("Pictures/actualQuestButton"));

            wolfPanel = new WolfPanel(Content.Load<Texture2D>("Pictures/rectangleForWolfPanel"), broadwayFont);
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/exitPicture"));
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/GoHuntingButton"));


            wolfPanel.font21 = Content.Load<SpriteFont>("Fonts/broadway21");
            wolfPanel.font18 = Content.Load<SpriteFont>("Fonts/broadway18");
            wolfPanel.font14 = Content.Load<SpriteFont>("Fonts/broadway14");

            actualQuestPanel = new ActualQuestPanel(Content.Load<Texture2D>("Pictures/actualQuestPanel"), wolfPanel.font14);
            QuestPanel = new QuestPanel(Content, wolfPanel.font14);


            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            screenWidthOld = 0;
            screenHeightOld = 0;
            stringOffsetWidth = 0;
            stringOffsetHeight = 0;

            huntingSystem.hudHunting = new HUDHunting(spriteBatch, device, Content, Cursor);

        }

        public void Update()
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recResources.X = 0;
                recResources.Y = 0;
                recResources.Height = screenHeight / 10;
                recResources.Width = screenWidth / 2;

                stringOffsetWidth = (recResources.Width / 100);
                stringOffsetHeight = (recResources.Height / 100);

                recMeal.X = recResources.Width / 80;
                recMeal.Y = (recResources.Height / 5) * 2;
                recMeal.Height = recResources.Height / 4;
                recMeal.Width = recResources.Width / 25;

                recWhiteFang.X = recResources.Width / 4;
                recWhiteFang.Y = recMeal.Y;
                recWhiteFang.Height = recMeal.Width;
                recWhiteFang.Width = recMeal.Height;

                recGoldFang.X = (recResources.Width / 100) * 58;
                recGoldFang.Y = recMeal.Y;
                recGoldFang.Height = recMeal.Width;
                recGoldFang.Width = recMeal.Height;

                recPausePanel.X = screenWidth / 3;
                recPausePanel.Y = (screenHeight / 100) * 5;
                recPausePanel.Height = (screenHeight / 10) * 9;
                recPausePanel.Width = screenWidth / 3;


                recResumeButton.X = recPausePanel.X + recPausePanel.Width / 4;
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

                recButtonPanel.X = (screenWidth / 2) - (screenWidth / 8);
                recButtonPanel.Y = (screenHeight / 100) * 92;
                recButtonPanel.Height = (screenHeight / 100) * 12;
                recButtonPanel.Width = (screenWidth / 4);

                recButtonWolf1Set.X = (recButtonPanel.X) + (recButtonPanel.X / 100) * 5;
                recButtonWolf1Set.Y = (recButtonPanel.Y) + (recButtonPanel.Y / 100) * 3;
                recButtonWolf1Set.Height = recButtonPanel.Height / 2;
                recButtonWolf1Set.Width = recButtonPanel.Height / 2;

                recButtonWolf2Set.X = recButtonPanel.X + recButtonPanel.Width / 2 - recButtonPanel.Width / 12;
                recButtonWolf2Set.Y = recButtonWolf1Set.Y;
                recButtonWolf2Set.Height = recButtonWolf1Set.Height;
                recButtonWolf2Set.Width = recButtonWolf1Set.Width;

                recButtonWolf3Set.X = recButtonPanel.X + recButtonPanel.Width - recButtonWolf1Set.Width - (recButtonPanel.X / 100) * 5;
                recButtonWolf3Set.Y = recButtonWolf1Set.Y;
                recButtonWolf3Set.Height = recButtonWolf1Set.Height;
                recButtonWolf3Set.Width = recButtonWolf1Set.Width;

                recActualQuestButton.X = screenWidth / 100;
                recActualQuestButton.Y = screenHeight - screenHeight / 6;
                recActualQuestButton.Width = screenHeight / 8;
                recActualQuestButton.Height = recActualQuestButton.Width;

                wolfPanel.Update(screenWidth, screenHeight);
                actualQuestPanel.Update(screenWidth, screenHeight);
            }

            UpdateCursorPosition();

            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;


            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;


            if (!ifPaused)
            {
                Wolf1ButtonEvent(); Wolf2ButtonEvent(); Wolf3ButtonEvent();

                ActualQuestButtonEvent();


                if (ifWolfPanel)
                {

                    if (wolfPanel.goHuntingButtonEvent(Cursor))
                    {
                        if (wataha.wolves.Where(w => w.Name == actualNameOfWolfPanel).ToList()[0].energy >= 50)
                        {
                            
                            ifWolfPanel = false;
                            wolfPanel.ifEnoughEnergy = true;
                            huntingSystem.InitializeHunting(wataha.wolves.Where(w => w.Name == actualNameOfWolfPanel).ToList()[0]);
                            huntingSystem.active = true;
                        }
                        else
                        {
                            wolfPanel.ifEnoughEnergy = false;

                        }

                    }

                    if (wolfPanel.exitButtonEvent(Cursor))
                    {
                        ifWolfPanel = false;
                    }

                }
                else
                {
                    actualNameOfWolfPanel = "";
                }
            }

        }

        public void Draw()
        {

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(pictures[0], recResources, Color.White);   // rectangle to display resources


            spriteBatch.Draw(pictures[1], recMeal, Color.White);   // meat picture
            spriteBatch.DrawString(font30, meat.ToString(), new Vector2(recMeal.X + stringOffsetWidth * 13, stringOffsetHeight * 33), Color.Red);

            spriteBatch.Draw(pictures[3], recWhiteFang, Color.White);     //whitefangs picture
            spriteBatch.DrawString(font30, white_fangs.ToString(), new Vector2(recWhiteFang.X + stringOffsetWidth * 19, stringOffsetHeight * 33), Color.White);

            spriteBatch.Draw(pictures[2], recGoldFang, Color.White);     //goldfangs picture
            spriteBatch.DrawString(font30, gold_fangs.ToString(), new Vector2(recGoldFang.X + stringOffsetWidth * 18, stringOffsetHeight * 33), Color.Gold);

            spriteBatch.Draw(pictures[8], recButtonPanel, Color.White);  //panel kontrolek wilków

            spriteBatch.Draw(pictures[9], recButtonWolf1Set, Wolf1ButtonSetColor); // 
            spriteBatch.DrawString(broadwayFont, "K i m i k o", new Vector2(recButtonWolf1Set.X, recButtonPanel.Y + recButtonPanel.Y / 200), Color.Blue);
            spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Kimiko")).ToList()[0].energy.ToString(), new Vector2(recButtonWolf1Set.X, recButtonWolf1Set.Y + recButtonWolf1Set.Height + recButtonWolf1Set.Height / 10), Color.Green);

            spriteBatch.Draw(pictures[10], recButtonWolf2Set, Wolf2ButtonSetColor);
            spriteBatch.DrawString(broadwayFont, "Y u a", new Vector2(recButtonWolf2Set.X, recButtonPanel.Y + recButtonPanel.Y / 200), Color.Yellow);
            spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Yua")).ToList()[0].energy.ToString(), new Vector2(recButtonWolf2Set.X, recButtonWolf2Set.Y + recButtonWolf2Set.Height + recButtonWolf2Set.Height / 10), Color.Green);

            spriteBatch.Draw(pictures[11], recButtonWolf3Set, Wolf3ButtonSetColor);
            spriteBatch.DrawString(broadwayFont, "H a t s u", new Vector2(recButtonWolf3Set.X, recButtonPanel.Y + recButtonPanel.Y / 200), Color.Orange);
            spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Hatsu")).ToList()[0].energy.ToString(), new Vector2(recButtonWolf3Set.X, recButtonWolf3Set.Y + recButtonWolf3Set.Height + recButtonWolf3Set.Height / 10), Color.Green);

            spriteBatch.Draw(pictures[12], recActualQuestButton, actualQuestButtonColor);


            if (QuestSystem.currentQuest != null)
            {
                spriteBatch.DrawString(broadwayFont, "Press F to interact", new Vector2((screenWidth / 2), (screenHeight / 2)), Color.Orange);
            }


            if (ifWolfPanel)
            {
                wolfPanel.Draw(spriteBatch);
            }

            if (ifActualQuestPanel)
            {
                actualQuestPanel.Draw(spriteBatch);
            }


            if (ifQuestPanel)
            {
                QuestPanel.Draw(spriteBatch);
            }

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





        public bool ActualQuestButtonEvent()
        {
            if (recActualQuestButton.Intersects(Cursor))
            {
                if (!ifActualQuestPanel)
                {
                    actualQuestButtonColor = Color.White;
                    if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                    {
                        recActualQuestButton.X = screenWidth / 120;
                        recActualQuestButton.Y = screenHeight - screenHeight / 10;
                        recActualQuestButton.Height /= 2;
                        recActualQuestButton.Width /= 2;
                        ifActualQuestPanel = true;
                        actualQuestButtonColor = Color.Gray;
                        return true;
                    }
                    return false;
                }
                else
                {
                    actualQuestButtonColor = Color.White;
                    if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                    {
                        recActualQuestButton.X = screenWidth / 100;
                        recActualQuestButton.Y = screenHeight - screenHeight / 6;
                        recActualQuestButton.Height *= 2;
                        recActualQuestButton.Width *= 2;
                        ifActualQuestPanel = false;
                        actualQuestButtonColor = Color.Gray;
                        return true;
                    }

                    return false;
                }

            }


            actualQuestButtonColor = Color.Gray;
            return false;
        }

        public bool Wolf1ButtonEvent()
        {
            if (recButtonWolf1Set.Intersects(Cursor))
            {

                Wolf1ButtonSetColor = Color.MediumBlue;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    actualNameOfWolfPanel = "Kimiko";
                    wolfPanel.SetPanel(wataha.wolves.Where(w => w.Name == actualNameOfWolfPanel).ToList()[0]);
                    ifWolfPanel = true;
                    return true;
                }

                return false;
            }
            else
                Wolf1ButtonSetColor = Color.White;
            return false;
        }

        public bool Wolf2ButtonEvent()
        {
            if (recButtonWolf2Set.Intersects(Cursor))
            {
                Wolf2ButtonSetColor = Color.Yellow;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    actualNameOfWolfPanel = "Yua";
                    wolfPanel.SetPanel(wataha.wolves.Where(w => w.Name == actualNameOfWolfPanel).ToList()[0]);
                    ifWolfPanel = true;
                    return true;
                }
                return false;
            }
            else
                Wolf2ButtonSetColor = Color.White;
            return false;
        }

        public bool Wolf3ButtonEvent()
        {
            if (recButtonWolf3Set.Intersects(Cursor))
            {
                Wolf3ButtonSetColor = Color.MonoGameOrange;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    actualNameOfWolfPanel = "Hatsu";
                    wolfPanel.SetPanel(wataha.wolves.Where(w => w.Name == actualNameOfWolfPanel).ToList()[0]);
                    ifWolfPanel = true;
                    return true;
                }
                return false;
            }
            else
                Wolf3ButtonSetColor = Color.White;
            return false;
        }

        public bool ResumeButtonEvent()
        {
            if ((recResumeButton.Intersects(Cursor)))
            {
                resumeButtonColor = Color.Yellow;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
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
                backToMainMenuButtonColor = Color.Yellow;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
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
                exitButtonColor = Color.Yellow;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                    return true;
                return false;
            }
            else
                exitButtonColor = Color.White;
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
