using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using Wataha.GameSystem;

namespace Wataha.GameObjects
{
    public class MainMenu
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        private Rectangle Cursor;

        private List<Texture2D> ButtonTextures = new List<Texture2D>();

        public float ScreenWidth, ScreenWidthOld;
        public float ScreenHeight, ScreenHeightOld;

        Rectangle BG;
        Rectangle recPlayButton;
        Rectangle recOptionButton;
        Rectangle recCloseButton;

        Rectangle recAudioSliderBG;
        Rectangle recAudioSlider;
        Rectangle recEffectsSliderBG;
        Rectangle recEffectsSlider;
        Rectangle recBackMenuButton;


        int alphaColor = 200;

        Color PlayButtonColor;
        Color OptionButtonColor;
        Color CloseButtonColor;

        Color BackMenuButtonColor;


        SpriteFont font30;
       public  bool inOptions = false;

        float AudioVolume = 0.4f;
        float EffectVolume = 0.3f;

        public MainMenu(SpriteBatch spriteBatch, ContentManager content, GraphicsDevice device)
        {
            this.spriteBatch = spriteBatch;

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/bg"));

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/start"));
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/options"));
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/close"));

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/slider"));
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/slider2"));

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/backMenu"));
            

            this.device = device; 

            PlayButtonColor = new Color(255, 255, 255, alphaColor);
            CloseButtonColor = new Color(255, 255, 255, alphaColor);
            OptionButtonColor = new Color(255, 255, 255, alphaColor);


            font30 = content.Load<SpriteFont>("Fonts/font1");

            ScreenHeightOld = 0;
            ScreenWidthOld = 0;
            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
        }

        public void Update()
        {

                BG.X = 0;
                BG.Y = 0;
                BG.Width = (int)ScreenWidth;
                BG.Height = (int)ScreenHeight;

                if (!inOptions)
                {
                    recPlayButton.X = (int)ScreenWidth / 2 - recPlayButton.Size.X / 2;
                    recPlayButton.Y = (int)ScreenHeight / 4 - recPlayButton.Size.Y / 2;

                    recPlayButton.Height = (int)ScreenHeight / 6;
                    recPlayButton.Width = (int)ScreenWidth / 2;

                    recOptionButton.X = recPlayButton.X;
                    recOptionButton.Y = (int)ScreenHeight / 4 + (int)ScreenHeight / 12 + recOptionButton.Size.Y / 2;

                    recOptionButton.Height = recPlayButton.Height;
                    recOptionButton.Width = recPlayButton.Width;

                    recCloseButton.X = recPlayButton.X;
                    recCloseButton.Y = (int)ScreenHeight / 4 + (int)ScreenHeight / 3 + recCloseButton.Size.Y / 2;

                    recCloseButton.Height = recPlayButton.Height;
                    recCloseButton.Width = recPlayButton.Width;

                    if (OptionButtonsEvents())
                    {
                       inOptions = true;
                        recCloseButton.Width = 0;
                        recOptionButton.Width = 0;
                        recPlayButton.Width = 0;
                    };
                }
                else
                {
                    recBackMenuButton.X = (int)ScreenWidth / 2 - recBackMenuButton.Size.X / 2;
                    recBackMenuButton.Y = (int)ScreenHeight / 4 + (int)ScreenHeight / 3 + recBackMenuButton.Size.Y / 2;

                    recBackMenuButton.Height = (int)ScreenHeight / 6;
                    recBackMenuButton.Width = (int)ScreenWidth / 2;

                    recAudioSliderBG.X = recBackMenuButton.X;
                    recAudioSliderBG.Y = (int)ScreenHeight / 10 + recBackMenuButton.Size.Y / 4;

                    recAudioSliderBG.Height = recBackMenuButton.Height;
                    recAudioSliderBG.Width = recBackMenuButton.Width;

                    recAudioSlider.X = recBackMenuButton.X;
                    recAudioSlider.Y = recAudioSliderBG.Y;

                    recAudioSlider.Height = recBackMenuButton.Height;
                    recAudioSlider.Width = (int)(recAudioSliderBG.Width * AudioVolume);
                    ///////////////////////////////////////
                    recEffectsSliderBG.X = recBackMenuButton.X;
                    recEffectsSliderBG.Y = (int)ScreenHeight / 4 + recEffectsSliderBG.Size.Y / 2;

                    recEffectsSliderBG.Height = recBackMenuButton.Height;
                    recEffectsSliderBG.Width = recBackMenuButton.Width;

                    recEffectsSlider.X = recBackMenuButton.X;
                    recEffectsSlider.Y = recEffectsSliderBG.Y;

                    recEffectsSlider.Height = recBackMenuButton.Height;
                    recEffectsSlider.Width = (int)(recEffectsSliderBG.Width * EffectVolume);

                    if (AudioSliderEvents())
                    {
                        recAudioSlider.Width = InputSystem.mouseState.Position.X - recAudioSliderBG.X;
                        AudioVolume = 1.0f*recAudioSlider.Width / recAudioSliderBG.Width;
                        MediaPlayer.Volume = AudioVolume;

                    }

                    if (EffectSliderEvents())
                    {
                        recEffectsSlider.Width = InputSystem.mouseState.Position.X - recEffectsSliderBG.X;
                        EffectVolume = 1.0f * recEffectsSlider.Width / recEffectsSliderBG.Width;
                        SoundEffect.MasterVolume = EffectVolume;
                    }

                    if (BackMenuButtonsEvents())
                    {
                        inOptions = false;
                    };
                }


            UpdateCursorPosition();


            ScreenHeightOld = ScreenHeight;
            ScreenWidthOld = ScreenWidth;

            ScreenWidth = device.Viewport.Width;
            ScreenHeight = device.Viewport.Height;
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(ButtonTextures[0], BG, Color.White);
            if (!inOptions)
            {
                spriteBatch.Draw(ButtonTextures[1], recPlayButton, PlayButtonColor);
                spriteBatch.Draw(ButtonTextures[2], recOptionButton, OptionButtonColor);
                spriteBatch.Draw(ButtonTextures[3], recCloseButton, CloseButtonColor);
            }
            else
            {
                //Audio Slider
                spriteBatch.DrawString(font30, "Audio Volume", new Vector2(100,100), Color.Fuchsia);
                spriteBatch.DrawString(font30, "Effects Volume", new Vector2(100,300), Color.DarkKhaki);

                spriteBatch.Draw(ButtonTextures[4], recAudioSliderBG, Color.White);  
                spriteBatch.Draw(ButtonTextures[5], recAudioSlider, Color.White);

                spriteBatch.Draw(ButtonTextures[4], recEffectsSliderBG, Color.White);
                spriteBatch.Draw(ButtonTextures[5], recEffectsSlider, Color.White);

                spriteBatch.Draw(ButtonTextures[6], recBackMenuButton, BackMenuButtonColor);
            }



            spriteBatch.End();
        }

        private void UpdateCursorPosition()
        {
            InputSystem.mouseStateOld = InputSystem.mouseState;

            /* Update Cursor position by Mouse */
            InputSystem.mouseState = Mouse.GetState();
            Cursor.X = InputSystem.mouseState.X; Cursor.Y = InputSystem.mouseState.Y;
        }

        public bool PlayButtonsEvents()
        {
            if ((recPlayButton.Intersects(Cursor)))
            {
                PlayButtonColor = new Color(0, 128, 0, alphaColor);
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
                PlayButtonColor = new Color(255, 255, 255, alphaColor);
            return false;
        }

        public bool OptionButtonsEvents()
        {
            if ((recOptionButton.Intersects(Cursor)))
            {
                OptionButtonColor = new Color(0, 128, 0, alphaColor);
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
                OptionButtonColor = new Color(255, 255, 255, alphaColor);
            return false;
        }

        public bool ExitButtonsEvents()
        {
            if ((recCloseButton.Intersects(Cursor)))
            {
                CloseButtonColor = new Color(0, 128, 0, alphaColor);
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
                CloseButtonColor = new Color(255, 255, 255, alphaColor);
            return false;
        }

        public bool BackMenuButtonsEvents()
        {
            if ((recBackMenuButton.Intersects(Cursor)))
            {
                BackMenuButtonColor = new Color(0, 128, 0, alphaColor);
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
                BackMenuButtonColor = new Color(255, 255, 255, alphaColor);
            return false;
        }



        public bool AudioSliderEvents()
        {
            if ((recAudioSliderBG.Intersects(Cursor)))
            {
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool EffectSliderEvents()
        {
            if ((recEffectsSliderBG.Intersects(Cursor)))
            {
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            return false;
        }


    }
}
