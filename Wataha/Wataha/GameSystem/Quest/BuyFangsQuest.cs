using Microsoft.Xna.Framework;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class BuyFangsQuest : Quest
    {
        int fangs = Resources.Whitefangs;
        int oldfangs = Resources.Whitefangs;
        GameObject barrell;

        public  BuyFangsQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {

            fangs = Resources.Whitefangs;
            oldfangs = Resources.Whitefangs;
            barrell = game;
        }
        public override bool IfCompleted(Wolf wolf)
        {
           if(oldfangs  < fangs && Vector3.Distance(wolf.position,barrell.model.Meshes[0].BoundingSphere.Center) < 20)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Update()
        {
            oldfangs = fangs;
            fangs = Resources.Whitefangs;
        }
    }
}
