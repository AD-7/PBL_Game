using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameObjects.Movable
{
    class Animal : GameObject
    {
        public int meat = 10;
        public float walkSpeed = 10;

        public Animal()
        {
        }

        public Animal(int meat, float speed)
        {
            this.meat = meat;
            this.walkSpeed = speed;
        }
    }
}
