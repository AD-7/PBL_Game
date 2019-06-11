using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
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

        private int wolf1Level;
        private int wolf2Level;
        private int wolf3Level;

        public List<UpgradeSystem> skills = new List<UpgradeSystem>();

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

            skills.Add(new UpgradeSystem(2, 1, 1, 0, 50, 0, 0));
            skills.Add(new UpgradeSystem(1, 2, 0, 0, 50, 0, 0));
            skills.Add(new UpgradeSystem(5, 5, 0, 0, 70, 10, 0));
            skills.Add(new UpgradeSystem(0, 0, 2, 2, 90, 20, 0));
            skills.Add(new UpgradeSystem(5, 2, 0, 5, 10, 50, 10));
            skills.Add(new UpgradeSystem(2, 8, 2, 0, 20, 50, 5));
            skills.Add(new UpgradeSystem(0, 0, 6, 5, 80, 20, 10));
            skills.Add(new UpgradeSystem(8, 5, 0, 0, 50, 50, 0));
            skills.Add(new UpgradeSystem(5, 2, 0, 5, 10, 50, 10));
            skills.Add(new UpgradeSystem(2, 8, 2, 0, 20, 50, 5));
            skills.Add(new UpgradeSystem(10, 2, 0, 5, 100, 30, 10));
            skills.Add(new UpgradeSystem(2, 10, 5, 0, 100, 30, 15));
            skills.Add(new UpgradeSystem(8, 12, 10, 1, 150, 20, 10));
            skills.Add(new UpgradeSystem(9, 12, 2, 15, 200, 50, 0));

            wolf1Level = 0;
            wolf2Level = 0;
            wolf3Level = 0;
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

            recExit.X = (int)(recWolfPanel.X + recWolfPanel.Width - recWolfPanel.Width * 0.05);
            recExit.Y = (int)(recWolfPanel.Y + recWolfPanel.Width * 0.017);
            recExit.Height = (int)(recWolfPanel.Width * 0.033);
            recExit.Width = (int)(recWolfPanel.Width * 0.033);

            recSkills.X = recWolfPanel.X;
            recSkills.Y = recWolfPanel.Y - (int)(recWolfPanel.Height * 0.15);
            recSkills.Width = (int)(recWolfPanel.Width * 0.2);
            recSkills.Height = (int)(recWolfPanel.Height * 0.17);

            recEvo.X = (int)(recWolfPanel.X + recSkills.Width + recSkills.Width * 0.30);
            recEvo.Y = recSkills.Y;
            recEvo.Width = recSkills.Width;
            recEvo.Height = recSkills.Height;


            recUpgradeButton.X = recWolfPanel.X + (int)(recWolfPanel.Width * 0.35); ;
            recUpgradeButton.Y = (int)(recWolfPanel.Y + recWolfPanel.Height - recWolfPanel.Height * 0.17);
            recUpgradeButton.Width = (int)(recWolfPanel.Width * 0.08);
            recUpgradeButton.Height = (int)(recWolfPanel.Width * 0.03);


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
               

                int wolfLevel = 0;

                if (wolfName == "Kimiko")
                    wolfLevel = wolf3Level;
                else if (wolfName == "Yua")
                    wolfLevel = wolf2Level;
                else if (wolfName == "Hatsu")
                    wolfLevel = wolf1Level;

                spriteBatch.Draw(currentUpgradeButton, recUpgradeButton, Color.White);
                spriteBatch.Draw(currentUpgradeButton2, recUpgradeButton2, Color.White);

                spriteBatch.DrawString(font, skills[wolfLevel].strength.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.37), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.35)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel].resistance.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.37), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.45)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel].speed.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.37), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.55)), Color.White);

                spriteBatch.DrawString(font, skills[wolfLevel].costM.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.20), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel].costWF.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.33), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel].costGF.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.43), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);


                spriteBatch.DrawString(font, skills[wolfLevel + 1].strength.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.77), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.35)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel + 1].resistance.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.77), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.45)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel + 1].speed.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.77), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.55)), Color.White);

                spriteBatch.DrawString(font, skills[wolfLevel + 1].costM.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.64), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel + 1].costWF.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.76), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);
                spriteBatch.DrawString(font, skills[wolfLevel + 1].costGF.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.86), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.70)), Color.White);
            }

            if (ifInSkills)
            {
                spriteBatch.DrawString(font, wolfAge.ToString(), new Vector2(recWolfPanel.X + (int)(recWolfPanel.Width * 0.175), recWolfPanel.Y + (int)(recWolfPanel.Height * 0.39)), Color.White);
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


        public bool upgradeButtonEvent(GameObjects.Movable.Wataha wataha)
        {
            if (recUpgradeButton.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton = elements[17];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    if (wolfName == "Kimiko")
                    {
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].strength += skills[wolf3Level].strength;
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].speed += skills[wolf3Level].speed;
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].resistance += skills[wolf3Level].resistance;
                        Resources.Meat -= skills[wolf3Level].costM;
                        Resources.Whitefangs -= skills[wolf3Level].costWF;
                        Resources.Goldfangs -= skills[wolf3Level].costGF;
                        wolf3Level += 2;
                    }
                    else if (wolfName == "Yua")
                    {
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].strength += skills[wolf2Level].strength;
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].speed += skills[wolf2Level].speed;
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].resistance += skills[wolf2Level].resistance;
                        Resources.Meat -= skills[wolf2Level].costM;
                        Resources.Whitefangs -= skills[wolf2Level].costWF;
                        Resources.Goldfangs -= skills[wolf2Level].costGF;
                        wolf2Level += 2;
                    }
                    else if (wolfName == "Hatsu")
                    {
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].strength += skills[wolf1Level].strength;
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].speed += skills[wolf1Level].speed;
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].resistance += skills[wolf1Level].resistance;
                        Resources.Meat -= skills[wolf1Level].costM;
                        Resources.Whitefangs -= skills[wolf1Level].costWF;
                        Resources.Goldfangs -= skills[wolf1Level].costGF;
                        wolf1Level += 2;
                    }

                    return true;
                }
                return false;
            }
            currentUpgradeButton = elements[16];
            return false;
        }
        public bool upgradeButtonEvent2(GameObjects.Movable.Wataha wataha)
        {
            if (recUpgradeButton2.Intersects(InputSystem.Cursor))
            {
                currentUpgradeButton2 = elements[17];

                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    if (wolfName == "Kimiko")
                    {
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].strength += skills[wolf3Level + 1].strength;
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].speed += skills[wolf3Level + 1].speed;
                        wataha.wolves.Where(w => w.Name == "Kimiko").ToList()[0].resistance += skills[wolf3Level + 1].resistance;
                        Resources.Meat -= skills[wolf3Level + 1].costM;
                        Resources.Whitefangs -= skills[wolf3Level + 1].costWF;
                        Resources.Goldfangs -= skills[wolf3Level + 1].costGF;
                        wolf3Level += 2;
                    }
                    else if (wolfName == "Yua")
                    {
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].strength += skills[wolf2Level + 1].strength;
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].speed += skills[wolf2Level + 1].speed;
                        wataha.wolves.Where(w => w.Name == "Yua").ToList()[0].resistance += skills[wolf2Level + 1].resistance;
                        Resources.Meat -= skills[wolf2Level + 1].costM;
                        Resources.Whitefangs -= skills[wolf2Level + 1].costWF;
                        Resources.Goldfangs -= skills[wolf2Level + 1].costGF;
                        wolf2Level += 2;
                    }
                    else if (wolfName == "Hatsu")
                    {
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].strength += skills[wolf1Level + 1].strength;
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].speed += skills[wolf1Level + 1].speed;
                        wataha.wolves.Where(w => w.Name == "Hatsu").ToList()[0].resistance += skills[wolf1Level + 1].resistance;
                        Resources.Meat -= skills[wolf1Level + 1].costM;
                        Resources.Whitefangs -= skills[wolf1Level + 1].costWF;
                        Resources.Goldfangs -= skills[wolf1Level + 1].costGF;
                        wolf1Level += 2;
                    }
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
