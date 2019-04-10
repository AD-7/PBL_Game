using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameObjects.Static
{
    class Forest : GameObject
    {
        public Forest(Model model,Matrix world  )
        {
            base.model = model;
            base.world = world;
            view = Matrix.CreateLookAt(new Vector3(0, 10, 30), new Vector3(0, 0, -10), Vector3.UnitY);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800 / 480f, 0.1f, 100f);
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
