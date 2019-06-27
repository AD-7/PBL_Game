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
    public class FindToolsQuest : Quest
    {


        public Wolf wolf;
        public List<QuestItem> tools;
        public int collectedTools;

        public FindToolsQuest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game, ContentManager content, Wolf wolf, Effect effect) : base(questId, questTitle, questDescription, needStrenght, needResistance, needSpeed, meatReward, whiteFangReward, goldFangReward, game)
        {
            // Matrix worldw4 = new Matrix();
            Matrix worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(180));
            worldw4 *= Matrix.CreateRotationZ(MathHelper.ToRadians(0));
            worldw4 *= Matrix.CreateTranslation(new Vector3(-40f, 1.4f, -23.0f));

            tools = new List<QuestItem>();
            tools.Add(new QuestItem( worldw4, content.Load<Model>("quest items/hammer"), 0.1f));



            worldw4 = new Matrix();
            worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(0));
            worldw4 *= Matrix.CreateRotationY(MathHelper.ToRadians(90));
            worldw4 *= Matrix.CreateTranslation(new Vector3(63f, 0.2f, -154.0f));
            tools.Add(new QuestItem(worldw4, content.Load<Model>("quest items/handsaw"), 0.1f));


            worldw4 = new Matrix();
            worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(90));
            worldw4 *= Matrix.CreateRotationZ(MathHelper.ToRadians(90));
            worldw4 *= Matrix.CreateTranslation(new Vector3(77, 0.01f, -30.0f));
            tools.Add(new QuestItem(worldw4, content.Load<Model>("quest items/spanner"), 0.1f));

            worldw4 = new Matrix();
            worldw4 = Matrix.CreateRotationX(MathHelper.ToRadians(145));
            worldw4 *= Matrix.CreateRotationY(MathHelper.ToRadians(130));
            worldw4 *= Matrix.CreateTranslation(new Vector3(-55.8f, 0.82f, -238.5f));
            tools.Add(new QuestItem(worldw4, content.Load<Model>("quest items/screwdriver"), 0.1f));


            this.wolf = wolf;

            foreach (QuestItem t in tools)
            {
                t.SetModelEffect(effect, true);
            }


        }

        public override void Update()
        {
            CheckFoundTools(wolf);
            foreach (QuestItem t in tools)
            {
        
            }
        }

        public override bool IfCompleted(Wolf wolf)
        {
            if (tools.Count <= 0 && Vector3.Distance(questDestination.model.Meshes[0].BoundingSphere.Center, wolf.position) < 8)
                return true;
            else
                return false;
        }



        void CheckFoundTools(Wolf wolf)
        {
            List<QuestItem> tmp = new List<QuestItem>();
            foreach (QuestItem t in tools)
            {
                if (Vector3.Distance(t.model.Meshes[0].BoundingSphere.Center, wolf.position) <= 6 && InputSystem.newKeybordState.IsKeyDown(Keys.F) && InputSystem.oldKeybordState.IsKeyUp(Keys.F))
                {


                    tmp.Add(t);
                    collectedTools++;

                }
            }
            foreach (QuestItem t in tmp)
            {
                tools.Remove(t);
            }
        }

    }
}
