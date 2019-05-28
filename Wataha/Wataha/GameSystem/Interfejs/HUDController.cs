using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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
        WolfPanel wolfPanel;
        public static ActualQuestPanel actualQuestPanel;
        public static QuestPanel QuestPanel;
        public MarketPanel marketPanel;
        public SpriteFont font30, broadwayFont;
        public List<Texture2D> pictures;
        Rectangle recResumeButton;
        Rectangle recBackToMainMenuButton;
        Rectangle recExitButton;
        Rectangle recPausePanel, recButtonPanel;
        Rectangle recResources, recMeal, recWhiteFang, recGoldFang;
        Rectangle recButtonWolf1Set, recButtonWolf2Set, recButtonWolf3Set;
        Rectangle recActualQuestButton;
        Rectangle recSaveButton;

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
            Resources.Meat = meat;
            Resources.Whitefangs = white_fangs;
            Resources.Goldfangs = gold_fangs;
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
            pictures.Add(Content.Load<Texture2D>("Pictures/saveGameButton"));

            wolfPanel = new WolfPanel(Content.Load<Texture2D>("Pictures/rectangleForWolfPanel"), broadwayFont);
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/exitPicture"));
            wolfPanel.elements.Add(Content.Load<Texture2D>("Pictures/GoHuntingButton"));


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
            huntingSystem.audio = new AudioSystem(Content);
        }

        [Serializable]
        public class SaveGameInfo
        {
            public float Wolf1PositionX { get; set; }
            public float Wolf1PositionY { get; set; }
            public float Wolf1PositionZ { get; set; }
            public float Wolf2PositionX { get; set; }
            public float Wolf2PositionY { get; set; }
            public float Wolf2PositionZ { get; set; }
            public float Wolf3PositionX { get; set; }
            public float Wolf3PositionY { get; set; }
            public float Wolf3PositionZ { get; set; }

            public int Meat { get; set; }

            public SaveGameInfo()
            {

            }

            public SaveGameInfo(Vector3 wolf1Position, Vector3 wolf2Position, Vector3 wolf3Position)
            {
                Wolf1PositionX = wolf1Position.X;
                Wolf1PositionY = wolf1Position.Y;
                Wolf1PositionZ = wolf1Position.Z;
                Wolf2PositionX = wolf2Position.X;
                Wolf2PositionY = wolf2Position.Y;
                Wolf2PositionZ = wolf2Position.Z;
                Wolf3PositionX = wolf3Position.X;
                Wolf3PositionY = wolf3Position.Y;
                Wolf3PositionZ = wolf3Position.Z;
                //Meat = meat;
            }

        }

        public void SaveGame()
        {

            string fileName = "save.txt";

            SaveGameInfo saveGameInfo = new SaveGameInfo(wataha.wolves.ElementAt(0).position, wataha.wolves.ElementAt(1).position, wataha.wolves.ElementAt(2).position);
            FileStream fs = new FileStream(fileName, FileMode.Create);

            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                if (saveGameInfo != null) formatter.Serialize(fs, saveGameInfo);
                // etc....
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally { fs.Close(); }
        }


        public void Update()
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recResources.X = 0;
                recResources.Y = (int)(screenHeight * 0.02);
                recResources.Height = screenHeight / 11;
                recResources.Width = screenWidth / 2;

                stringOffsetWidth = (recResources.Width / 100);
                stringOffsetHeight = (recResources.Height / 100);

                recMeal.X = (int)(recResources.Width * 0.02);
                recMeal.Y = (int)(recResources.Height * 0.5);
                recMeal.Height = (int)(recResources.Height * 0.25);
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

                recSaveButton.X = recResumeButton.X;
                recSaveButton.Y = recPausePanel.Y + recPausePanel.Height / 2;
                recSaveButton.Width = recPausePanel.Width / 2;
                recSaveButton.Height = recPausePanel.Height / 10;

                recBackToMainMenuButton.X = recResumeButton.X;
                recBackToMainMenuButton.Y = recSaveButton.Y + recPausePanel.Height / 6;
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
                QuestPanel.Update(screenWidth, screenHeight);
                marketPanel.Update(screenWidth, screenHeight);
            }

            InputSystem.UpdateCursorPosition();

            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;


            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;


            if (!ifPaused)
            {
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

                    if(QuestPanel.AcceptButtonEvent())
                    {
                        QuestSystem.currentQuest = QuestSystem.currentGiver.actualQuest;
                        ifQuestPanel = false;
                    }
                }

                if (ifWolfPanel)
                {

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


            spriteBatch.Draw(pictures[1], recMeal, Color.White);   // meat picture
            spriteBatch.DrawString(font30, Resources.Meat.ToString(), new Vector2(recMeal.X + stringOffsetWidth * 14, recMeal.Y + stringOffsetHeight * 32), Color.Red);

            spriteBatch.Draw(pictures[3], recWhiteFang, Color.White);     //whitefangs picture
            spriteBatch.DrawString(font30, Resources.Whitefangs.ToString(), new Vector2(recWhiteFang.X + stringOffsetWidth * 19, recWhiteFang.Y + stringOffsetHeight * 32), Color.White);

            spriteBatch.Draw(pictures[2], recGoldFang, Color.White);     //goldfangs picture
            spriteBatch.DrawString(font30, Resources.Goldfangs.ToString(), new Vector2(recGoldFang.X + stringOffsetWidth * 16, recGoldFang.Y + stringOffsetHeight * 32), Color.Gold);

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





            if (ifWolfPanel)
            {
                wolfPanel.Draw(spriteBatch);
            }

            if (ifActualQuestPanel)
            {
                actualQuestPanel.SetPanel(QuestSystem.currentQuest);
                actualQuestPanel.Draw(spriteBatch);
            }

            if (QuestSystem.currentGiver != null &&  QuestSystem.currentGiver.actualQuest != QuestSystem.currentQuest && !ifQuestPanel)
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

            if (ifPaused)
            {
                spriteBatch.Draw(pictures[4], recPausePanel, Color.White);

                spriteBatch.Draw(pictures[5], recResumeButton, resumeButtonColor);
                spriteBatch.Draw(pictures[13], recSaveButton, saveButtonColor);
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
            if (recActualQuestButton.Intersects(InputSystem.Cursor))
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
