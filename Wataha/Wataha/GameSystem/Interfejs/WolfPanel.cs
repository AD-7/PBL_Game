﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem.Interfejs
{
    public class WolfPanel
    {
        private Texture2D panel;
        private SpriteFont font;
        private Rectangle recWolfPanel;
        private Rectangle recExit;
        private Rectangle recGoHuntingButton;
        private Color goHuntingButtonColor = Color.Gray;


        public SpriteFont font21, font18, font14;
        public List<Texture2D> elements;
        public string wolfName;
        public int wolfStrength;
        public int wolfResistance;
        public int wolfSpeed;
        public int wolfEnergy;

        public bool ifEnoughEnergy = true;

        public WolfPanel(Texture2D panel, SpriteFont font)
        {
            this.font = font;
            this.panel = panel;
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
            recWolfPanel.X = (int)(width  * 0.70);
            recWolfPanel.Y = (int)(height  * 0.15);
            recWolfPanel.Width = (int)(width * 0.30);
            recWolfPanel.Height = (int)(height * 0.80);

            recExit.X = recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width / 11;
            recExit.Y = recWolfPanel.Y + recWolfPanel.Width / 30;
            recExit.Height = recWolfPanel.Width / 20;
            recExit.Width = recWolfPanel.Width / 20;

            recGoHuntingButton.X = recWolfPanel.X + recWolfPanel.Width / 4;
            recGoHuntingButton.Y = recWolfPanel.Y + recWolfPanel.Height - recWolfPanel.Height / 6;
            recGoHuntingButton.Width = recWolfPanel.Width / 2;
            recGoHuntingButton.Height = recWolfPanel.Height / 16;


        }

        public void Draw(SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(panel, recWolfPanel, Color.White);
            spriteBatch.Draw(elements[0], recExit, Color.White);

            spriteBatch.DrawString(font21, wolfName, new Vector2(recWolfPanel.X + recWolfPanel.Width / 3 + recWolfPanel.Width / 12, recWolfPanel.Y + recWolfPanel.Height / 30), Color.OrangeRed);
            int ParametersX = recWolfPanel.X + recWolfPanel.Width / 9;
            int ParametersY = recWolfPanel.Y + recWolfPanel.Height / 6;
            spriteBatch.DrawString(font18, "strength : ", new Vector2(ParametersX, ParametersY), Color.Red);
            spriteBatch.DrawString(font18, wolfStrength.ToString(), new Vector2(ParametersX + (recWolfPanel.Width / 100) * 25, ParametersY), Color.Red);

            spriteBatch.DrawString(font18, "resistance : ", new Vector2(ParametersX, ParametersY + recWolfPanel.Height / 20), Color.Yellow);
            spriteBatch.DrawString(font18, wolfResistance.ToString(), new Vector2(ParametersX + (recWolfPanel.Width / 100) * 30, ParametersY + recWolfPanel.Height / 20), Color.Yellow);

            spriteBatch.DrawString(font18, "speed : ", new Vector2(ParametersX, ParametersY + recWolfPanel.Height / 10), Color.LightSkyBlue);
            spriteBatch.DrawString(font18, wolfSpeed.ToString(), new Vector2(ParametersX + (recWolfPanel.Width / 100) * 18, ParametersY + recWolfPanel.Height / 10), Color.LightSkyBlue);

            spriteBatch.DrawString(font18, "energy : ", new Vector2(ParametersX + recWolfPanel.Width / 2, ParametersY + recWolfPanel.Height / 10), Color.LightGreen);
            spriteBatch.DrawString(font18, wolfEnergy.ToString(), new Vector2(ParametersX + recWolfPanel.Width / 2 + (recWolfPanel.Width / 100) * 20, ParametersY + recWolfPanel.Height / 10), Color.LightGreen);

            int ParametersX2 = recWolfPanel.X + (recWolfPanel.Width / 100) * 43;
            int ParametersY2 = recWolfPanel.Y + (recWolfPanel.Height / 100) * 38;

            spriteBatch.DrawString(font14, "::: Evolution :::", new Vector2(ParametersX2, ParametersY2), Color.OrangeRed);


            spriteBatch.Draw(elements[1], recGoHuntingButton, goHuntingButtonColor);
            if(!ifEnoughEnergy)
            spriteBatch.DrawString(font14, "Not enaugh energy", new Vector2(recGoHuntingButton.X, recGoHuntingButton.Y + recGoHuntingButton.Height + recGoHuntingButton.Height/4), Color.Red);
        }

        public bool goHuntingButtonEvent(Rectangle cursor)
        {
            if (recGoHuntingButton.Intersects(cursor))
            {
                goHuntingButtonColor = Color.White;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
                return false;
            }
            goHuntingButtonColor = Color.Gray;
            return false;
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
