using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace Wataha.GameSystem
{
    class AudioSystem
    {
        public List<Song> songList;
        public List<SoundEffect> soundEffects;
        public List<SoundEffectInstance> growl;
        Random rnd = new Random();
        int i = 0;

        ContentManager Content;
  


        public AudioSystem(ContentManager content)
        {
            Content = content;
            songList = new List<Song>();
            soundEffects = new List<SoundEffect>();
            growl = new List<SoundEffectInstance>();

            songList.Add(Content.Load<Song>("Songs/forest (1)"));
            songList.Add(Content.Load<Song>("Songs/Forest3"));
            songList.Add(Content.Load<Song>("Songs/forest"));
         

            MediaPlayer.Volume = 0.4f;
            MediaPlayer.Play(songList[i]);
            //  Uncomment the following line will also loop the song
            //MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl6"));
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl5"));
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl4"));

            growl.Add(soundEffects[0].CreateInstance());
            growl.Add(soundEffects[1].CreateInstance());
            growl.Add(soundEffects[2].CreateInstance());

            SoundEffect.MasterVolume = 0.3f;
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            i++;
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.4f;
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.PlayPosition.TotalSeconds == 0.0f)
            {
                if (i == songList.Count)
                    i = 0;
                MediaPlayer.Play(songList[i]);

            }
        }

        public void playGrowl(int i)
        {
            if (!growl[i].State.Equals(SoundState.Playing))
                growl[i].Play();
        }


    }
}
