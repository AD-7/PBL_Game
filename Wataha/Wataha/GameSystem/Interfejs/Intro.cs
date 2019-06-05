using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem.Interfejs
{
    public class Intro
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;

        private Texture2D story;
        private Texture2D bg;
        private Texture2D storybg;
        private int Timer;

        public int ScreenWidth;
        public int ScreenHeight;

        Rectangle recIntro;

        public Intro(SpriteBatch spriteBatch, GraphicsDevice device, ContentManager content, int screenWidth, int screenHeight)
        {
            this.spriteBatch = spriteBatch;
            this.device = device;
            this.story = content.Load<Texture2D>("MainMenu/story");
            this.bg = content.Load<Texture2D>("MainMenu/bg2");
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            recIntro.Y = (int)(ScreenHeight * 0.9);
            recIntro.X = (int)(ScreenWidth * 0.05);

            recIntro.Width = (int)(ScreenWidth * 0.9);
            recIntro.Height = (int)(ScreenHeight);

            Timer = 3;
        }

        public void Update(GameTime gameTime)
        {
            if (Timer <= 0)
            {
                recIntro.Y -= 1;
                Timer = 3;
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
