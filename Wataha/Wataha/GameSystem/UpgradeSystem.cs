using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem
{
    public class UpgradeSystem
    {
        public int strength;
        public int resistance;
        public int speed;
        public int agression;
        public int costM;
        public int costWF;
        public int costGF;

        public UpgradeSystem(int strength, int resistance, int speed, int agression, int costM, int costWF, int costGF)
        {
            this.strength = strength;
            this.resistance = resistance;
            this.speed = speed;
            this.agression = agression;
            this.costM = costM;
            this.costWF = costWF;
            this.costGF = costGF;
        }
    }
}
