using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Wataha.GameObjects.Static
{
    class Plane :GameObject
    {
        public BoundingBox collider;
        public float colliderSize;


        public Plane(Model model, Matrix world, float colliderSize) : base(world,model)
        {
           
            this.colliderSize = colliderSize;
            collider = new BoundingBox(new Vector3(world.Translation.X - colliderSize/2, world.Translation.Y, world.Translation.Z - colliderSize/2), 
                                        new Vector3(world.Translation.X + colliderSize/2, world.Translation.Y, world.Translation.Z + colliderSize/2));
        }

        public override void Draw()
        {
            base.Draw();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
