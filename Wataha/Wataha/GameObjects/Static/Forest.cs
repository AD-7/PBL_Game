using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.System;

namespace Wataha.GameObjects.Static
{
    class Forest : GameObject
    {
        public Forest(Model model, Matrix world, Camera camera)
        {
            base.model = model;
            base.world = world;
            view = camera.ViewMatrix;
            projection = camera.ProjectionMatrix;
        }

    public override void Draw()
        {
            base.DrawModel(model, view, projection);
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
