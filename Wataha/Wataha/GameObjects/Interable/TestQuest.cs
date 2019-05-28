using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameObjects.Interable
{
    class TestQuest : Quest
    {
        public TestQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward)
        {
        }

        public override bool IfCompleted()
        {
            if(Vector3.Distance(questDestination.model.Meshes[0].BoundingSphere.Center,Vector3.Zero) <10.0f)
                  return true;
            else
                  return false;
        }
    }
}
