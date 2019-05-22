using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wataha.GameObjects.Interable;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem.Interfejs;


namespace Wataha.GameSystem
{
    class QuestSystem
    {
        public List<QuestGiver> questGivers;
        public QuestGiver currentGiver;
        public static Quest currentQuest;
        public QuestPanel questPanel;

        public QuestSystem()
        {
            this.questGivers = new List<QuestGiver>();
            this.currentGiver = null;
        }

        public void Update(GameTime gameTime)
        {
            foreach (QuestGiver q in questGivers)
                q.Update(gameTime);
        }

        public void Draw(Camera camera)
        {
            
        }

        public bool ChceckNearestQuestGiver(Wolf wolf)
        {
            foreach (QuestGiver giver in questGivers)
            {
                System.Console.WriteLine("test + ", giver);
                if (Vector3.Distance(wolf.model.Meshes[0].BoundingSphere.Center, giver.model.Meshes[0].BoundingSphere.Center) < 10.0f)
                {
                    currentGiver = giver;
                    return true;
                }
                else
                {
                    currentGiver = null;
                    return false;
                }

            }
            return false;
        }
    }
}
