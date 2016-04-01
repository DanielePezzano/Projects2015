using System;

namespace BLL.Utilities
{
    public static class GenericUtils
    {
        public static bool IsInRangeDoubleSimple(double value, double perc)
        {
            var percMore = Math.Truncate(value + value * perc / 100);
            var percLess = Math.Truncate(value - value * perc / 100);

            return value >= percLess && value <= percMore;
        }

        public static bool IsInRangeDoubleCompared(double value, double rif, double perc)
        {
            var percMore = Math.Truncate(rif + rif * perc / 100);
            var percLess = Math.Truncate(rif - rif * perc / 100);

            return value >= percLess && value <= percMore; 
        }
    }
}
