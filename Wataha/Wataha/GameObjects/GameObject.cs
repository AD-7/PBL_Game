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

       Vector3 lightPos = new Vector3(-100, 60, 30);
        float lightPower = 1.2f;
        float ambientPower = 0.4f;
        Matrix lightsViewProjectionMatrix;
        float alpha = 1.0f;
        public Texture2D shadowMap;


        public GameObject(Matrix world, Model model)
        {
            this.world = world;
            this.model = model;
            this.material = new Material();
            Matrix lightsView = Matrix.CreateLookAt(lightPos, new Vector3(80, 30, 0), new Vector3(0, 1, 0));
            Matrix lightsProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(150), 1f, 10f, 200f);
            lightsViewProjectionMatrix = lightsView * lightsProjection;

            generateTags();
        }


        public virtual  void Draw()
        {
            
        }

        public virtual void Draw(Camera camera,string technique)
        {
           
            DrawModel( camera.View, camera.Projection, camera,technique);

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


        public void DrawModel(Matrix view, Matrix projection, Camera camera, string technique)

        {
          
            foreach (ModelMesh mesh in model.Meshes)
            {
             
            
             if ((Vector3.Distance(mesh.BoundingSphere.Center, camera.CamTarget) < 100.0f  && (Vector3.Distance(mesh.BoundingSphere.Center, camera.CamPos) > 10.0f))   || mesh.Name.Contains("Plane"))
                {
                    foreach (ModelMeshPart meshPart in mesh.MeshParts)
                    {



                        //if ((Vector3.Distance(mesh.BoundingSphere.Center, camera.CamPos) < 15.0f))
                        //    alpha = 0.0f;
                        //else
                        //    alpha = 1.0f;

                        Effect effect = meshPart.Effect;

                        if (effect is BasicEffect)
                        {
                            ((BasicEffect)effect).World = world;
                            ((BasicEffect)effect).View = view;
                            ((BasicEffect)effect).Projection = projection;
                            material.SetEffectParameters(effect);
                            ((BasicEffect)effect).EnableDefaultLighting();



                        }
                        else
                        {

                            effect.CurrentTechnique = effect.Techniques[technique];
                            effect.Parameters["xWorldViewProjection"].SetValue(world * camera.View * camera.Projection);
                            effect.Parameters["xLightsWorldViewProjection"].SetValue(world * lightsViewProjectionMatrix);
                            effect.Parameters["xWorld"].SetValue(world);
                            effect.Parameters["xLightPos"].SetValue(lightPos);
                            effect.Parameters["xLightPower"].SetValue(lightPower);
                            effect.Parameters["xAmbient"].SetValue(ambientPower);
                            effect.Parameters["xShadowMap"].SetValue(shadowMap);
                            effect.Parameters["xAlpha"].SetValue(alpha);
                        }

                    }


                    mesh.Draw();


                }
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
