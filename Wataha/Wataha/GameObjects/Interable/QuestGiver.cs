﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wataha.System;

namespace Wataha.GameObjects.Interable
{
    class QuestGiver : InterableObject
    {
        public QuestGiver questsGiverNeedToStart;
        public List<Quest> questsList;
        public List<Quest> questCompleted;
        public Quest actualQuest;
        // public GameObject reward;

        public QuestGiver(Model model, Matrix world)
        {
            base.model = model;
            base.world = world;

            questsList = new List<Quest>();
            questCompleted = new List<Quest>();

        }

        public override void Draw()
        {

            base.Draw();
        }

        public override void Draw(Camera camera)
        {

            base.Draw(camera);
        }

        // Update is called once per frame
        public override void Update()
        {

            if (actualQuest != null)
                if (actualQuest.questStatus.Equals(Quest.status.SUCCED))
                    CompletedQuest();
        }

        public void CompletedQuest()
        {
            questCompleted.Add(actualQuest);
            if ((questsList.IndexOf(actualQuest) + 1) < questsList.Count)
            {
                actualQuest = questsList[questsList.IndexOf(actualQuest) + 1];
            }
            else
            {
                actualQuest = null;
            }
        }
    }
}
