using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wataha.GameObjects.Interable
{
    public class InterableObject : GameObject
    {
        public InterableObject(Matrix world, Model model) : base(world, model)
        {
        }
    }
}
