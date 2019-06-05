using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem.Interfejs
{
    public class WolfPanel
    {
        
        private SpriteFont font;
        private Rectangle recWolfPanel;
        private Rectangle recExit;
        
        private Rectangle recUpgradeButton, recUpgradeButton2;

       
        private Texture2D currentUpgradeButton, currentUpgradeButton2;

    
        public List<Texture2D> elements;
        public string wolfName;
        public int wolfStrength;
        public int wolfResistance;
        public int wolfSpeed;
        public int wolfEnergy;

 

        public WolfPanel(ContentManager Content , SpriteFont font)
        {
            this.font = font;
          
            elements = new List<Texture2D>();
            elements.Add(Content.Load<Texture2D>("Pictures/exitPicture"));
            
            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton"));
            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton2"));


    
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
            recWolfPanel.X = (int)(width * 0.70);
            recWolfPanel.Y = (int)(height * 0.15);
            recWolfPanel.Width = (int)(width * 0.30);
            recWolfPanel.Height = (int)(height * 0.80);

            recExit.X = recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width / 11;
            recExit.Y = recWolfPanel.Y + recWolfPanel.Width / 30;
            recExit.Height = recWolfPanel.Width / 20;
            recExit.Width = recWolfPanel.Width / 20;

           

            recUpgradeButton.X = recWolfPanel.X + (int)(recWolfPanel.Width * 0.2);
            recUpgradeButton.Y = recWolfPanel.Y + recWolfPanel.Height - (int)(recWolfPanel.Height * 0.31);
            recUpgradeButton.Width = recWolfPanel.Width / 6;
            recUpgradeButton.Height = recWolfPanel.Width / 10;


            recUpgradeButton2.X = recUpgradeButton.X + (int)(recWolfPanel.Width * 0.45);
            recUpgradeButton2.Y = recUpgradeButton.Y;
            recUpgradeButton2.Width = recUpgradeButton.Width;
            recUpgradeButton2.Height = recUpgradeButton.Height;
        }

        public void Draw(SpriteBatch spriteBatch)
        {


           
        }

        public bool upgradeButtonEvent()
        {
            if (recUpgradeButton.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton = elements[3];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
            currentUpgradeButton = elements[2];
            return false;
        }
        public bool upgradeButtonEvent2()
        {
            if (recUpgradeButton2.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton2 = elements[3];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
            currentUpgradeButton2 = elements[2];
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
