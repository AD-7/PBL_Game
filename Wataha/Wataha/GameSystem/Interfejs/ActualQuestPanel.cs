using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Interable;

namespace Wataha.GameSystem.Interfejs
{
    public class ActualQuestPanel
    {
        private Texture2D panel;
        private SpriteFont font;
        private Rectangle recActualQuestPanel;

        private string description = "";
        private string reward = "";
        private string title = "";

        public ActualQuestPanel(Texture2D panel, SpriteFont font)
        {
            this.panel = panel;
            this.font = font;
        }


        public void SetPanel(Quest quest)
        {
            if (quest != null)
            {
                title = quest.questTitle;
                description = quest.questDescription;
                reward = "Meat: " + quest.MeatReward + "    " +
                         "White Fang: " + quest.WhiteFangReward + "    " +
                         "Gold Fang: " + quest.GoldFangReward;
            }
         }

        public void ClearPanel()
        {
            description = "";
            reward = "";
            title = "";
        }

        public void Update(int width, int height)
        {
            recActualQuestPanel.X = (int)((width / 100) * 0.12f);
            recActualQuestPanel.Y = (height / 100) * 12;
            recActualQuestPanel.Width = (width / 100) *37;
            recActualQuestPanel.Height = (height / 100) * 62;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panel, recActualQuestPanel, Color.White);

            spriteBatch.DrawString(font, title, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.45), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.05)), Color.Yellow);
            spriteBatch.DrawString(font, description, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.05), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.15)), Color.Yellow);
            spriteBatch.DrawString(font, reward, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.09), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.9)), Color.Yellow);
        }




    }
}
