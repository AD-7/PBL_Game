using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem
{
    public class WolfPanel
    {
        private Texture2D wolfPanelScreen;
        private SpriteFont font;
        private Rectangle recWolfPanel;
        private Rectangle recExit;

        public SpriteFont font21,font18;
        public List<Texture2D> elements;
        public string wolfName;
        public int wolfStrength;
        public int wolfResistance;
        public int wolfSpeed;
        public int wolfEnergy;

        public WolfPanel(Texture2D screen, SpriteFont font)
        {
            this.font = font;
            this.wolfPanelScreen = screen;
            elements = new List<Texture2D>();
        }

        public void SetPanel(Wolf wolf)
        {
            wolfName = wolf.Name;
            wolfResistance = wolf.resistance;
            wolfSpeed = wolf.speed;
            wolfStrength = wolf.strength;
            wolfEnergy = wolf.energy;

        }

        public void Update(int width, int height)
        {
            recWolfPanel.X = (width / 100) * 70;
            recWolfPanel.Y = (height / 100) * 15;
            recWolfPanel.Width = (width / 100) * 30;
            recWolfPanel.Height = (height / 100) * 80;

            recExit.X = recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width/11;
            recExit.Y = recWolfPanel.Y + recWolfPanel.Width/30;
            recExit.Height = recWolfPanel.Width / 20;
            recExit.Width = recWolfPanel.Width / 20;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

        
            spriteBatch.Draw(wolfPanelScreen, recWolfPanel, Color.White);
            spriteBatch.Draw(elements[0], recExit, Color.White);

            spriteBatch.DrawString(font21, wolfName, new Vector2(recWolfPanel.X + recWolfPanel.Width / 3 + recWolfPanel.Width/12, recWolfPanel.Y + recWolfPanel.Height / 30), Color.White);
            int ParametersX = recWolfPanel.X + recWolfPanel.Width / 10;
            int ParametersY = recWolfPanel.Y + recWolfPanel.Height / 8;
            spriteBatch.DrawString(font18, "strength : ", new Vector2( ParametersX,ParametersY), Color.Red);
            spriteBatch.DrawString(font18, wolfStrength.ToString(), new Vector2(ParametersX + (recWolfPanel.Width/100)*25, ParametersY), Color.Red);

            spriteBatch.DrawString(font18, "resistance : ", new Vector2(ParametersX, ParametersY + recWolfPanel.Height / 20), Color.Yellow);
            spriteBatch.DrawString(font18, wolfResistance.ToString(), new Vector2(ParametersX + (recWolfPanel.Width / 100) * 30, ParametersY + recWolfPanel.Height / 20), Color.Yellow);

            spriteBatch.DrawString(font18, "speed : ", new Vector2(ParametersX, ParametersY + recWolfPanel.Height / 10), Color.LightSkyBlue);
            spriteBatch.DrawString(font18, wolfSpeed.ToString(), new Vector2(ParametersX + (recWolfPanel.Width / 100) * 18, ParametersY + recWolfPanel.Height / 10), Color.LightSkyBlue);

            spriteBatch.DrawString(font18, "energy : ", new Vector2(ParametersX + recWolfPanel.Width/2, ParametersY + recWolfPanel.Height / 10), Color.LightGreen);
            spriteBatch.DrawString(font18, wolfEnergy.ToString(), new Vector2(ParametersX + recWolfPanel.Width / 2 + (recWolfPanel.Width / 100) * 20, ParametersY + recWolfPanel.Height / 10), Color.LightGreen);
        }

        public bool exitButtonEvent(Rectangle cursor)
        {
            if (recExit.Intersects(cursor))
            {
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

    }
}
