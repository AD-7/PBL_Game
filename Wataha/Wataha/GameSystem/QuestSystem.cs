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
        public static QuestGiver currentQuestGivers;
        public static QuestGiver currentGiver = null;
        public static Quest currentQuest = null;

        public QuestSystem()
        {
            this.questGivers = new List<QuestGiver>();
        }

        public void Update(GameTime gameTime, Wolf wolf)
        {
            foreach (QuestGiver q in questGivers)
                q.Update(gameTime);

            if(currentQuest != null && currentQuest.IfCompleted(wolf))
            {
                if(currentQuest is DeliverQuest)
                {
                    Resources.Meat -= ((DeliverQuest)currentQuest).NeedMeat;
                    Resources.Whitefangs -= ((DeliverQuest)currentQuest).NeedWhite;
                    Resources.Goldfangs -= ((DeliverQuest)currentQuest).NeedGold;
                }
                currentQuestGivers.CompletedQuest();
                Resources.Meat += currentQuest.MeatReward;
                Resources.Whitefangs += currentQuest.WhiteFangReward;
                Resources.Goldfangs += currentQuest.GoldFangReward;
                currentQuest = null;
                currentQuestGivers = null;
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
                    currentQuestGivers = currentGiver;
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
