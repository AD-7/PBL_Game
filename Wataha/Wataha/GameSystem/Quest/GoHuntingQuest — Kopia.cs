using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class GoHuntingQuest : Quest
    {
        int meat = Resources.Meat;
        int oldmeat = Resources.Meat;

        public  GoHuntingQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {

             meat = Resources.Meat;
             oldmeat = Resources.Meat;

        }
        public override bool IfCompleted(Wolf wolf)
        {
           if(oldmeat  < meat)
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
            oldmeat = meat;
            meat = Resources.Meat;
        }
    }
}
