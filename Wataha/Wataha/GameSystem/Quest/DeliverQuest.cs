using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class DeliverQuest : Quest
    {
        public int NeedGold;
        public int NeedWhite;
        public int NeedMeat;

        public DeliverQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game, int gold, int white, int meat) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {
            NeedGold = gold;
            NeedWhite = white;
            NeedMeat = meat;
        }

        public override bool IfCompleted(Wolf wolf)
        {
            if (Vector3.Distance(questDestination.model.Meshes[0].BoundingSphere.Center, wolf.model.Meshes[0].BoundingSphere.Center) <10.0f &&
                (Resources.Goldfangs >= NeedGold && Resources.Whitefangs >= NeedWhite && Resources.Meat >= NeedMeat))
                  return true;
            else
                  return false;
        }

        public override void Update()
        {
        }
    }
}
