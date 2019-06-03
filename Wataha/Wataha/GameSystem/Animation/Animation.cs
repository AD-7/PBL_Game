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

using System.Diagnostics;

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
            animation = new Dictionary<int, Model>();
            this.content = content;
            loadContent(animationfolder);
            frameSpeed = 0.02f;
        }
        public Animation()
        {
            //For testing purposes;
        }
        public void loadContent(String animationFolder)
        {
            Trace.WriteLine("Animation - LaodContentStart");
            
            String path = Directory.GetCurrentDirectory();
            Trace.WriteLine("Animation - LaodContent  path " + path );

            List<String> objList = new List<string>();
            int pom = path.IndexOf("Wataha");
           // path = path.Substring(0,pom+6);
            path = path + "\\Content\\"+animationFolder;
            String[] fileList = Directory.GetFiles(path, "*.obj");
            if(fileList.Count() == 0)
                fileList = Directory.GetFiles(path, "*.xnb");

            
            foreach (String file in fileList)
            {
                Trace.WriteLine("Animation - LaodContent  file " + file);
                objList.Add(Path.GetFileNameWithoutExtension(file));
            }
            int i = 0;
            foreach (String obj in objList)
            {

                Trace.WriteLine("Animation - LaodContent  animation " + obj);
                if (obj.Equals("tex_0"))
                    continue;
                String modelPath = animationFolder + "\\" + obj;
                animation.Add(i, content.Load<Model>(modelPath));
                i++;
            }
            NumberOfFrames = animation.Count;
            
        }
    }
}
