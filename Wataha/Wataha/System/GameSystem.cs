﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Wataha.GameObjects;
using Wataha.GameObjects.Static;

namespace Wataha.System
{
    class GameSystem
    {
        ContentManager Content;
        private Matrix world;

        public List<GameObject> objects;

        public GameSystem(ContentManager content, Matrix world)
        {
            objects = new List<GameObject>();
            Content = content;
            this.world = world;
            Forest example_forest = new Forest(Content.Load<Model>("terrain"), world);
            AddObjcets(example_forest);
        }

        public void AddObjcets(GameObject gameObject)
        {
            objects.Add(gameObject);
        }

        public void Draw()
        {
            foreach (GameObject obj in objects)
            {
                obj.Draw();
            }
        }

        public void Update()
        {
            foreach(GameObject obj in objects)
            {
                obj.Update();
            }
        }

    }
}
