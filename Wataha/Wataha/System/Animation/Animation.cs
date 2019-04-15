using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Wataha.System.Animation
{
    class Animation
    {
        public int CurrentFrame { get; set; }
        public bool IsLooping { get; set; }
        public Dictionary<String,Model> animation { get; private set; }
        public float frameSpeed { get; set; }
        public ContentManager content { get; set; }

        public Animation(ContentManager content,String animationfolder)
        {
            this.content = content;
            Model animationFrame;
            animationFrame = content.Load<Model>(animationfolder+"");
            frameSpeed = 0.2f;
        }
    }
}
