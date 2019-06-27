using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Wataha.GameSystem;
using System;
using System.Linq;
using System.Collections.Generic;
using Wataha.GameObjects.Movable;

namespace Wataha.GameObjects
{
    public class GameObject
    {
        public Matrix world;
        public Model model;
        public Material material;
        public BoundingBox collider;
        public BoundingSphere sphere;

        public static Vector3 lightPos = new Vector3(-100, 170, 180);
        public static float lightPower = 0.9f;
        public static float ambientPower = 0.5f;
        Color color = new Color(255, 211, 142);
        public static Vector4 lightColor = new Vector4(1, 1, 1, 1);
        Matrix lightsViewProjectionMatrix;
        float alpha = 1.0f;
        public Texture2D shadowMap;
        public Texture2D texture;

        public GameObject(Matrix world, Model model)
        {
            this.world = world;
            this.model = model;
            this.material = new Material();
            lightPos = new Vector3(-100, 170, 170);
            lightPower = 0.9f;
            ambientPower = 0.5f;
            color = new Color(255, 211, 142, 255);
            lightColor = color.ToVector4();
            

            Matrix lightsView = Matrix.CreateLookAt(lightPos,new Vector3(-50, -20, -250), new Vector3(0, 1, 0));
            // Matrix lightsProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(150), 1f, 10f, 200f);
            Matrix lightsProjection = Matrix.CreateOrthographic(300, 300, 0.1f, 1000f);
            lightsViewProjectionMatrix = lightsView * lightsProjection;

            generateTags();
        }


        public virtual void Draw()
        {

        }

        public virtual void Draw(Camera camera, string technique)
        {

            DrawModel(camera.View, camera.Projection, camera, technique);

        }

        public virtual void Update(GameTime gameTime)
        {
                Matrix lightsView = Matrix.CreateLookAt(lightPos, new Vector3(-50, -20, -250), new Vector3(0, 1, 0));
                Matrix lightsProjection = Matrix.CreateOrthographic(300, 300, 0.1f, 1000f);
                lightsViewProjectionMatrix = lightsView * lightsProjection;
        }

       public static double timer = 0;

        public static void changeDay(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalMilliseconds / 1000;
       
            if(timer  <= 180)
            {
                lightPos.X += 0.0005f;
               // lightPos.Y += 0.0005f;
                lightPos.Z -= 0.0005f;

                lightColor.Y += 0.00000862745f;
                lightColor.Z += 0.00002607843f;

                ambientPower += 0.00005f;
                lightPower += 0.000007f;

            }
            else if(timer >= 180  && timer <= 360)
            {
                lightPos.X += 0.005f;
                // lightPos.Y -= 0.0005f;
                lightPos.Z -= 0.0005f;


                lightColor.Y -= 0.00000862745f;
                lightColor.Z -= 0.00002607843f;

                ambientPower -= 0.00005f;
                lightPower -= 0.000007f;

            }
            else if(timer >= 360 && timer <= 480)
            {
                lightPos.X -= 0.005f;
                //  lightPos.Y -= 0.0005f;
                lightPos.Z += 0.0005f;


                lightColor.X -= 0.00003921568f;
                lightColor.Y -= 0.000027058823f;
                lightColor.Z += 0.000096862745f;

                ambientPower -= 0.00005f;
                lightPower -= 0.000007f;

            }
            else if(timer >= 480 && timer <=600)
            {
                lightPos.X -= 0.005f;
                // lightPos.Y += 0.0005f;
                lightPos.Z += 0.0005f;


                lightColor.X += 0.00003921568f;
                lightColor.Y += 0.000027058823f;
                lightColor.Z -= 0.000096862745f;

                ambientPower += 0.00005f;
                lightPower += 0.000007f;

            }
            else
            {
                timer = 0;
            }



            //i++;
            //if (lightPos.X >= -100 && lightPos.X < 0 && lightPos.Y >= 170 && lightPos.Y < 180)
            //{
            //    lightPos.X += 0.005f;
            //    lightPos.Y += 0.0005f;

            //    lightColor.Y += 0.00000862745f;
            //    lightColor.Z += 0.00002607843f;

            //    ambientPower += 0.00005f;
            //    lightPower += 0.000005f;

            //    Console.WriteLine("1" + lightPos + "\ncolor" + lightColor +"\n" + i);
            //}
            //else if (lightPos.X >= 0 && lightPos.X < 100 && lightPos.Y <= 181 && lightPos.Y > 170)
            //{
            //    lightPos.X += 0.005f;
            //    lightPos.Y -= 0.0005f;

            //    lightColor.Y -= 0.00000862745f;
            //    lightColor.Z -= 0.00002607843f;

            //    ambientPower += 0.00005f;
            //    lightPower += 0.000001f;

            //    Console.WriteLine("2" + lightPos);


            //}
            //else if (lightPos.X > 0 && lightPos.X <= 100 && lightPos.Y <= 171 && lightPos.Y > 160)
            //{
            //    lightPos.X -= 0.005f;
            //    lightPos.Y -= 0.0005f;

            //    lightColor.X -= 0.00003921568f;
            //    lightColor.Y -= 0.000027058823f;
            //    lightColor.Z += 0.000046862745f;

            //    ambientPower -= 0.00005f;
            //    lightPower -= 0.000001f;

            //    Console.WriteLine("3" + lightPos);

            //}
            //else if (lightPos.X > -100 && lightPos.X <= 0 && lightPos.Y >= 159 && lightPos.Y < 170)
            //{
            //    lightPos.X -= 0.005f;
            //    lightPos.Y += 0.0005f;

            //    lightColor.X += 0.00003921568f;
            //    lightColor.Y += 0.000027058823f;
            //    lightColor.Z -= 0.000046862745f;

            //    ambientPower -= 0.00005f;
            //    lightPower -= 0.000005f;

            //    Console.WriteLine("4" + lightPos);
            //}
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
                if (!(this is Animal))
                {
                    if ((camera.frustum.Contains(mesh.BoundingSphere) != ContainmentType.Disjoint) &&
                                      (Vector3.Distance(mesh.BoundingSphere.Center, camera.CamTarget) < 120.0f &&
                                      (Vector3.Distance(mesh.BoundingSphere.Center, camera.CamPos) > 10.0f)) || mesh.Name.Contains("Plane"))
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
                                effect.Parameters["LightColor"].SetValue(lightColor);
                                effect.Parameters["xAmbient"].SetValue(ambientPower);
                                effect.Parameters["xShadowMap"].SetValue(shadowMap);
                                effect.Parameters["xAlpha"].SetValue(alpha);

                            }

                        }
                        mesh.Draw();
                    }
                }
                else
                {
                    if ((camera.frustum.Contains(sphere) != ContainmentType.Disjoint) &&
                                    (Vector3.Distance(sphere.Center, camera.CamTarget) < 120.0f &&
                                    (Vector3.Distance(sphere.Center, camera.CamPos) > 10.0f)) || mesh.Name.Contains("Plane"))
                    {
                        foreach (ModelMeshPart meshPart in mesh.MeshParts)
                        {



                            //if ((Vector3.Distance(sphere.Center, camera.CamPos) < 15.0f))
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
                                effect.Parameters["LightColor"].SetValue(lightColor);
                                effect.Parameters["xAmbient"].SetValue(ambientPower);
                                effect.Parameters["xShadowMap"].SetValue(shadowMap);
                                effect.Parameters["xAlpha"].SetValue(alpha);

                            }

                        }
                        mesh.Draw();
                    }
                }

            }
        }

        public void SetTexture()
        {
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect.Parameters["xTexture"].SetValue(texture);
                }
        }


        public void SetModelEffect(Effect effect, bool CopyEffect)
        {
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Effect toSet = effect;
                    //Copy the effect if necessary
                    if (CopyEffect)
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
