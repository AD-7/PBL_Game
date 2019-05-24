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

namespace Wataha.GameSystem.Interfejs
{
    public class MainMenu
    {
        private SpriteBatch spriteBatch;
        private GraphicsDevice device;
        private Rectangle Cursor;

        private List<Texture2D> ButtonTextures = new List<Texture2D>();
        private Texture2D title;
        private Texture2D story;
        private Texture2D newGameActual, loadActual, optionsActual, exitActual, authorsActual, aboutActual;

        public int ScreenWidth, ScreenWidthOld;
        public int ScreenHeight, ScreenHeightOld;

        Rectangle BG;
        Rectangle recButtonsBkg;
        Rectangle recStory;
        Rectangle recNewGameButton;
        Rectangle recOptionButton;
        Rectangle recExitButton;
        Rectangle recAuthorsButton;
        Rectangle recLoadButton;
        Rectangle recAboutButton;

        Rectangle recAudioSliderBG;
        Rectangle recAudioSlider;
        Rectangle recEffectsSliderBG;
        Rectangle recEffectsSlider;
        Rectangle recAudioMute;
        Rectangle recEffectMute;
        Rectangle recAudioVolume;
        Rectangle recEffectsVolume;
        Rectangle recTitle;
        Rectangle recAuthors;

        int alphaColor = 200;




        Color AudioColor;
        Color EffectColor;


        SpriteFont font30;
        public bool inOptions = false;
        public bool ifStory = false;

        float AudioVolume = 0.4f;
        float EffectVolume = 0.3f;
        private bool ifAuthors;

        public MainMenu(SpriteBatch spriteBatch, ContentManager content, GraphicsDevice device)
        {
            this.spriteBatch = spriteBatch;

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/bg")); //0
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/buttonsBkg"));//1
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/newGame")); //2
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/newGame2")); //3

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/options")); //4
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/options2")); //5
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/exit"));  //6
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/exit2"));  //7
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/authors"));  //8
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/authors2"));  //9
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/load"));  //10
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/load2"));  //11
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/about")); //12
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/about2")); //13

            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/slider"));  //14
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/slider2"));  //15



            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/Mute")); //16
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/notMute")); //17
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/audioVolume")); //18
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/effectVolume")); //19
            ButtonTextures.Add(content.Load<Texture2D>("MainMenu/authorsss")); //20
            title = content.Load<Texture2D>("MainMenu/title");
            story = content.Load<Texture2D>("MainMenu/story");
            this.device = device;

            newGameActual = ButtonTextures[2];
            optionsActual = ButtonTextures[4];
            exitActual = ButtonTextures[6];
            authorsActual = ButtonTextures[8];
            loadActual = ButtonTextures[10];
            aboutActual = ButtonTextures[12];

            AudioColor = new Color(255, 255, 255, alphaColor);
            EffectColor = new Color(255, 255, 255, alphaColor);

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

            recTitle.X = (ScreenWidth / 6) * 2;
            recTitle.Y = ScreenHeight / 12;
            recTitle.Height = ScreenHeight / 5;
            recTitle.Width = (ScreenWidth / 6) * 2;

            recButtonsBkg.X = ScreenWidth / 12;
            recButtonsBkg.Y = ScreenHeight / 3;
            recButtonsBkg.Width = (ScreenWidth / 12) * 10;
            recButtonsBkg.Height = (ScreenHeight / 100) * 60;

            recNewGameButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recNewGameButton.Y = recButtonsBkg.Y + recButtonsBkg.Height / 6;
            recNewGameButton.Width = recButtonsBkg.Width / 8;
            recNewGameButton.Height = recButtonsBkg.Height / 9;

            recLoadButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recLoadButton.Y = recNewGameButton.Y + recButtonsBkg.Height / 8;
            recLoadButton.Width = recButtonsBkg.Width / 10;
            recLoadButton.Height = recButtonsBkg.Height / 9;

            recOptionButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recOptionButton.Y = recLoadButton.Y + recButtonsBkg.Height / 8;
            recOptionButton.Width = recButtonsBkg.Width / 9;
            recOptionButton.Height = recButtonsBkg.Height / 9;

            recAboutButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recAboutButton.Y = recOptionButton.Y + recButtonsBkg.Height / 8;
            recAboutButton.Width = recButtonsBkg.Width / 8;
            recAboutButton.Height = recButtonsBkg.Height / 9;

            recAuthorsButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recAuthorsButton.Y = recAboutButton.Y + recButtonsBkg.Height / 8;
            recAuthorsButton.Width = recButtonsBkg.Width / 9;
            recAuthorsButton.Height = recButtonsBkg.Height / 9;

            recExitButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
            recExitButton.Y = recAuthorsButton.Y + recButtonsBkg.Height / 8;
            recExitButton.Width = recButtonsBkg.Width / 12;
            recExitButton.Height = recButtonsBkg.Height / 9;

            recStory.X = recButtonsBkg.X + (recButtonsBkg.Width / 100) * 43;
            recStory.Y = recNewGameButton.Y - recButtonsBkg.Height / 16;
            recStory.Width = recButtonsBkg.Width / 2;
            recStory.Height = (recButtonsBkg.Height / 100) * 80;

            recAudioSliderBG.X = recStory.X + recStory.Width / 10;
            recAudioSliderBG.Y = recStory.Y + recStory.Height / 18;
            recAudioSliderBG.Height = recButtonsBkg.Height / 14;
            recAudioSliderBG.Width = recButtonsBkg.Width / 2;

            recAudioSlider.X = recAudioSliderBG.X;
            recAudioSlider.Y = recAudioSliderBG.Y;
            recAudioSlider.Height = recButtonsBkg.Height / 14;
            recAudioSlider.Width = (int)(recAudioSliderBG.Width * AudioVolume);



            recEffectsSliderBG.X = recAudioSliderBG.X;
            recEffectsSliderBG.Y = recAudioSliderBG.Y + recStory.Height / 5;
            recEffectsSliderBG.Height = recAudioSliderBG.Height;
            recEffectsSliderBG.Width = recAudioSliderBG.Width;

            recEffectsSlider.X = recEffectsSliderBG.X;
            recEffectsSlider.Y = recEffectsSliderBG.Y;
            recEffectsSlider.Height = recEffectsSliderBG.Height;
            recEffectsSlider.Width = (int)(recEffectsSliderBG.Width * EffectVolume);


            recAudioMute.X = recAudioSliderBG.X - recAudioSliderBG.Width / 16;
            recAudioMute.Y = recAudioSliderBG.Y;
            recAudioMute.Height = recAudioSlider.Height;
            recAudioMute.Width = recAudioMute.Height;

            recEffectMute.X = recEffectsSliderBG.X - recEffectsSliderBG.Width / 16;
            recEffectMute.Y = recEffectsSliderBG.Y;
            recEffectMute.Height = recEffectsSliderBG.Height;
            recEffectMute.Width = recEffectMute.Height;

            recAudioVolume.X = recAudioMute.X - recAudioMute.Width * 5;
            recAudioVolume.Y = recAudioMute.Y + recAudioMute.Height / 4;
            recAudioVolume.Width = recAudioSliderBG.Width / 5;
            recAudioVolume.Height = recAudioSliderBG.Height / 2;

            recEffectsVolume.X = recEffectMute.X - recEffectMute.Width * 5;
            recEffectsVolume.Y = recEffectMute.Y + recEffectMute.Height / 4;
            recEffectsVolume.Width = recEffectsSliderBG.Width / 5;
            recEffectsVolume.Height = recEffectsSliderBG.Height / 2;

            recAuthors.X = recButtonsBkg.X + (recButtonsBkg.Width/10)*4;
            recAuthors.Y = recButtonsBkg.Y+ recButtonsBkg.Height/7;
            recAuthors.Width = recButtonsBkg.Width / 2;
            recAuthors.Height = recButtonsBkg.Height / 2;


            if (AuthorsButtonEvent() && !ifAuthors)
            {
                inOptions = false;
                ifStory = false;
                ifAuthors = true;
            }


            if (AboutButtonEvent() && !ifStory)
            {
                ifAuthors = false;
                inOptions = false;
                ifStory = true;
            }
            if (OptionButtonEvent() && !inOptions)
            {
                ifAuthors = false;
                ifStory = false;
                inOptions = true;
            }

            if (inOptions)
            {
                if (AudioSliderEvents())
                {
                    recAudioSlider.Width = InputSystem.mouseState.Position.X - recAudioSliderBG.X;
                    AudioVolume = 1.0f * recAudioSlider.Width / recAudioSliderBG.Width;
                    MediaPlayer.Volume = AudioVolume;
                }
                if (EffectSliderEvents())
                {
                    recEffectsSlider.Width = InputSystem.mouseState.Position.X - recEffectsSliderBG.X;
                    EffectVolume = 1.0f * recEffectsSlider.Width / recEffectsSliderBG.Width;
                    if (AudioSystem.effectEnable)
                    {
                        SoundEffect.MasterVolume = EffectVolume;
                        AudioSystem.growl[0].Play();
                    }
                }
                if (AudioCheckboxEvents())
                {
                    AudioSystem.audioEnable = !AudioSystem.audioEnable;
                    MediaPlayer.IsMuted = !AudioSystem.audioEnable;
                }

                if (EffectCheckboxEvents())
                {
                    AudioSystem.effectEnable = !AudioSystem.effectEnable;
                    if (AudioSystem.effectEnable)
                        SoundEffect.MasterVolume = EffectVolume;
                    else
                        SoundEffect.MasterVolume = 0;
                }
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
            spriteBatch.Draw(ButtonTextures[1], recButtonsBkg, Color.White);
            spriteBatch.Draw(newGameActual, recNewGameButton, Color.White);
            spriteBatch.Draw(loadActual, recLoadButton, Color.White);
            spriteBatch.Draw(optionsActual, recOptionButton, Color.White);
            spriteBatch.Draw(authorsActual, recAuthorsButton, Color.White);
            spriteBatch.Draw(exitActual, recExitButton, Color.White);
            spriteBatch.Draw(aboutActual, recAboutButton, Color.White);

            if (inOptions)
            {
                spriteBatch.Draw(ButtonTextures[14], recAudioSliderBG, Color.White);
                spriteBatch.Draw(ButtonTextures[15], recAudioSlider, Color.White);

                spriteBatch.Draw(ButtonTextures[14], recEffectsSliderBG, Color.White);
                spriteBatch.Draw(ButtonTextures[15], recEffectsSlider, Color.White);
                spriteBatch.Draw(ButtonTextures[18], recAudioVolume, Color.White);
                spriteBatch.Draw(ButtonTextures[19], recEffectsVolume, Color.White);
                if (AudioSystem.audioEnable)
                    spriteBatch.Draw(ButtonTextures[16], recAudioMute, Color.White);
                else
                    spriteBatch.Draw(ButtonTextures[17], recAudioMute, Color.White);

                if (AudioSystem.effectEnable)
                    spriteBatch.Draw(ButtonTextures[16], recEffectMute, Color.White);
                else
                    spriteBatch.Draw(ButtonTextures[17], recEffectMute, Color.White);
            }




            if (ifStory)
            {
                spriteBatch.Draw(story, recStory, Color.White);
            }

            spriteBatch.Draw(title, recTitle, Color.White);

            if (ifAuthors)
            {
                spriteBatch.Draw(ButtonTextures[20], recAuthors, Color.White);
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



        public bool NewGameButtonEvent()
        {
            if ((recNewGameButton.Intersects(Cursor)))
            {

                newGameActual = ButtonTextures[3];
                recNewGameButton.Width = (int)(recNewGameButton.Width * 1.2);
                recNewGameButton.Height = (int)(recNewGameButton.Height * 1.2);
                recNewGameButton.X = recButtonsBkg.X + recButtonsBkg.Width / 16;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
            {
                newGameActual = ButtonTextures[2];
                recNewGameButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
                recNewGameButton.Y = recButtonsBkg.Y + recButtonsBkg.Height / 6;
                recNewGameButton.Width = recButtonsBkg.Width / 8;
                recNewGameButton.Height = recButtonsBkg.Height / 9;
            }
            return false;
        }

        public bool LoadButtonEvent()
        {
            if (recLoadButton.Intersects(Cursor))
            {
                loadActual = ButtonTextures[11];
                recLoadButton.Width = (int)(recLoadButton.Width * 1.2);
                recLoadButton.Height = (int)(recLoadButton.Height * 1.2);
                recLoadButton.X = recButtonsBkg.X + recButtonsBkg.Width / 13;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
            {
                loadActual = ButtonTextures[10];
                recLoadButton.X = recButtonsBkg.X + recButtonsBkg.Width / 11;
                recLoadButton.Y = recNewGameButton.Y + recButtonsBkg.Height / 8;
                recLoadButton.Width = recButtonsBkg.Width / 10;
                recLoadButton.Height = recButtonsBkg.Height / 9;
            }
            return false;
        }


        public bool OptionButtonEvent()
        {
            if ((recOptionButton.Intersects(Cursor)))
            {
                optionsActual = ButtonTextures[5];
                recOptionButton.Width = (int)(recOptionButton.Width * 1.2);
                recOptionButton.Height = (int)(recOptionButton.Height * 1.2);
                recOptionButton.X = recButtonsBkg.X + recButtonsBkg.Width / 13;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {

                    return true;
                }
                return false;
            }
            else
            {
                optionsActual = ButtonTextures[4];
                recOptionButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
                recOptionButton.Y = recLoadButton.Y + recButtonsBkg.Height / 8;
                recOptionButton.Width = recButtonsBkg.Width / 9;
                recOptionButton.Height = recButtonsBkg.Height / 9;
            }

            return false;
        }

        public bool AboutButtonEvent()
        {
            if ((recAboutButton.Intersects(Cursor)))
            {
                aboutActual = ButtonTextures[13];
                recAboutButton.Width = (int)(recAboutButton.Width * 1.2);
                recAboutButton.Height = (int)(recAboutButton.Height * 1.2);
                recAboutButton.X = recButtonsBkg.X + recButtonsBkg.Width / 16;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {


                    return true;
                }
                return false;
            }
            else
            {
                aboutActual = ButtonTextures[12];
                recAboutButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
                recAboutButton.Y = recOptionButton.Y + recButtonsBkg.Height / 8;
                recAboutButton.Width = recButtonsBkg.Width / 8;
                recAboutButton.Height = recButtonsBkg.Height / 9;
            }
            return false;
        }

        public bool AuthorsButtonEvent()
        {
            if ((recAuthorsButton.Intersects(Cursor)))
            {
                authorsActual = ButtonTextures[9];
                recAuthorsButton.Width = (int)(recAuthorsButton.Width * 1.2);
                recAuthorsButton.Height = (int)(recAuthorsButton.Height * 1.2);
                recAuthorsButton.X = recButtonsBkg.X + recButtonsBkg.Width / 13;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
            {
                authorsActual = ButtonTextures[8];
                recAuthorsButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
                recAuthorsButton.Y = recAboutButton.Y + recButtonsBkg.Height / 8;
                recAuthorsButton.Width = recButtonsBkg.Width / 9;
                recAuthorsButton.Height = recButtonsBkg.Height / 9;
            }
            return false;
        }

        public bool ExitButtonEvent()
        {
            if ((recExitButton.Intersects(Cursor)))
            {
                exitActual = ButtonTextures[7];
                recExitButton.Width = (int)(recExitButton.Width * 1.2);
                recExitButton.Height = (int)(recExitButton.Height * 1.2);
                recExitButton.X = recButtonsBkg.X + recButtonsBkg.Width / 12;
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            else
            {
                exitActual = ButtonTextures[6];
                recExitButton.X = recButtonsBkg.X + recButtonsBkg.Width / 10;
                recExitButton.Y = recAuthorsButton.Y + recButtonsBkg.Height / 8;
                recExitButton.Width = recButtonsBkg.Width / 12;
                recExitButton.Height = recButtonsBkg.Height / 9;
            }
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

        public bool AudioCheckboxEvents()
        {
            if ((recAudioMute.Intersects(Cursor)))
            {
                if (InputSystem.mouseState.LeftButton == ButtonState.Pressed && InputSystem.mouseStateOld != InputSystem.mouseState)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public bool EffectCheckboxEvents()
        {
            if ((recEffectMute.Intersects(Cursor)))
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
