using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Interable;

namespace Wataha.GameSystem.Interfejs
{
    public class QuestPanel
    {
        private List<Texture2D> panelTextures;
        private SpriteFont font;
        private Rectangle recQuestPanel;

        private Rectangle recAcceptQuest;
        private Rectangle recCancelQuest;
        private Rectangle recInteract;


        private Texture2D currentAccept; 
        private Texture2D currentCancel;

        private string description = "";
        private string reward = "";
        private string title = "";
        private string requirements = "";
		private string NeedStrenght = "";
		private string NeedSpeed = "";
		private string NeedResistance = "";


		public QuestPanel(ContentManager manager, SpriteFont font)
        {
            panelTextures = new List<Texture2D>()
            {
                manager.Load<Texture2D>("Pictures/QuestPanel/QuestPanel"),  //0
                manager.Load<Texture2D>("Pictures/QuestPanel/accept"),      //1
                manager.Load<Texture2D>("Pictures/QuestPanel/accept2"),     //2
                manager.Load<Texture2D>("Pictures/QuestPanel/cancel"),      //3
                manager.Load<Texture2D>("Pictures/QuestPanel/cancel2"),     //4
                manager.Load<Texture2D>("Pictures/QuestPanel/pressFInfo")   //5
                

            };
            this.font = font;
            currentCancel = panelTextures[3];
            currentAccept = panelTextures[1];
        }


        public void SetPanel(Quest quest)   
        {
            title = quest.questTitle;
            description = quest.questDescription;
            reward = quest.MeatReward + "\n \n" +
                     quest.WhiteFangReward + "\n \n" +
                     quest.GoldFangReward;
			NeedStrenght = quest.NeedStrenght + "";
			NeedSpeed = quest.NeedSpeed + "";
			NeedResistance =   quest.NeedResistance + "";
            //  if(quest.reward) reward = "\n Dostep do kolejnego obszaru \n"; 
        }

        public void Update(int width, int height)
        {
            recQuestPanel.X = (int)(width * 0.2);
            recQuestPanel.Y = (int)(height  * 0.15);
            recQuestPanel.Width = (int)(width * 0.6);
            recQuestPanel.Height = (int)(height * 0.7);

            recAcceptQuest.X = (int)(recQuestPanel.X + 0.2 * recQuestPanel.Width);
            recAcceptQuest.Y = (int)(recQuestPanel.Y + 0.9 * recQuestPanel.Height);
            recAcceptQuest.Width = (int)(recQuestPanel.Width * 0.1);
            recAcceptQuest.Height = (int)(recQuestPanel.Height * 0.05);

            recCancelQuest.X = (int)(recQuestPanel.X + 0.7 * recQuestPanel.Width);
            recCancelQuest.Y = (int)(recAcceptQuest.Y);
            recCancelQuest.Width = (int)(recAcceptQuest.Width);
            recCancelQuest.Height = (int)(recAcceptQuest.Height);

            recInteract.X = (int)(width * 0.5) - (int)(width * 0.1);
            recInteract.Y = (int)(height * 0.20);
            recInteract.Width = (int)(width * 0.2);
            recInteract.Height = (int)(height  * 0.1);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelTextures[0], recQuestPanel, Color.White);
            spriteBatch.Draw(currentAccept, recAcceptQuest, Color.White);
            spriteBatch.Draw(currentCancel, recCancelQuest, Color.White);

            spriteBatch.DrawString(font, title, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.45), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.01)), Color.Yellow);
            spriteBatch.DrawString(font, description, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.05), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.2)), Color.Yellow);
            spriteBatch.DrawString(font, reward, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.8), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.2)), Color.Yellow);
            spriteBatch.DrawString(font, NeedStrenght, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.4), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.8)), Color.Yellow);
			spriteBatch.DrawString(font, NeedResistance, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.6), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.8)), Color.Yellow);
			spriteBatch.DrawString(font, NeedSpeed, new Vector2((int)(recQuestPanel.X + recQuestPanel.Width * 0.8), recQuestPanel.Y + (int)(recQuestPanel.Height * 0.8)), Color.Yellow);
        }

        public void DrawInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelTextures[5], recInteract, Color.White);
        }


        public bool AcceptButtonEvent()
        {
            if (recAcceptQuest.Intersects(InputSystem.Cursor))
            {
                currentAccept = panelTextures[2];
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
                currentAccept = panelTextures[1];
            return false;
        }

        public bool CancelButtonEvent()
        {
            if (recCancelQuest.Intersects(InputSystem.Cursor))
            {
                currentCancel = panelTextures[4];
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }

                return false;
            }
            else
                currentCancel = panelTextures[3];
            return false;
        }

    }
}
