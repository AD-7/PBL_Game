using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wataha.GameSystem
{
    public static class Resources
    {
      static  int  meat = 0;
        static int whitefangs = 0;
        static int goldfangs = 0;

        public static int Goldfangs { get => goldfangs; set => goldfangs = value; }
        public static int Whitefangs { get => whitefangs; set => whitefangs = value; }
        public static int Meat { get => meat; set => meat = value; }
    }
}
