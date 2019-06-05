﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using Wataha.GameObjects.Interable;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem.Interfejs
{
    public class HUDController
    {
        public HuntingSystem huntingSystem;

        public GameObjects.Movable.Wataha wataha;
        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager Content;
        WolfPanel wolfPanel;
        public static ActualQuestPanel actualQuestPanel;
        public static QuestPanel QuestPanel;
        public MarketPanel marketPanel;
        public SpriteFont font30, broadwayFont;
        private SpriteFont arial18Italic;
        private SpriteFont arial15Italic;
        private SpriteFont arial12Italic;
        public List<Texture2D> pictures;
        Rectangle recResumeButton;
        Rectangle recBackToMainMenuButton;
        Rectangle recExitButton;
        Rectangle recPausePanel, recButtonPanel;
        Rectangle recResources;
        Rectangle recButtonWolf1Set, recButtonWolf2Set, recButtonWolf3Set;
        Rectangle recActualQuestButton;
        Rectangle recSaveButton;
        Rectangle recSaveInfo, recSaveInfoOk;
        Rectangle recGameOver, recGameOverInfoOk;
        Rectangle recNoMeat;
        Rectangle recEnergy;

        Texture2D actualSaveInfoOk;
        Texture2D actualGameOverInfoOk;

        Color resumeButtonColor = Color.White;
        Color saveButtonColor = Color.White;
        Color backToMainMenuButtonColor = Color.White;
        Color exitButtonColor = Color.White;
        Color Wolf1ButtonSetColor = Color.White, Wolf3ButtonSetColor = Color.White, Wolf2ButtonSetColor = Color.White;
        Color actualQuestButtonColor = Color.Gray;
        public bool ifPaused = false;
        public bool ifWolfPanel = false;
        public bool ifActualQuestPanel = false;
        public bool ifQuestPanel = false;
        public bool ifSaveInfo = false;
        public bool ifGameOver = false;

        int screenWidth, screenWidthOld;
        int screenHeight, screenHeightOld;
        int stringOffsetWidth, stringOffsetHeight;

        string actualNameOfWolfPanel = "";


        double timer = 20;
        double gameOverTimer = 30;
        double dyingTimer = 0.4;

        int consumption = 0;
        private bool ifDying;
        private bool ifNoMeatChanged;

        public HUDController(SpriteBatch batch, GraphicsDevice device, ContentManager manager, int meat, int white_fangs, int gold_fangs, Wataha.GameObjects.Movable.Wataha wataha, HuntingSystem hs)
        {

            this.huntingSystem = hs;
            this.wataha = wataha;
            this.device = device;
            this.spriteBatch = batch;
            this.Content = manager;
            Resources.Meat = meat;
            Resources.Whitefangs = white_fangs;
            Resources.Goldfangs = gold_fangs;
            pictures = new List<Texture2D>();


            font30 = Content.Load<SpriteFont>("Fonts/font1");
            arial18Italic = Content.Load<SpriteFont>("Fonts/arial/arial18");
            arial15Italic = Content.Load<SpriteFont>("Fonts/arial/arial15");

            arial12Italic = Content.Load<SpriteFont>("Fonts/arial/arial12");
            broadwayFont = Content.Load<SpriteFont>("Fonts/Broadway");

            pictures.Add(Content.Load<Texture2D>("Pictures/panel"));
            pictures.Add(Content.Load<Texture2D>("Pictures/meat"));
            pictures.Add(Content.Load<Texture2D>("Pictures/goldFangs"));
            pictures.Add(Content.Load<Texture2D>("Pictures/whiteFang"));
            pictures.Add(Content.Load<Texture2D>("Pictures/pauseScreen"));
            pictures.Add(Content.Load<Texture2D>("Pictures/resumeButton")); // 5
            pictures.Add(Content.Load<Texture2D>("Pictures/backToMainMenu"));
            pictures.Add(Content.Load<Texture2D>("Pictures/exitButton"));
            pictures.Add(Content.Load<Texture2D>("Pictures/rectangleForButtons"));//8
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto"));
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto2"));  //10
            pictures.Add(Content.Load<Texture2D>("Pictures/buttonPhoto3"));
            pictures.Add(Content.Load<Texture2D>("Pictures/actualQuestButton"));
            pictures.Add(Content.Load<Texture2D>("Pictures/saveGameButton"));
            pictures.Add(Content.Load<Texture2D>("Pictures/saveInfo"));//14
            pictures.Add(Content.Load<Texture2D>("Pictures/saveInfoOk"));//15
            pictures.Add(Content.Load<Texture2D>("Pictures/saveInfoOk2"));//16
            pictures.Add(Content.Load<Texture2D>("Pictures/gameOver")); //17
            pictures.Add(Content.Load<Texture2D>("Pictures/noMeat")); //18
            pictures.Add(Content.Load<Texture2D>("pictures/energy")); //19


            actualSaveInfoOk = pictures[15];
            actualGameOverInfoOk = pictures[15];

            wolfPanel = new WolfPanel(Content.Load<Texture2D>("Pictures/rectangleForWolfPanel"), broadwayFont);
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/exitPicture"));
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/GoHuntingButton"));
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton"));
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton2"));


            wolfPanel.font21 = Content.Load<SpriteFont>("Fonts/broadway21");
            wolfPanel.font18 = Content.Load<SpriteFont>("Fonts/broadway18");
            wolfPanel.font14 = Content.Load<SpriteFont>("Fonts/broadway14");

            actualQuestPanel = new ActualQuestPanel(Content.Load<Texture2D>("Pictures/actualQuestPanel"), wolfPanel.font14);
            QuestPanel = new QuestPanel(Content, wolfPanel.font14);
            marketPanel = new MarketPanel(Content);

            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            screenWidthOld = 0;
            screenHeightOld = 0;
            stringOffsetWidth = 0;
            stringOffsetHeight = 0;

            huntingSystem.hudHunting = new HUDHunting(spriteBatch, device, Content);
        }



        public void Update(GameTime gameTime)
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recResources.X = screenWidth / 3;
                recResources.Y = (int)(screenHeight * 0.02);
                recResources.Height = screenHeight / 11;
                recResources.Width = screenWidth / 3;

                stringOffsetWidth = (recResources.Width / 100);
                stringOffsetHeight = (recResources.Height / 100);

                recPausePanel.X = screenWidth / 3;
                recPausePanel.Y = (screenHeight / 100) * 5;
                recPausePanel.Height = (screenHeight / 10) * 9;
                recPausePanel.Width = screenWidth / 3;


                recResumeButton.X = recPausePanel.X + recPausePanel.Width / 4;
                recResumeButton.Y = recPausePanel.Y + recPausePanel.Height / 4;
                recResumeButton.Width = recPausePanel.Width / 2;
                recResumeButton.Height = recPausePanel.Height / 10;

                recSaveButton.X = recResumeButton.X;
                recSaveButton.Y = recResumeButton.Y + recPausePanel.Height / 6;
                recSaveButton.Width = recPausePanel.Width / 2;
                recSaveButton.Height = recPausePanel.Height / 10;

                recBackToMainMenuButton.X = recResumeButton.X;
                recBackToMainMenuButton.Y = recSaveButton.Y + recPausePanel.Height / 6;
                recBackToMainMenuButton.Width = recPausePanel.Width / 2;
                recBackToMainMenuButton.Height = recPausePanel.Height / 10;

                recExitButton.X = recResumeButton.X;
                recExitButton.Y = recBackToMainMenuButton.Y + recPausePanel.Height / 6;
                recExitButton.Width = recPausePanel.Width / 2;
                recExitButton.Height = recPausePanel.Height / 10;

                recButtonPanel.X = (int)(screenWidth * 0.85);
                recButtonPanel.Y = recResources.Y + recResources.Height;
                recButtonPanel.Height = (int)(screenHeight * 0.5);
                recButtonPanel.Width = (int)(screenWidth * 0.15);

    

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

                recActualQuestButton.X = (int)(screenWidth * 0.02);
                recActualQuestButton.Y = (int)(screenHeight * 0.02);
                recActualQuestButton.Width = screenHeight / 8;
                recActualQuestButton.Height = recActualQuestButton.Width;



                recSaveInfo.X = recPausePanel.X + recPausePanel.Width / 3;
                recSaveInfo.Y = recPausePanel.Y + recPausePanel.Width / 2;
                recSaveInfo.Width = recPausePanel.Width / 2;
                recSaveInfo.Height = recPausePanel.Height / 6;

                recSaveInfoOk.X = recSaveInfo.X + (int)(recSaveInfo.Width * 0.4);
                recSaveInfoOk.Y = recSaveInfo.Y + recSaveInfo.Height - recSaveInfo.Height / 3;
                recSaveInfoOk.Width = recSaveInfo.Width / 6;
                recSaveInfoOk.Height = recSaveInfo.Width / 9;

                recGameOver.X = (int)(screenWidth * 0.35);
                recGameOver.Y = screenHeight / 3;
                recGameOver.Width = (int)(screenWidth * 0.3);
                recGameOver.Height = screenHeight / 4;

                recGameOverInfoOk.X = recGameOver.X + (int)(recGameOver.Width * 0.42);
                recGameOverInfoOk.Y = recGameOver.Y + recGameOver.Height - recGameOver.Height / 4;
                recGameOverInfoOk.Width = recGameOver.Width / 6;
                recGameOverInfoOk.Height = recGameOver.Height / 6;

                recNoMeat.X = (int)(screenWidth * 0.46);
                recNoMeat.Y = (int)(screenHeight * 0.15);
                recNoMeat.Width = screenWidth / 16;
                recNoMeat.Height = screenHeight / 16;

                wolfPanel.Update(screenWidth, screenHeight);
                actualQuestPanel.Update(screenWidth, screenHeight);
                QuestPanel.Update(screenWidth, screenHeight);
                marketPanel.Update(screenWidth, screenHeight);
            }











            InputSystem.UpdateCursorPosition();

            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;


            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;


            if (!ifPaused && !ifGameOver)
            {
                foreach (Wolf w in wataha.wolves)
                {
                    consumption += w.strength + w.speed;
                }
                consumption /= 4;

                if (timer > 0)
                {
                    timer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                }
                if (timer <= 0)
                {

                    Resources.Meat -= consumption;
                    timer = 20;
                }
                if (Resources.Meat <= 0)
                {
                    ifDying = true;
                    gameOverTimer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
                }
                else
                {
                    ifDying = false;
                    gameOverTimer = 30;
                }

                if (gameOverTimer <= 0)
                {
                    ifGameOver = true;
                }


                if (ifDying)
                {

                    dyingTimer -= gameTime.ElapsedGameTime.TotalMilliseconds / 1000;

                    if (dyingTimer <= 0)
                    {
                        if (!ifNoMeatChanged)
                        {
                            recNoMeat.X += recResources.Width / 30;
                            recNoMeat.Width /= 2;
                            recNoMeat.Height /= 2;
                        }
                        else
                        {
                            recNoMeat.X -= recResources.Width / 30;
                            recNoMeat.Width *= 2;
                            recNoMeat.Height *= 2;
                        }
                        ifNoMeatChanged = !ifNoMeatChanged;
                        if (gameOverTimer <= 10)
                        {
                            dyingTimer = 0.2;
                        }
                        else
                        {
                            dyingTimer = 0.4;
                        }

                    }

                }
                else
                {
                    dyingTimer = 0.4;
                }

                Wolf1ButtonEvent(); Wolf2ButtonEvent(); Wolf3ButtonEvent();

                ActualQuestButtonEvent();

                if (marketPanel.active)
                {


                    if (marketPanel.Buy1ButtonEvent())
                    {
                        if (Resources.Meat >= 5)
                        {
                            Resources.Meat -= 5;
                            Resources.Whitefangs += 1;
                        }


                    }
                    if (marketPanel.Buy2ButtonEvent())
                    {
                        if (Resources.Meat >= 15)
                        {
                            Resources.Meat -= 15;
                            Resources.Goldfangs += 1;
                        }

                    }
                    if (marketPanel.ExitButtonEvent())
                    {
                        marketPanel.active = false;
                    }

                }


                if (ifQuestPanel)
                {
                    QuestPanel.AcceptButtonEvent();
                    if (QuestPanel.CancelButtonEvent())
                    {
                        ifQuestPanel = false;
                    }

                    if (QuestPanel.AcceptButtonEvent())
                    {
                        QuestSystem.currentQuest = QuestSystem.currentGiver.actualQuest;
                        QuestSystem.currentQuest.questStatus = Quest.status.ACTIVE;
                        ifQuestPanel = false;
                    }
                }

                if (ifWolfPanel)
                {

                    if (wolfPanel.upgradeButtonEvent())
                    {

                    }
                    if (wolfPanel.upgradeButtonEvent2())
                    {

                    }

                    if (wolfPanel.goHuntingButtonEvent(InputSystem.Cursor))
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

                    if (wolfPanel.exitButtonEvent(InputSystem.Cursor))
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



            spriteBatch.DrawString(arial18Italic, Resources.Meat.ToString(), new Vector2(recResources.X + (int)(recResources.Width * 0.25), recResources.Y + (int)(recResources.Height * 0.25)), Color.White);
            spriteBatch.DrawString(arial12Italic, "-" + consumption + " / 20s", new Vector2(recResources.X + (int)(recResources.Width * 0.24), recResources.Y + (int)(recResources.Height * 0.67)), Color.Red);


            spriteBatch.DrawString(arial18Italic, Resources.Whitefangs.ToString(), new Vector2(recResources.X + (int)(recResources.Width * 0.55), recResources.Y + (int)(recResources.Height * 0.30)), Color.White);
            spriteBatch.DrawString(arial18Italic, Resources.Goldfangs.ToString(), new Vector2(recResources.X + (int)(recResources.Width * 0.82), recResources.Y + (int)(recResources.Height * 0.30)), Color.White);

            spriteBatch.Draw(pictures[8], recButtonPanel, Color.White);  //panel kontrolek wilków

            // spriteBatch.Draw(pictures[9], recButtonWolf1Set, Wolf1ButtonSetColor); // 
              spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Kimiko")).ToList()[0].energy.ToString(), new Vector2(recButtonPanel.X + (int)(recButtonPanel.Width * 0.56), recButtonPanel.Y + (int)(recButtonPanel.Width * 1.54)), Color.Green);

            //   spriteBatch.Draw(pictures[10], recButtonWolf2Set, Wolf2ButtonSetColor);
                spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Yua")).ToList()[0].energy.ToString(), new Vector2(recButtonPanel.X + (int)(recButtonPanel.Width * 0.56), recButtonPanel.Y + (int)(recButtonPanel.Width * 0.95)), Color.Green);

            //    spriteBatch.Draw(pictures[11], recButtonWolf3Set, Wolf3ButtonSetColor);
            spriteBatch.DrawString(broadwayFont, (wataha.wolves.Where(w => w.Name == "Hatsu")).ToList()[0].energy.ToString(), new Vector2(recButtonPanel.X + (int)(recButtonPanel.Width * 0.56), recButtonPanel.Y+ (int)(recButtonPanel.Width * 0.27)), Color.Green);

            spriteBatch.Draw(pictures[12], recActualQuestButton, actualQuestButtonColor);





            if (ifWolfPanel)
            {
                wolfPanel.Draw(spriteBatch);
            }

            if (ifActualQuestPanel)
            {
                if (QuestSystem.currentQuest != null)
                    actualQuestPanel.SetPanel(QuestSystem.currentQuest);
                else
                    actualQuestPanel.ClearPanel();
                actualQuestPanel.Draw(spriteBatch);
            }

            if (QuestSystem.currentGiver != null && QuestSystem.currentGiver.actualQuest != QuestSystem.currentQuest && !ifQuestPanel)
            {
                QuestPanel.DrawInfo(spriteBatch);
            }

            if (ifQuestPanel)
            {
                QuestPanel.SetPanel(QuestSystem.currentGiver.actualQuest);
                QuestPanel.Draw(spriteBatch);
            }
            if (marketPanel.infoActive)
            {
                marketPanel.DrawInfo(spriteBatch);
            }
            if (marketPanel.active)
            {
                marketPanel.Draw(spriteBatch);
            }


            if (ifDying)
            {
                spriteBatch.Draw(pictures[18], recNoMeat, Color.White);
                spriteBatch.DrawString(arial18Italic, gameOverTimer.ToString("0.# s"), new Vector2(recResources.X + (int)(recResources.Width * 0.43), recResources.Y + recResources.Height), Color.Red);
            }
            if (ifPaused)
            {
                spriteBatch.Draw(pictures[4], recPausePanel, Color.White);

                spriteBatch.Draw(pictures[5], recResumeButton, resumeButtonColor);
                spriteBatch.Draw(pictures[13], recSaveButton, saveButtonColor);
                spriteBatch.Draw(pictures[6], recBackToMainMenuButton, backToMainMenuButtonColor);
                spriteBatch.Draw(pictures[7], recExitButton, exitButtonColor);
                if (ifSaveInfo)
                {
                    spriteBatch.Draw(pictures[14], recSaveInfo, Color.White);
                    spriteBatch.Draw(actualSaveInfoOk, recSaveInfoOk, Color.White);
                }

            }

            if (ifGameOver)
            {
                spriteBatch.Draw(pictures[17], recGameOver, Color.White);
                spriteBatch.Draw(actualGameOverInfoOk, recGameOverInfoOk, Color.White);

            }
            spriteBatch.End();

            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }


        public bool InfoGameOverOkEvent()
        {
            if (recGameOverInfoOk.Intersects(InputSystem.Cursor))
            {
                actualGameOverInfoOk = pictures[16];
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    ifGameOver = false;
                    return true;
                }
                return false;

            }
            actualGameOverInfoOk = pictures[15];
            return false;
        }

        public bool InfoSaveOkEvent()
        {
            if (recSaveInfoOk.Intersects(InputSystem.Cursor))
            {
                actualSaveInfoOk = pictures[16];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    ifSaveInfo = false;
                    return true;
                }
                return false;
            }

            actualSaveInfoOk = pictures[15];
            return false;
        }


        public bool ActualQuestButtonEvent()
        {
            if (recActualQuestButton.Intersects(InputSystem.Cursor))
            {
                if (!ifActualQuestPanel)
                {
                    actualQuestButtonColor = Color.White;
                    if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                    {
                        recActualQuestButton.X = (int)(screenWidth * 0.02);
                        recActualQuestButton.Y = (int)(screenHeight * 0.02);
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
                        recActualQuestButton.X = (int)(screenWidth * 0.02);
                        recActualQuestButton.Y = (int)(screenHeight * 0.02);
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
            if (recButtonWolf1Set.Intersects(InputSystem.Cursor))
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
            if (recButtonWolf2Set.Intersects(InputSystem.Cursor))
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
            if (recButtonWolf3Set.Intersects(InputSystem.Cursor))
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
            if ((recResumeButton.Intersects(InputSystem.Cursor)))
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

        public bool SaveButtonEvent()
        {
            if ((recSaveButton.Intersects(InputSystem.Cursor)))
            {
                saveButtonColor = Color.Yellow;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                    return true;
                return false;
            }
            else
                saveButtonColor = Color.White;
            return false;
        }

        public bool BackToMainMenuButtonEvent()
        {
            if ((recBackToMainMenuButton.Intersects(InputSystem.Cursor)))
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
            if ((recExitButton.Intersects(InputSystem.Cursor)))
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


    }
}
