using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wataha.GameObjects;

namespace Wataha.System
{
    class GameSystem
    {
        public List<GameObject> objects;


        public void Draw()
        {
            foreach(GameObject gameObject in objects)
            {
                gameObject.Draw();
            }
        }

        public void Update()
        {
            foreach (GameObject gameObject in objects)
            {
                gameObject.Update();
            }
        }

    }
}
