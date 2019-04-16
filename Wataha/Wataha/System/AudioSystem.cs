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
        Random rnd = new Random();

        ContentManager Content;

        public AudioSystem(ContentManager content)
        {
            Content = content;
            songList = new List<Song>();
            soundEffects = new List<SoundEffect>();

            songList.Add(Content.Load<Song>("Songs/forest (1)"));
            songList.Add(Content.Load<Song>("Songs/forest"));

            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl6"));
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl5"));
            soundEffects.Add(Content.Load<SoundEffect>("SoundEffects/growl4"));

            SoundEffect.MasterVolume = 0.3f;

            MediaPlayer.Play(songList[0]);
            //  Uncomment the following line will also loop the song
            //MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume = 0.2f;
            if (MediaPlayer.State != MediaState.Playing && MediaPlayer.PlayPosition.TotalSeconds == 0.0f)
            {
                MediaPlayer.Play(songList[rnd.Next(songList.Count)]);
            }
        }
    }
}
