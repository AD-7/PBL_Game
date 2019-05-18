﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem.Interfejs
{
    public class ActualQuestPanel
    {
        private Texture2D panel;
        private SpriteFont font;
        private Rectangle recActualQuestPanel;

        public ActualQuestPanel(Texture2D panel, SpriteFont font)
        {
            this.panel = panel;
            this.font = font;
        }


        public void SetPanel()   //parametr może quest 
        {

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
        }




    }
}