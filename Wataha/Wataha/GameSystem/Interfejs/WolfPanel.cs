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
        private Texture2D actualEvo;
        private SpriteFont font;
        private Rectangle recWolfPanel;
        private Rectangle recExit;
        private Rectangle recSkills;
        private Rectangle recEvo;


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
        bool ifInEvo;

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
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/hatsuPanelEvo"));         //10
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/yuaPanelEvo"));           //11
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/kimikoPanelEvo"));        //12
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/evolution1"));            //13
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/evolution2"));            //14
            elements.Add(Content.Load<Texture2D>("Pictures/wolfpanel/evolution3"));            //15

            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton"));
            elements.Add(Content.Load<Texture2D>("Pictures/upgradeButton2"));



        }

        public void SetPanel(Wolf wolf)
        {
            actualSkills = elements[4];
            actualEvo = elements[13];
            currentUpgradeButton = elements[16];
            currentUpgradeButton2 = elements[16];
            ifInSkills = false;
            ifInEvo = false;

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
            recWolfPanel.X = (int)(width * 0.22);
            recWolfPanel.Y = (int)(height * 0.7);
            recWolfPanel.Width = (int)(width * 0.60);
            recWolfPanel.Height = (int)(height * 0.26);

            recExit.X = recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width / 20;
            recExit.Y = recWolfPanel.Y + recWolfPanel.Width / 60;
            recExit.Height = recWolfPanel.Width / 30;
            recExit.Width = recWolfPanel.Width / 30;

            recSkills.X = recWolfPanel.X;
            recSkills.Y = recWolfPanel.Y - (int)(recWolfPanel.Height * 0.15);
            recSkills.Width = recWolfPanel.Width / 5;
            recSkills.Height = recWolfPanel.Height / 6;

            recEvo.X = recWolfPanel.X + recSkills.Width + recSkills.Width / 30;
            recEvo.Y = recSkills.Y;
            recEvo.Width = recSkills.Width;
            recEvo.Height = recSkills.Height;


            recUpgradeButton.X = recWolfPanel.X + (int)(recWolfPanel.Width * 0.35); ; 
            recUpgradeButton.Y = recWolfPanel.Y + recWolfPanel.Height - recWolfPanel.Height/6;
            recUpgradeButton.Width = (int)(recWolfPanel.Width * 0.08);
            recUpgradeButton.Height =(int) (recWolfPanel.Width * 0.03);
            

            recUpgradeButton2.X = recUpgradeButton.X + (int)(recWolfPanel.Width * 0.44);
            recUpgradeButton2.Y = recUpgradeButton.Y;
            recUpgradeButton2.Width = recUpgradeButton.Width;
            recUpgradeButton2.Height = recUpgradeButton.Height;



        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(actualSkills, recSkills, Color.White);
            spriteBatch.Draw(actualEvo, recEvo, Color.White);
            spriteBatch.Draw(actualPanel, recWolfPanel, Color.White);
            spriteBatch.Draw(elements[0], recExit, Color.White);

            if (ifInEvo)
            {
                spriteBatch.Draw(currentUpgradeButton, recUpgradeButton, Color.White);
                spriteBatch.Draw(currentUpgradeButton2, recUpgradeButton2, Color.White);
            }
            if (ifInSkills)
            {
                spriteBatch.DrawString(font, wolfAge.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.175), recWolfPanel.Y + (int)(recWolfPanel.Height*0.39)), Color.White);
                spriteBatch.DrawString(font, wolfStrength.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.335), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.39)), Color.White);
                spriteBatch.DrawString(font, wolfResistance.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.66), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.39)), Color.White);
                spriteBatch.DrawString(font, wolfSpeed.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.865), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.39)), Color.White);
                spriteBatch.DrawString(font, wolfEnergy.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.47), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.73)), Color.White);

            }

        }

        public bool evolutionButtonEvent()
        {

            if (recEvo.Intersects(InputSystem.Cursor))
            {
                if (!ifInEvo)
                    actualEvo = elements[14];
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    actualEvo = elements[15];
                    if (wolfName == "Kimiko")
                        actualPanel = elements[12];
                    else if (wolfName == "Yua")
                        actualPanel = elements[11];
                    else if (wolfName == "Hatsu")
                        actualPanel = elements[10];
                     ifInSkills = false;
                    ifInEvo = true;
                  
                    return true;
                }
                return false;
            }
            if (!ifInEvo)
                actualEvo = elements[13];
            return false;
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
                    ifInEvo = false;
                    ifInSkills = true;
                    return true;
                }
                return false;
            }
            if (!ifInSkills)
                actualSkills = elements[4];

            return false;
        }


        public bool upgradeButtonEvent()
        {
            if (recUpgradeButton.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton = elements[17];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
            currentUpgradeButton = elements[16];
            return false;
        }
        public bool upgradeButtonEvent2()
        {
            if (recUpgradeButton2.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton2 = elements[17];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    return true;
                }
                return false;
            }
            currentUpgradeButton2 = elements[16];
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
