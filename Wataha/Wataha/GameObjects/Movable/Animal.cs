using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameObjects.Movable
{
    public class Animal : GameObject
    {
        public int meat = 10;
        public float walkSpeed = 10;

    

        public Animal(Matrix world, Model model,int meat, float speed) : base(world, model)
        {
            this.meat = meat;
            this.walkSpeed = speed;
        }
    }
}
