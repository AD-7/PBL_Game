﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wataha.GameObjects;
using Wataha.GameObjects.Movable;

namespace Wataha.GameObjects.Interable
{

    public abstract class Quest
    {
        public enum status : int { INACTIVE, ACTIVE, FAILD, SUCCED };

        public int questId;
        public string questTitle;
        public string questDescription;

        public status questStatus;
        public int questStage;
        public int questFinalStage;
        public int questCollectedItems;

        public GameObject questDestination;

        public int NeedStrenght;
        public int NeedResistance;
        public int NeedSpeed;

        public int MeatReward;
        public int WhiteFangReward;
        public int GoldFangReward;
        public GameObject[] questItems;

        public Quest()
        {
            questStage = 0;
            questCollectedItems = 0;
            questStatus = status.INACTIVE;
        }

        public Quest(int questId, string questTitle, string questDescription, int needStrenght, int needResistance, int needSpeed, int meatReward, int whiteFangReward, int goldFangReward, GameObject game)
        {
            questStage = 0;
            questCollectedItems = 0;
            questStatus = status.INACTIVE;
            this.questId = questId;
            this.questTitle = questTitle;
            this.questDescription = questDescription;
            NeedStrenght = needStrenght;
            NeedResistance = needResistance;
            NeedSpeed = needSpeed;
            MeatReward = meatReward;
            WhiteFangReward = whiteFangReward;
            GoldFangReward = goldFangReward;
            questDestination = game;
        }

        // Update is called once per frame
        public abstract void Update();


        public void QuestActive()
        {
            this.questStatus = status.ACTIVE;
        }

        public void QuestFaild()
        {
            this.questStatus = status.FAILD;
        }

        public void QuestSucced()
        {
            this.questStatus = status.SUCCED;
        }

        public abstract bool IfCompleted(Wolf wolf);
        
        public void ItemCollected()
        {
            questCollectedItems++;
        }
    }
}
