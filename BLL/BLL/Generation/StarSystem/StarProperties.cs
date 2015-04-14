using BLL.Utilities;
using BLL.Utilities.Structs;
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

        public static StarType DetermineStarType(StarColor color, int seed)
        {
            StarType result = StarType.Dwarf;

            switch (color)
            {
                case StarColor.Blue:
                    if (seed <= 40) result = StarType.Dwarf;
                    if (seed > 40 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.Red:
                    if (seed <= 70) result = StarType.Dwarf;
                    if (seed > 70 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.White:
                    if (seed <= 80) result = StarType.Dwarf;
                    if (seed > 80 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    if (seed <= 60) result = StarType.Dwarf;
                    if (seed > 60 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                default:
                    result = StarType.Dwarf;
                    break;
            }
            return result;
        }

        public static int CalculateResultInRange(int seed, int min, int max, int minResult)
        {
            int result = minResult;
            using (RangeConversion rangeConverter = new RangeConversion(0, 10, min, max, new ScaleConversion(10, max - min)))
            {
                result = (int)rangeConverter.DoConversion(seed);
            }
            return result;
        }

        public static IntRange CalculateTemperatureRange(StarColor starcolor, StarType starType)
        {
            IntRange result = new IntRange(2800, 3000);
            switch (starcolor)
            {
                case StarColor.Blue:
                    if (starType == StarType.Dwarf) { result.Min = 10000; result.Max = 20000; }
                    if (starType == StarType.Giant) { result.Min = 15000; result.Max = 30000; }
                    if (starType == StarType.HyperGiant) { result.Min = 25000; result.Max = 40000; }
                    break;
                case StarColor.Orange:
                    if (starType == StarType.Dwarf) { result.Min = 3700; result.Max = 4000; }
                    if (starType == StarType.Giant) { result.Min = 4000; result.Max = 5000; }
                    if (starType == StarType.HyperGiant) { result.Min = 5000; result.Max = 5200; }
                    break;
                case StarColor.Red:
                    if (starType == StarType.Dwarf) { result.Min = 2800; result.Max = 3000; }
                    if (starType == StarType.Giant) { result.Min = 3000; result.Max = 3200; }
                    if (starType == StarType.HyperGiant) { result.Min = 3200; result.Max = 3700; }
                    break;
                case StarColor.White:
                    if (starType == StarType.Dwarf) { result.Min = 7500; result.Max = 9000; }
                    if (starType == StarType.Giant) { result.Min = 9000; result.Max = 9500; }
                    if (starType == StarType.HyperGiant) { result.Min = 9500; result.Max = 12000; }
                    break;
                case StarColor.Yellow:
                    if (starType == StarType.Dwarf) { result.Min = 5200; result.Max = 5500; }
                    if (starType == StarType.Giant) { result.Min = 5500; result.Max = 5800; }
                    if (starType == StarType.HyperGiant) { result.Min = 5800; result.Max = 6000; }
                    break;
                default:
                    break;
            }
            return result;
        }

        private static IntRange CalculateRadiationRange(StarColor starcolor)
        {
            IntRange range = new IntRange(0, 10);
            switch (starcolor)
            {
                case StarColor.Blue:
                    range.Min = 10; range.Max = 15;
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    range.Min = 3; range.Max = 5;
                    break;
                case StarColor.Red:
                    range.Min = 5; range.Max = 7;
                    break;
                case StarColor.White:
                    range.Min = 7; range.Max = 11;
                    break;
                default:
                    break;
            }
            return range;
        }

        public static int DetermineSurfaceTemp(StarColor starColor, StarType starType, int seed)
        {
            IntRange temperature = CalculateTemperatureRange(starColor, starType);
            int result = CalculateResultInRange(seed, temperature.Min, temperature.Max, 2800);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="starColor"></param>
        /// <param name="seed">number between 0,10</param>
        /// <returns></returns>
        public static int DetermineStarRadiation(StarColor starColor, int seed)
        {
            IntRange radiationRange = CalculateRadiationRange(starColor);
            int result = CalculateResultInRange(seed, radiationRange.Min, radiationRange.Max, 3);
            return result;
        }

        public static double DetermineStarMass(StarType starType, StarColor starColor, int seed)
        {
            double result = 0.08;
            switch (starColor)
            {
                case StarColor.Blue:
                    break;
                case StarColor.Orange:
                    break;
                case StarColor.Red:
                    break;
                case StarColor.White:
                    break;
                case StarColor.Yellow:
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}
