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
        public static List<QuestGiver> questGivers;
        public static QuestGiver currentQuestGivers = null;
        public static QuestGiver currentGiver = null;
        public static Quest currentQuest = null;

        public QuestSystem()
        {
            questGivers = new List<QuestGiver>();
            currentGiver = null;
            currentQuest = null;
            currentQuestGivers = null;
        }

        public void Update(GameTime gameTime, Wolf wolf)
        {
            if(currentQuest != null)
                  currentQuest.Update();

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

                HUDController.ifQuestCompleted = true; 
            }


        }

        public void Draw(Camera camera)
        {
            
        }

        public bool ChceckNearestQuestGiver(Wolf wolf)
        {
            foreach (QuestGiver giver in questGivers)
            {
                if (Vector3.Distance(wolf.model.Meshes[0].BoundingSphere.Center, giver.model.Meshes[0].BoundingSphere.Center) < 15.0f && currentQuest == null &&
                    (giver.questsGiverNeedToStart == null || (giver.questsGiverNeedToStart != null && giver.questsGiverNeedToStart.actualQuest == null)))
                {
                    currentGiver = giver;
                    currentQuestGivers = currentGiver;
                    return true;
                }
                else
                {
                    currentGiver = null;
                }

            }
            return false;
        }
    }
}
