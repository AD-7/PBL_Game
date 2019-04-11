using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.System
{
   public  class ColisionSystem
    {

        public bool IsCollisionTerrain(BoundingBox player, BoundingBox terrain )
        {
            
               if (player.Intersects(terrain))
                        return true;
               else
             
                        return false;
        }
        public bool IsTreeCollision(BoundingBox player, List<BoundingBox> trees)
        {
            foreach(BoundingBox box in trees)
            {
                if (player.Intersects(box))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
