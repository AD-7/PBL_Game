using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects;

namespace Wataha.GameSystem.Animation
{
    public class AnimationSystem
    {
        public Animation animation;
        public GameObject WhoAmI; 
        private float timer;
        public Vector3 position { get; set; }
        public Effect effect;

        public AnimationSystem(Animation animation,GameObject me)
        {
            this.animation = animation;
            WhoAmI = me;
        }
        public void Play(Animation animation)
        {
            if(this.animation == animation)
            {
                return;
            }
            this.animation = animation;
            this.animation.CurrentFrame = 0;
            timer = 0;
        }
        public void Stop()
        {
            this.animation.CurrentFrame = 0;
            timer = 0;
        }
        public void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(timer > animation.frameSpeed)
            {
                timer -= animation.frameSpeed;
                animation.CurrentFrame++;
                if(animation.CurrentFrame >= animation.NumberOfFrames)
                {
                    animation.CurrentFrame = 0;
                }
            }
            Draw();

        }
        public void Draw()
        {
            WhoAmI.model = animation.animation[animation.CurrentFrame];
            
        }

    }
}
