using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
    class AnimationSystem
    {
        private Model model;
        public AnimationSystem() {
            List<Model> animation = new List<Model>();
        }
        public AnimationSystem(Model model)
        {
            this.model = model;
        }

    }
}
