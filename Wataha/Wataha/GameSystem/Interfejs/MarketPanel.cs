using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem.Interfejs
{
    public class MarketPanel
    {
        Texture2D marketPanel;
        Texture2D pressInfo;
        Texture2D exitButton, exitButton2;
        Texture2D buyButton, buyButton2;

        Rectangle recMarketPanel;
        Rectangle recInteract;
        Rectangle recExit;
        Rectangle recBuy1;
        Rectangle recBuy2;

        Texture2D currentBuy1, currentBuy2;
        Texture2D currentExit;

        public bool active = false;
        public bool infoActive = false;

        public MarketPanel(ContentManager manager)
        {
            marketPanel = manager.Load<Texture2D>("Pictures/market");
            pressInfo = manager.Load<Texture2D>("Pictures/QuestPanel/pressFInfo");
            exitButton = manager.Load<Texture2D>("Pictures/exitMarketButton");
            exitButton2 = manager.Load<Texture2D>("Pictures/exitMarketButton2");
            buyButton = manager.Load<Texture2D>("Pictures/buyButton");
            buyButton2 = manager.Load<Texture2D>("Pictures/buyButton2");

            currentBuy1 = buyButton;
            currentBuy2 = buyButton;
            currentExit = exitButton;
        }

        public void Update(int width, int height)
        {
            recMarketPanel.X = (int)(width * 0.35);
            recMarketPanel.Y = (int)(height * 0.3);
            recMarketPanel.Width = (int)(width * 0.3);
            recMarketPanel.Height = (int)(height * 0.3);

            recInteract.X = (int)(width * 0.5) - (int)(width * 0.1);
            recInteract.Y = (int)(height * 0.20);
            recInteract.Width = (int)(width * 0.2);
            recInteract.Height = (int)(height * 0.1);

            recBuy1.X = recMarketPanel.X + recMarketPanel.Width / 5;
            recBuy1.Y = recMarketPanel.Y + (recMarketPanel.Height / 100) * 62;
            recBuy1.Width = recMarketPanel.Width / 7;
            recBuy1.Height = recMarketPanel.Width / 13;

            recBuy2.X = recBuy1.X + (recMarketPanel.Width / 100) * 48;
            recBuy2.Y = recBuy1.Y;
            recBuy2.Width = recBuy1.Width;
            recBuy2.Height = recBuy1.Height;

            recExit.X = (recBuy1.X + recBuy2.X) / 2;
            recExit.Y = recBuy1.Y + recMarketPanel.Height / 5;
            recExit.Width = recBuy1.Width;
            recExit.Height = recBuy1.Height;
        }

        public void DrawInfo(SpriteBatch spriteBatch)
        {
            if (infoActive && !active)
            {
                spriteBatch.Draw(pressInfo, recInteract, Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(marketPanel, recMarketPanel, Color.White);
                spriteBatch.Draw(currentBuy1, recBuy1, Color.White);
                spriteBatch.Draw(currentBuy2, recBuy2, Color.White);
                spriteBatch.Draw(currentExit, recExit, Color.White);
            }

        }


        public bool Buy1ButtonEvent()
        {
            if (recBuy1.Intersects(InputSystem.Cursor))
            {
                currentBuy1 = buyButton2;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
      
                return false;
            }
            else
                currentBuy1 = buyButton;
            return false;
        }

        public bool Buy2ButtonEvent()
        {
            if (recBuy2.Intersects(InputSystem.Cursor))
            {
                currentBuy2 = buyButton2;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
            
                return false;

            }
            else
                currentBuy2 = buyButton;
            return false;
        }

        public bool ExitButtonEvent()
        {
            if (recExit.Intersects(InputSystem.Cursor))
            {
                currentExit = exitButton2;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }

                return false;

            }
            else
                currentExit = exitButton;
            return false;
        }



        public bool CheckIfWolfIsClose(Wolf wolf, Wataha.GameObjects.Static.Environment market)
        {
            if (Vector3.Distance(wolf.position, market.model.Meshes[0].BoundingSphere.Center) < 10.0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





    }
}
