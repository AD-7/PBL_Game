using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameObjects.Static;

namespace Wataha.GameSystem
{

    public class HuntingSystem
    {
        

        Camera camera;
        GraphicsDevice device;
        RenderTarget2D renderTarget;
        Plane plane;
        GameObjects.Static.Environment trees;
        Skybox skybox;
        Wataha.GameObjects.Movable.Wataha huntingWataha; // składa się z jednego wilka

        public bool active = false;
        public List<Animal> animals;

        public HuntingSystem(Camera camera, GraphicsDevice device,RenderTarget2D rt, Plane plane, GameObjects.Static.Environment trees,Skybox skybox)
        {
            this.camera = camera;
            this.device = device;
            this.plane = plane;
            this.trees = trees;
            this.skybox = skybox;
            this.renderTarget = rt;
        }


        public void Update()
        {

        }
        public void Draw()
        {



        }



    }
}
