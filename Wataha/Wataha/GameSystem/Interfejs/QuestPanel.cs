using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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

        public QuestPanel(ContentManager manager, SpriteFont font)
        {
            panelTextures = new List<Texture2D>()
            {
                manager.Load<Texture2D>("Pictures/QuestPanel/QuestPanel"),
                manager.Load<Texture2D>("Pictures/QuestPanel/accept"),
                manager.Load<Texture2D>("Pictures/QuestPanel/accept2"),
                manager.Load<Texture2D>("Pictures/QuestPanel/cancel"),
                manager.Load<Texture2D>("Pictures/QuestPanel/cancel2")

            };
            this.font = font;
        }


        public void SetPanel(Quest quest)   
        {

        }

        public void Update(int width, int height)
        {
            recQuestPanel.X = (int)((width / 100) * 0.2f);
            recQuestPanel.Y = (height / 100) * 40;
            recQuestPanel.Width = (width / 100) *37;
            recQuestPanel.Height = (height / 100) * 55;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panelTextures[0], recQuestPanel, Color.White);
        }




    }
}
