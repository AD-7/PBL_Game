using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects;
using Wataha.GameObjects.Movable;

namespace Wataha.GameSystem
{
    public class ColisionSystem
    {



        public bool IsCollisionTerrain(BoundingBox player, BoundingBox terrain)
        {

            if (player.Intersects(terrain))
                return true;
            else

                return false;
        }

        public bool IsEnvironmentCollision(Animal animal, Wataha.GameObjects.Static.Environment env)
        {
            int i = 0;
            foreach (ModelMesh mesh in env.model.Meshes)
            {

                if (mesh.Name.Contains("House") || mesh.Name.Contains("Bound") || mesh.Name.Contains("Blockade") || mesh.Name.Contains("defaultobject") || mesh.Name.Contains("well"))
                {
                    if (animal.collider.Intersects(env.colliders[i]))
                    {
                        animal.ifColisionTerrain = true;
                        animal.ProccedCollisionBuilding();
                        return true;
                    }
                }
                else
                {
                    if (animal.collider.Intersects(env.colliders[i]))
                    {
                        animal.ifColisionTerrain = true;
                        animal.ProccedCollisionTree();
                        return true;
                    }
                }
                i++;
            }
            return false;
        }



        public bool IsEnvironmentCollision(Wolf player, Wataha.GameObjects.Static.Environment env, Wataha.GameObjects.Movable.Wataha wataha)
        {
            int i = 0;
            foreach (ModelMesh mesh in env.model.Meshes)
            {
                if (!mesh.Name.Contains("Plane"))
                {


                    if (mesh.Name.Contains("House") || mesh.Name.Contains("Bound") || mesh.Name.Contains("Blockade") || mesh.Name.Contains("defaultobject") || mesh.Name.Contains("well") || mesh.Name.Contains("Plotek") || mesh.Name.Contains("Barell"))
                    {
                        if (player.collider.Intersects(env.colliders[i]))
                        {

                            foreach (Wolf w in wataha.wolves)
                            {
                                w.ifColisionTerrain = true;
                                w.ProccedCollisionBuilding();
                            }

                            return true;
                        }
                    }
                    else
                    {
                        if (player.collider.Intersects(env.colliders[i]))
                        {
                            foreach (Wolf w in wataha.wolves)
                            {
                                w.ifColisionTerrain = true;
                                w.ProccedCollisionTree();
                            }

                            return true;
                        }
                    }
                    i++;
                }
            }
            return false;
        }





    }
}
