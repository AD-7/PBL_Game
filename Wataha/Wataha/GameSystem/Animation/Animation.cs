using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.IO;

namespace Wataha.GameSystem.Animation
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int NumberOfFrames { get; private set; }
        public bool IsLooping { get; set; }
        public Dictionary<int,Model> animation { get; private set; }
        public float frameSpeed { get; set; }
        public ContentManager content { get; set; }

        public Animation(ContentManager content,String animationfolder)
        {
            this.content = content;
            loadContent(animationfolder);
            frameSpeed = 0.2f;
        }
        public Animation()
        {
            //For testing purposes;
        }
        public void loadContent(String animationFolder)
        {
            String path = Directory.GetCurrentDirectory();
            List<String> objList = new List<string>();
            int pom = path.IndexOf("Wataha");
            path = path.Substring(0,pom+6);
            path = path + "\\Wataha\\Content\\"+animationFolder;
            String[] fileList = Directory.GetFiles(path, "*.obj");
            foreach (String file in fileList)
            {
                objList.Add(Path.GetFileNameWithoutExtension(file));
            }
            foreach(String obj in objList)
            {
                int i =  0;
                String modelPath = animationFolder + "\\" + obj;
                animation.Add(i, content.Load<Model>(modelPath));
            }
            NumberOfFrames = animation.Count;
            
        }
    }
}
