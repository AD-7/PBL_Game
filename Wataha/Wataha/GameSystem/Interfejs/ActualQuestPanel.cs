﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
                reward = quest.MeatReward + "      " +
                         quest.WhiteFangReward + "      " +
                         quest.GoldFangReward;

                if (quest is SheepQuest)
                    description += "\n sheep in craft:" + ((SheepQuest)quest).sheepInCroft +
                                   "\n sheep was eat:" + ((SheepQuest)quest).eatSheep;
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
            recActualQuestPanel.X = (int)(width * 0.0012f);
            recActualQuestPanel.Y = (int)(height * 0.12);
            recActualQuestPanel.Width = (int)(width * 0.37);
            recActualQuestPanel.Height = (int)(height * 0.62);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(panel, recActualQuestPanel, Color.White);

            spriteBatch.DrawString(font, title, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.45), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.05)), Color.Yellow);
            spriteBatch.DrawString(font, description, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.05), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.7)), Color.Yellow);
            spriteBatch.DrawString(font, reward, new Vector2((int)(recActualQuestPanel.X + recActualQuestPanel.Width * 0.09), recActualQuestPanel.Y + (int)(recActualQuestPanel.Height * 0.2)), Color.Yellow);
        }




    }
}
