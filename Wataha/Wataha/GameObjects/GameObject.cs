using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameSystem;
using System;

namespace Wataha.GameObjects
{
    public class GameObject
    {
        public Matrix world;
        public Model model;
        public Material material;

        Vector3 lightPos = new Vector3(-5, 60, 10);
        float lightPower = 0.7f;
        float ambientPower = 0.5f;


        public GameObject(Matrix world, Model model)
        {
            this.world = world;
            this.model = model;
            this.material = new Material();
            generateTags();
        }


        public virtual  void Draw()
        {
            
        }

        public virtual void Draw(Camera camera)
        {

            DrawModel( camera.View, camera.Projection, camera);

        }

        public virtual void Update()
        {

          
        }

        public void RotateY(float degrees)
        {
            world *= Matrix.CreateRotationY(MathHelper.ToRadians(degrees));
        }
        public void RotateX(float degrees)
        {
            world *= Matrix.CreateRotationX(MathHelper.ToRadians(degrees));
        }
        public void RotateZ(float degrees)
        {
            world *= Matrix.CreateRotationZ(MathHelper.ToRadians(degrees));
        }
        public void Translate(Vector3 vector)
        {
            world *= Matrix.CreateTranslation(vector);
        } 
        public void Scale(float scale)
        {
            world *= Matrix.CreateScale(scale);
        }
        public void Forward(Vector3 vector)
        {
          
        }


        public void DrawModel(Matrix view, Matrix projection, Camera camera)

        {
          
            foreach (ModelMesh mesh in model.Meshes)
            {

               foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {

                    Effect effect = meshPart.Effect;

                    if(effect is BasicEffect)
                    {
                        ((BasicEffect)effect).World = world;
                        ((BasicEffect)effect).View = view;
                        ((BasicEffect)effect).Projection = projection;
                        material.SetEffectParameters(effect);
                        ((BasicEffect)effect).EnableDefaultLighting();

                    }
                    else
                    {
                        effect.CurrentTechnique = effect.Techniques["BasicColorDrawing"];
                        effect.Parameters["xWorldViewProjection"].SetValue(world * camera.View * camera.Projection);
                        effect.Parameters["xWorld"].SetValue(world);
                        effect.Parameters["xLightPos"].SetValue(lightPos);
                        effect.Parameters["xLightPower"].SetValue(lightPower);
                        effect.Parameters["xAmbient"].SetValue(ambientPower);

                      

                    }

                }

                if (Vector3.Distance(mesh.BoundingSphere.Center, camera.CamTarget) < 100.0f  || mesh.Name.Contains("Plane"))
                     mesh.Draw();
            }
        }



        public void SetModelEffect (Effect effect, bool CopyEffect)
        {
            foreach(ModelMesh mesh in model.Meshes)
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    Effect toSet = effect;
                    //Copy the effect if necessary
                    if (CopyEffect)
                        toSet = effect.Clone();

                    MeshTag tag = ((MeshTag)part.Tag);

                    if(tag.Texture != null)
                    {
                        setEffectParameter(toSet, "xTexture", tag.Texture);
                       
                    }
                    else
                    {

                    }
                       

                        part.Effect = toSet;
                    

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


        private void generateTags()
        {
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    if (part.Effect is BasicEffect)
                    {
                        BasicEffect effect = (BasicEffect)part.Effect;
                        MeshTag tag = new MeshTag(effect.DiffuseColor, effect.Texture, effect.SpecularPower);
                        part.Tag = tag;
                    }
        }

        public void CacheEffects()
        {
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    ((MeshTag)part.Tag).CachedEffect = part.Effect;
        }

        public void RestoreEffects()
        {
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    if (((MeshTag)part.Tag).CachedEffect != null)
                        part.Effect = ((MeshTag)part.Tag).CachedEffect;
        }

    }
}
