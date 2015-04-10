using Models.Universe.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Generation.StarSystem
{
    public static class StarProperties
    {
        public static StarColor DetermineStarColor(int seed)
        {
            if (seed >= 0 && seed <= 50) return StarColor.Red;
            if (seed >= 51 && seed <= 71) return StarColor.Yellow;
            if (seed >= 72 && seed <= 92) return StarColor.Orange;
            if (seed >= 93 && seed <= 96) return StarColor.White;

            return StarColor.Blue;
        }

        public static StarType DetermineStarType(StarColor color)
        {
            return StarType.Dwarf;
        }
    }
}
