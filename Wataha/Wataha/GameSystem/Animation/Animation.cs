using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Content;
using System.IO;

using System.Diagnostics;
using Wataha.GameObjects;

namespace Wataha.GameSystem.Animation
{
    public class Animation
    {
        public int CurrentFrame { get; set; }
        public int NumberOfFrames { get; private set; }
        public bool IsLooping { get; set; }
        public Dictionary<int, Model> animation { get; private set; }
        public float frameSpeed { get; set; }
        public ContentManager content { get; set; }


        public Animation(ContentManager content, String animationfolder)
        {
            animation = new Dictionary<int, Model>();
            this.content = content;
            loadContent(animationfolder);
            frameSpeed = 0.02f;
        }
        public Animation(ContentManager content, String animationfolder, float _frameSpeed)
        {
            animation = new Dictionary<int, Model>();
            this.content = content;
            loadContent(animationfolder);
            frameSpeed = _frameSpeed;
        }
        public Animation()
        {
            //For testing purposes;
        }
        public void loadContent(String animationFolder)
        {
            Trace.WriteLine("Animation - LaodContentStart");

            String path = Directory.GetCurrentDirectory();
            Trace.WriteLine("Animation - LaodContent  path " + path);

            List<String> objList = new List<string>();
            int pom = path.IndexOf("Wataha");
            // path = path.Substring(0,pom+6);
            path = path + "\\Content\\" + animationFolder;
            String[] fileList = Directory.GetFiles(path, "*.obj");
            if (fileList.Count() == 0)
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
                if (obj.Contains("tex_0"))
                    continue;
                String modelPath = animationFolder + "\\" + obj;
                animation.Add(i, content.Load<Model>(modelPath));
                i++;
            }
            NumberOfFrames = animation.Count;

        }

        public void SetEffect(Effect effect, bool copy)
        {
            for (int i = 0; i < animation.Count; i++)
            {
                foreach(ModelMesh mesh in animation[i].Meshes)
                    foreach(ModelMeshPart part in mesh.MeshParts)
                    {
                        Effect toSet = effect;
                        if (copy)
                            toSet = effect.Clone();


                        MeshTag tag = ((MeshTag)part.Tag);

                        if (tag.Texture != null)
                        {
                            setEffectParameter(toSet, "xTexture", tag.Texture);

                        }
                        else
                        {

                        }

                        part.Effect = toSet;
                    }
            }
        }

        public void generateTags()
        {
            for (int i = 0; i < animation.Count; i++)
            {
                foreach(ModelMesh mesh in animation[i].Meshes)
                    foreach(ModelMeshPart part in mesh.MeshParts)
                        if(part.Effect is BasicEffect)
                        {
                            BasicEffect effect = (BasicEffect)part.Effect;
                            MeshTag tag = new MeshTag(effect.DiffuseColor, effect.Texture, effect.SpecularPower);
                            part.Tag = tag;
                        }
            }
        }

        void setEffectParameter(Effect effect, string paramName, object val)
        {
            if (effect.Parameters[paramName] == null)
                return;

            if (val is Vector3)
                effect.Parameters[paramName].SetValue((Vector3)val);
            else if (val is bool)
                effect.Parameters[paramName].SetValue((bool)val);
            else if (val is Matrix)
                effect.Parameters[paramName].SetValue((Matrix)val);
            else if (val is Texture2D)
                effect.Parameters[paramName].SetValue((Texture2D)val);
        }

    }
}
