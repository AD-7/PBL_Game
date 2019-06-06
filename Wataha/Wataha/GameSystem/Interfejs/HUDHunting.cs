using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Wataha.GameSystem.Interfejs
{
    public class HUDHunting
    {

        SpriteBatch spriteBatch;
        GraphicsDevice device;
        ContentManager Content;
        public int screenWidth, screenHeight, screenWidthOld, screenHeightOld;

        Texture2D infoHuntingWindow;
        Texture2D yesButton;
        Texture2D okButton;
        Texture2D endHuntingWindow;
        Texture2D endWindow;
        Texture2D clock;
        SpriteFont timerFont, infoHuntingFont, infoHuntingFontSmall;

        Rectangle recInfoHuntingWindow;
        Rectangle recYesButton;
        Rectangle recOkButton;
        Rectangle recInfoWindow;
        Rectangle recClock;

        Color yesButtonColor = Color.Gray;
        Color okButtonColor = Color.Gray;
        Color clockColor = Color.White;

        public double seconds = 0;
        public int maxMeat = 0;
        public int huntedMeat = 0;
        public int energyLoss = 0;


        public bool ifInfoHuntingWindow = true;
        public bool ifEndHuntingWindow = false;



        public HUDHunting(SpriteBatch spriteBatch, GraphicsDevice device, ContentManager content)
        {
            this.spriteBatch = spriteBatch;
            this.device = device;
            Content = content;
            screenHeight = device.Viewport.Height;
            screenWidth = device.Viewport.Width;
            screenWidthOld = 0;
            screenHeightOld = 0;

            infoHuntingWindow = Content.Load<Texture2D>("Pictures/Hunting/infoHuntingWindow");
            yesButton = Content.Load<Texture2D>("Pictures/Hunting/yesButton");
            okButton = Content.Load<Texture2D>("Pictures/Hunting/okButton");
            endHuntingWindow = Content.Load<Texture2D>("Pictures/Hunting/endHuntingWindow");
            endWindow = Content.Load<Texture2D>("Pictures/Hunting/endWindow");

            timerFont = Content.Load<SpriteFont>("Fonts/timerFont");
            infoHuntingFont = Content.Load<SpriteFont>("Fonts/infoHuntingFont");
            infoHuntingFontSmall = Content.Load<SpriteFont>("Fonts/infoHuntingFontSmall");
            clock = Content.Load<Texture2D>("Pictures/zegar");

        }

        public void Update(GameTime gameTime)
        {
            if (screenWidth != screenWidthOld || screenHeight != screenHeightOld)
            {
                recInfoHuntingWindow.X = (int)(screenWidth * 0.25);
                recInfoHuntingWindow.Y = (int)(screenHeight * 0.20);
                recInfoHuntingWindow.Width = (int)(screenWidth * 0.5);
                recInfoHuntingWindow.Height = (int)(screenHeight * 0.5);

                recYesButton.X = (int)(recInfoHuntingWindow.X + recInfoHuntingWindow.Width * 0.48);
                recYesButton.Y = (int)(recInfoHuntingWindow.Y + recInfoHuntingWindow.Height - recInfoHuntingWindow.Height * 0.125);
                recYesButton.Width = (int)(recInfoHuntingWindow.Width * 0.1);
                recYesButton.Height = (int)(recInfoHuntingWindow.Height * 0.083);

                recOkButton.X = (int)(recInfoHuntingWindow.X + recInfoHuntingWindow.Width * 0.48);
                recOkButton.Y = (int)(recInfoHuntingWindow.Y + recInfoHuntingWindow.Height - recInfoHuntingWindow.Height * 0.125);
                recOkButton.Width = (int)(recInfoHuntingWindow.Width * 0.1);
                recOkButton.Height = (int)(recInfoHuntingWindow.Height * 0.083);

                recInfoWindow.X = (int)(screenWidth * 0.417);
                recInfoWindow.Y = (int)(screenHeight * 0.05);
                recInfoWindow.Width = (int)(screenWidth * 0.17);
                recInfoWindow.Height = (int)(screenHeight * 0.1);

                recClock.X = (int)(screenWidth * 0.49);
                recClock.Y = (int)(screenHeight * 0.85);
                recClock.Width = (int)(screenHeight * 0.05);
                recClock.Height = recClock.Width;


            }
            InputSystem.UpdateCursorPosition();
            screenHeightOld = screenHeight;
            screenWidthOld = screenWidth;
            screenWidth = device.Viewport.Width;
            screenHeight = device.Viewport.Height;

            yesButtonEvent();

            if (!ifInfoHuntingWindow)
            {
                if (seconds <= 0)
                {
                    seconds = 0;
                    ifEndHuntingWindow = true;

                }
                else
                {
                    seconds -= gameTime.ElapsedGameTime.TotalMilliseconds * 0.001;
                }

                if (seconds <= 6)
                {
                    clockColor = Color.Red;
                }
                else if (seconds > 6)
                {
                    clockColor = Color.White;
                }

            }

        }

        public void Draw()
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            if (ifInfoHuntingWindow)
            {
                spriteBatch.Draw(infoHuntingWindow, recInfoHuntingWindow, Color.White);
                spriteBatch.Draw(yesButton, recYesButton, yesButtonColor);
            }

            else
            {
                if (ifEndHuntingWindow)
                {
                    spriteBatch.Draw(endWindow, recInfoHuntingWindow, Color.White);
                    spriteBatch.Draw(okButton, recOkButton, okButtonColor);

                    spriteBatch.DrawString(infoHuntingFontSmall, "meat hunted : " + huntedMeat.ToString(), new Vector2(recInfoHuntingWindow.X + (recInfoHuntingWindow.Width / 100) * 45, recInfoHuntingWindow.Y + recInfoHuntingWindow.Height / 4), Color.White);
                    spriteBatch.DrawString(infoHuntingFontSmall, "energy loss : " + energyLoss.ToString(), new Vector2(recInfoHuntingWindow.X + (recInfoHuntingWindow.Width / 100) * 45, recInfoHuntingWindow.Y + recInfoHuntingWindow.Height / 4 + recInfoHuntingWindow.Height / 8), Color.White);
                }

                spriteBatch.Draw(clock, recClock, clockColor);
                spriteBatch.DrawString(timerFont, seconds.ToString(" 0.# s"), new Vector2((screenWidth / 100) * 48, (screenHeight / 100) * 78), Color.Red);
                spriteBatch.Draw(endHuntingWindow, recInfoWindow, Color.White);
                spriteBatch.DrawString(infoHuntingFont, "Max meat: " + maxMeat.ToString(), new Vector2(recInfoWindow.X + recInfoWindow.Width / 12, recInfoWindow.Y + recInfoWindow.Height / 24), Color.Orange);
                spriteBatch.DrawString(infoHuntingFont, "Meat hunted: " + huntedMeat.ToString(), new Vector2(recInfoWindow.X + recInfoWindow.Width / 12, recInfoWindow.Y + recInfoWindow.Height / 2 + recInfoWindow.Height / 24), Color.Orange);
            }

            spriteBatch.End();
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            device.SamplerStates[0] = SamplerState.LinearWrap;
        }


        public bool yesButtonEvent()
        {
            if (recYesButton.Intersects(InputSystem.Cursor))
            {
                yesButtonColor = Color.White;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {
                    ifInfoHuntingWindow = false;
                    return true;

                }
                return false;
            }
            yesButtonColor = Color.Gray;
            return false;
        }

        public bool okButtonEvent()
        {
            if (recOkButton.Intersects(InputSystem.Cursor))
            {
                okButtonColor = Color.White;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld.LeftButton == ButtonState.Released)
                {

                    return true;

                }
                return false;
            }
            okButtonColor = Color.Gray;
            return false;
        }
    }
}
