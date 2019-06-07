using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Wataha.GameSystem.Interfejs
{
    public class Intro
    {
        private SpriteBatch spriteBatch;

        private Texture2D story;
        private Texture2D bg;
        private int Timer;
        private int StartTime = 2;

        public int ScreenWidth;
        public int ScreenHeight;

        Rectangle recIntro;

        public Intro(SpriteBatch spriteBatch, ContentManager content, int screenWidth, int screenHeight)
        {
            this.spriteBatch = spriteBatch;
            this.story = content.Load<Texture2D>("MainMenu/story");
            this.bg = content.Load<Texture2D>("MainMenu/bg2");
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            recIntro.Y = (int)(ScreenHeight * 0.9);
            recIntro.X = (int)(ScreenWidth * 0.05);

            recIntro.Width = (int)(ScreenWidth * 0.9);
            recIntro.Height = (int)(ScreenHeight);

            Timer = StartTime;
        }

        public void Update(GameTime gameTime)
        {
            if (Timer <= 0)
            {
                recIntro.Y -= 1;
                Timer = StartTime;
            }
            Timer--;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(bg, new Rectangle(0,0,ScreenWidth,ScreenHeight), Color.White);
            spriteBatch.Draw(story, recIntro, Color.White);
            spriteBatch.End();
        }

        public bool IntroEvent()
        {
            if (recIntro.Y <= -recIntro.Height * 0.8)
                return true;
            return false;
        }
    }
}
