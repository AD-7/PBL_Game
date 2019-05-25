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
