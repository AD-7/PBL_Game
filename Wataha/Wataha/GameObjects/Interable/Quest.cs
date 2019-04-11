using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wataha.GameObjects;

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

       // public GameObject questDestination;

        public int NeedStrenght;
        public int NeedResistance;
        public int NeedSpeed;
        public int MaxAgresion;

        public int MeatReward;
        public int WhiteFangReward;
        public int GoldFangReward;
        public GameObject[] questItems;

        void Initialized()
        {
            questStage = 0;
            questCollectedItems = 0;
            questStatus = status.INACTIVE;
        }

        // Update is called once per frame
        public void Update()
        {

        }

        public void questActive()
        {
            this.questStatus = status.ACTIVE;
        }

        public void questFaild()
        {
            this.questStatus = status.FAILD;
        }

        public void questSucced()
        {
            this.questStatus = status.SUCCED;
        }

        public void itemCollected()
        {
            questCollectedItems++;
        }
    }
}
