using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe.Enum;

namespace BLL.Generation.StarSystem
{
    public static class StarProperties
    {
        public const int MinBaseRange = 0;
        public const int MaxBaseRange = 10;

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

        /// <summary>
        /// Given a minimum and maximum range
        /// </summary>
        /// <param name="seed"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="minResult"></param>
        /// <returns></returns>
        public static double CalculateResultInRange(int seed, double min, double max, int minResult)
        {
            double result = minResult;
            using (RangeConversion rangeConverter = new RangeConversion(MinBaseRange, MaxBaseRange, min, max, new ScaleConversion((MaxBaseRange-MinBaseRange), max - min)))
            {
                result = rangeConverter.DoConversion(seed);
            }
            return result;
        }
        /// <summary>
        /// Based on starcolor and type, determine the surface temperature range
        /// </summary>
        /// <param name="starcolor"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Based on star color, determine the star radiation range
        /// </summary>
        /// <param name="starcolor"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Based on star color and star type, determine the star mass range
        /// </summary>
        /// <param name="starcolor"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
        private static DoubleRange CalculateMassRange(StarColor starcolor, StarType starType)
        {
            DoubleRange result = new DoubleRange(0.08, 1.0);
            switch (starcolor)
            {
                case StarColor.Blue:
                    if (starType == StarType.Dwarf) { result.Min = 90; result.Max =150; }
                    if (starType == StarType.Giant) { result.Min = 150; result.Max = 200; }
                    if (starType == StarType.HyperGiant) { result.Min = 200; result.Max = 300; }
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    if (starType == StarType.Dwarf) { result.Min = 0.45; result.Max = 0.8; }
                    if (starType == StarType.Giant) { result.Min = 0.8; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 100; }
                    break;
                case StarColor.Red:
                    if (starType == StarType.Dwarf) { result.Min = 0.08; result.Max = 0.5; }
                    if (starType == StarType.Giant) { result.Min = 0.5; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 100; }
                    break;
                case StarColor.White:
                    if (starType == StarType.Dwarf) { result.Min = 1.4; result.Max = 2.1; }
                    if (starType == StarType.Giant) { result.Min = 2.1; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 150; }
                    break;                
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// Based on star color and star type, determine the star radius range
        /// </summary>
        /// <param name="starColor"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
        private static DoubleRange CalculateRadiusRange(StarColor starColor, StarType starType)
        {
            DoubleRange result = new DoubleRange(0.08, 1.0);
            switch (starColor)
            {
                case StarColor.Blue:
                    if (starType == StarType.Dwarf) { result.Min = 6.6; result.Max = 18; }
                    if (starType == StarType.Giant) { result.Min = 18; result.Max = 50; }
                    if (starType == StarType.HyperGiant) { result.Min = 50; result.Max = 350; }
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    if (starType == StarType.Dwarf) { result.Min = 0.45; result.Max = 0.8; }
                    if (starType == StarType.Giant) { result.Min = 0.8; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 100; }
                    break;
                case StarColor.Red:
                    if (starType == StarType.Dwarf) { result.Min = 0.08; result.Max = 0.5; }
                    if (starType == StarType.Giant) { result.Min = 0.5; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 100; }
                    break;
                case StarColor.White:
                    if (starType == StarType.Dwarf) { result.Min = 1.4; result.Max = 2.1; }
                    if (starType == StarType.Giant) { result.Min = 2.1; result.Max = 10; }
                    if (starType == StarType.HyperGiant) { result.Min = 10; result.Max = 150; }
                    break;
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// Determine Star SurfaceTemperature
        /// </summary>
        /// <param name="starColor"></param>
        /// <param name="starType"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static int DetermineSurfaceTemp(StarColor starColor, StarType starType, int seed)
        {
            IntRange temperature = CalculateTemperatureRange(starColor, starType);
            return (int)CalculateResultInRange(seed, temperature.Min, temperature.Max, 2800);
        }
        /// <summary>
        /// Determine star radiation level
        /// </summary>
        /// <param name="starColor"></param>
        /// <param name="seed">number between 0,10</param>
        /// <returns></returns>
        public static int DetermineStarRadiation(StarColor starColor, int seed)
        {
            IntRange radiationRange = CalculateRadiationRange(starColor);
            return (int)CalculateResultInRange(seed, radiationRange.Min, radiationRange.Max, 3);
        }
        /// <summary>
        /// Determine STar Mass
        /// </summary>
        /// <param name="starType"></param>
        /// <param name="starColor"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static double DetermineStarMass(StarType starType, StarColor starColor, int seed)
        {
            DoubleRange mass = CalculateMassRange(starColor, starType);
            return CalculateResultInRange(seed, mass.Min, mass.Max, 1);
        }
        /// <summary>
        /// Determine StarRadius
        /// </summary>
        /// <param name="starColor"></param>
        /// <param name="starType"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double DetermineStarRadius(StarColor starColor, StarType starType, int seed)
        {
            DoubleRange radius = CalculateRadiusRange(starColor, starType);
            return CalculateResultInRange(seed, radius.Min, radius.Max, 1);
        }

    }
}
