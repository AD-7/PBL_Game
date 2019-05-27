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
            title = quest.questTitle;
            description = quest.questDescription;
            reward = "Meat: " + quest.MeatReward + "\n" +
                     "White Fang: " + quest.WhiteFangReward + "\n" +
                     "Gold Fang: " + quest.GoldFangReward;
         }

        public void Update(int width, int height)
        {
            recActualQuestPanel.X = (int)((width / 100) * 0.2f);
            recActualQuestPanel.Y = (height / 100) * 40;
            recActualQuestPanel.Width = (width / 100) *37;
            recActualQuestPanel.Height = (height / 100) * 55;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panel, recActualQuestPanel, Color.White);

            spriteBatch.DrawString(font, title, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.5), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.1)), Color.Yellow);
            spriteBatch.DrawString(font, description, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.2), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.5)), Color.Yellow);
            spriteBatch.DrawString(font, reward, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.7), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.5)), Color.Yellow);
        }




    }
}
