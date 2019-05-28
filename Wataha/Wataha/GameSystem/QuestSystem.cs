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
        public static QuestGiver currentGiver = null;
        public static Quest currentQuest = null;

        public QuestSystem()
        {
            this.questGivers = new List<QuestGiver>();
        }

        public void Update(GameTime gameTime)
        {
            foreach (QuestGiver q in questGivers)
                q.Update(gameTime);

            if(currentQuest.IfCompleted())
            {
                Resources.Meat += currentQuest.MeatReward;
                Resources.Whitefangs += currentQuest.WhiteFangReward;
                Resources.Goldfangs += currentQuest.GoldFangReward;
                currentQuest = null;
            }


        }

        public void Draw(Camera camera)
        {
            
        }

        public bool ChceckNearestQuestGiver(Wolf wolf)
        {
            foreach (QuestGiver giver in questGivers)
            {
                if (Vector3.Distance(wolf.model.Meshes[0].BoundingSphere.Center, giver.model.Meshes[0].BoundingSphere.Center) < 15.0f)
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
