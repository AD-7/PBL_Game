using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.GameObjects.Movable;
using Wataha.GameSystem;

namespace Wataha.GameObjects.Interable
{
    public class UpgradeQuest : Quest
    {


        public GameObjects.Movable.Wataha wataha;
        int sumOfSkills;
        int oldSumOfSkills;

        public UpgradeQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game,GameObjects.Movable.Wataha wataha) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {
            this.wataha = wataha;
            foreach(Wolf w in wataha.wolves)
            {
                sumOfSkills += w.speed + w.strength + w.resistance;
            }

            oldSumOfSkills = sumOfSkills;

        }

        public override void Update()
        {
            
            oldSumOfSkills = sumOfSkills;
            sumOfSkills = 0;
            foreach (Wolf w in wataha.wolves)
            {
                sumOfSkills += w.speed + w.strength + w.resistance;
            }
        }

        public override bool IfCompleted(Wolf wolf)
        {
        
            if(oldSumOfSkills < sumOfSkills)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



       

    }
}
