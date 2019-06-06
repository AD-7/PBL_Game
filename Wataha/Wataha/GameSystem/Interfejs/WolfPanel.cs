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
        public Texture2D actualPanel;
        public Texture2D actualSkills;
        private SpriteFont font;
        private Rectangle recWolfPanel;
        private Rectangle recExit;
        private Rectangle recSkills;


        private Rectangle recUpgradeButton, recUpgradeButton2;


        private Texture2D currentUpgradeButton, currentUpgradeButton2;


        public List<Texture2D> elements;

        public string wolfName;
        public int wolfAge;
        public int wolfStrength;
        public int wolfResistance;
        public int wolfSpeed;
        public int wolfEnergy;

        bool ifInSkills;

        public WolfPanel(ContentManager Content, SpriteFont font)
        {
            this.font = font;

            elements = new List<Texture2D>();
            elements.Add(Content.Load<Texture2D>("Pictures/exitPicture"));                  //0
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/kimikoPanel"));        //1
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/yuaPanel"));           //2
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/hatsuPanel"));         //3
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/skills1"));             //4
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/skills2"));            //5 
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/skills3"));            //6
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/kimikoPanelSkills"));        //7
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/yuaPanelSkills"));           //8
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/hatsuPanelSkills"));         //9



            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton"));
            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton2"));

            

        }

        public void SetPanel(Wolf wolf)
        {
            actualSkills = elements[4];
            ifInSkills = false;

            wolfName = wolf.Name;
            wolfResistance = wolf.resistance;
            wolfSpeed = wolf.speed;
            wolfStrength = wolf.strength;
            wolfEnergy = wolf.energy;
            if (wolfName == "Kimiko")
            {
                actualPanel = elements[1];
                wolfAge = 7;
            }
            else if (wolfName == "Yua")
            {
                actualPanel = elements[2];
                wolfAge = 2;
            }
            else if (wolfName == "Hatsu")
            {
                actualPanel = elements[3];
                wolfAge = 4;
            }

        }

        public void Update(int width, int height)
        {
            recWolfPanel.X = (int)(width * 0.15);
            recWolfPanel.Y = (int)(height * 0.7);
            recWolfPanel.Width = (int)(width * 0.60);
            recWolfPanel.Height = (int)(height * 0.25);

            recExit.X = recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width / 22;
            recExit.Y = recWolfPanel.Y + recWolfPanel.Width / 60;
            recExit.Height = recWolfPanel.Width / 30;
            recExit.Width = recWolfPanel.Width / 30;

            recSkills.X = recWolfPanel.X;
            recSkills.Y = recWolfPanel.Y - (int)(recWolfPanel.Height * 0.15);
            recSkills.Width = recWolfPanel.Width / 5;
            recSkills.Height = recWolfPanel.Height / 6;



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
            spriteBatch.Draw(actualSkills, recSkills, Color.White);
            spriteBatch.Draw(actualPanel, recWolfPanel, Color.White);
            spriteBatch.Draw(elements[0], recExit, Color.White);


        }



        public bool skillsButtonEvent()
        {
            if (recSkills.Intersects(InputSystem.Cursor))
            {
                if (!ifInSkills)
                    actualSkills = elements[5];
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    actualSkills = elements[6];
                    if (wolfName == "Kimiko")
                        actualPanel = elements[7];
                    else if (wolfName == "Yua")
                        actualPanel = elements[8];
                    else if (wolfName == "Hatsu")
                        actualPanel = elements[9];
                    ifInSkills = true;
                    return true;
                }
                return false;
            }
            if(!ifInSkills)
            actualSkills = elements[4];

            return false;
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
