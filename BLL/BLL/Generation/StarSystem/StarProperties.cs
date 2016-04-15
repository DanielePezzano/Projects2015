using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe.Enum;

namespace BLL.Generation.StarSystem
{
    public static class StarProperties
    {
        public const int MinBaseRange = 0;
        public const int MaxBaseRange = 10;

        public static string DetermineStarColor(int seed)
        {
            if (seed >= 0 && seed <= 50) return StarColor.Red.ToString();
            if (seed >= 51 && seed <= 71) return StarColor.Yellow.ToString();
            if (seed >= 72 && seed <= 92) return StarColor.Orange.ToString();
            if (seed >= 93 && seed <= 96) return StarColor.White.ToString();

            return StarColor.Blue.ToString();
        }

        public static string DetermineStarType(string color, int seed)
        {
            var result = StarType.Dwarf.ToString();
            var determiner = SwitchColorDeterminer(color);
            switch (determiner)
            {
                case 0:
                    result = ReturnBlueType(seed, result);
                    break;
                case 1:
                    result = ReturnRedType(seed, result);
                    break;
                case 2:
                    result = ReturnWhiteType(seed, result);
                    break;
                case 3:
                case 4:
                    result = ReturnHabType(seed, result);
                    break;
                default:
                    result = StarType.Dwarf.ToString();
                    break;
            }
            return result;
        }

        public static int SwitchColorDeterminer(string color)
        {
            if (StarColor.Blue.ToString() == color) return 0;
            if (StarColor.Red.ToString() == color) return 1;
            if (StarColor.White.ToString() == color) return 2;
            if (StarColor.Orange.ToString() == color) return 3;
            if (StarColor.Yellow.ToString() == color) return 4;
            return 5;
        }

        private static string ReturnHabType(int seed, string result)
        {
            if (seed <= 60) result = StarType.Dwarf.ToString();
            if (seed > 60 && seed <= 90) result = StarType.Giant.ToString();
            if (seed > 90) result = StarType.HyperGiant.ToString();
            return result;
        }

        private static string ReturnWhiteType(int seed, string result)
        {
            if (seed <= 80) result = StarType.Dwarf.ToString();
            if (seed > 80 && seed <= 90) result = StarType.Giant.ToString();
            if (seed > 90) result = StarType.HyperGiant.ToString();
            return result;
        }

        private static string ReturnRedType(int seed, string result)
        {
            if (seed <= 70) result = StarType.Dwarf.ToString();
            if (seed > 70 && seed <= 90) result = StarType.Giant.ToString();
            if (seed > 90) result = StarType.HyperGiant.ToString();
            return result;
        }

        private static string ReturnBlueType(int seed, string result)
        {
            if (seed <= 40) result = StarType.Dwarf.ToString();
            if (seed > 40 && seed <= 90) result = StarType.Giant.ToString();
            if (seed > 90) result = StarType.HyperGiant.ToString();
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
            double result;
            using (var rangeConverter = new RangeConversion(min, max, new ScaleConversion(MaxBaseRange-MinBaseRange, max - min)))
            {
                result = rangeConverter.DoConversion(seed);
            }
            return result < minResult ? minResult : result;
        }
        /// <summary>
        /// Based on starcolor and type, determine the surface temperature range
        /// </summary>
        /// <param name="color"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
        public static IntRange CalculateTemperatureRange(string color, string starType)
        {
            var result = UtilitiesFactory.RetrieveRange(2800, 3000);
            var determiner = SwitchColorDeterminer(color);

            switch (determiner)
            {
                case 0:
                    result = RetrieveIntRangeCalculation(starType, 
                        UtilitiesFactory.RetrieveRange(10000, 20000),
                        UtilitiesFactory.RetrieveRange(15000, 30000), UtilitiesFactory.RetrieveRange(25000, 40000));
                    break;
                case 1:
                    result = RetrieveIntRangeCalculation(starType,
                        UtilitiesFactory.RetrieveRange(2800, 3000),
                        UtilitiesFactory.RetrieveRange(3000, 3200), UtilitiesFactory.RetrieveRange(3200, 3700));
                    break;
                case 2:
                    result = RetrieveIntRangeCalculation(starType,
                        UtilitiesFactory.RetrieveRange(7500, 9000),
                        UtilitiesFactory.RetrieveRange(9000, 9500), UtilitiesFactory.RetrieveRange(9500, 12000));
                    break;
                case 3:
                    result = RetrieveIntRangeCalculation(starType,
                        UtilitiesFactory.RetrieveRange(3700, 4000),
                        UtilitiesFactory.RetrieveRange(4000, 5000), UtilitiesFactory.RetrieveRange(5000, 5200));
                    break;
                case 4:
                    result = RetrieveIntRangeCalculation(starType,
                        UtilitiesFactory.RetrieveRange(5200, 5500),
                        UtilitiesFactory.RetrieveRange(5500, 5800), UtilitiesFactory.RetrieveRange(5800, 6000));
                    break;
            }
            return result;
        }

        private static IntRange RetrieveIntRangeCalculation(string starType, IntRange dwarfRange,IntRange giantRange,IntRange hypergiantRange)
        {
            if (starType == StarType.Giant.ToString())return giantRange;
            if (starType == StarType.HyperGiant.ToString())return hypergiantRange;
            return dwarfRange;
        }

        /// <summary>
        /// Based on star color, determine the star radiation range
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static IntRange CalculateRadiationRange(string color)
        {
            var range = UtilitiesFactory.RetrieveRange(0,10);
            var determiner = SwitchColorDeterminer(color);
            switch (determiner)
            {
                case 0:
                    range.Min = 10; range.Max = 15;
                    break;
                case 1:
                    range.Min = 5; range.Max = 7;
                    break;
                case 2:
                    range.Min = 7; range.Max = 11;
                    break;
                case 3:
                case 4:
                    range.Min = 3; range.Max = 5;
                    break;
            }
            return range;
        }
        /// <summary>
        /// Based on star color and star type, determine the star mass range
        /// </summary>
        /// <param name="color"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
        private static DoubleRange CalculateMassRange(string starType, string color)
        {
            var result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(1.4, 2.1),
                        UtilitiesFactory.RetrieveDoubleRange(2.1, 10), UtilitiesFactory.RetrieveDoubleRange(10, 150));
            var determiner = SwitchColorDeterminer(color);
            switch (determiner)
            {
                case 0:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(90, 150),
                        UtilitiesFactory.RetrieveDoubleRange(150, 200), UtilitiesFactory.RetrieveDoubleRange(200, 300));
                    break;
                case 1:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(0.08, 0.5),
                        UtilitiesFactory.RetrieveDoubleRange(0.5, 10), UtilitiesFactory.RetrieveDoubleRange(10, 100));
                    break;
                case 2:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(1.4, 2.1),
                        UtilitiesFactory.RetrieveDoubleRange(2.1, 10), UtilitiesFactory.RetrieveDoubleRange(10, 150));
                    break;
                case 3:
                case 4:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(0.45, 0.8),
                        UtilitiesFactory.RetrieveDoubleRange(0.8, 10), UtilitiesFactory.RetrieveDoubleRange(10, 100));
                    break;
                
            }
            return result;
        }

        private static DoubleRange RetrieveDoubleCalculation(string starType, DoubleRange dwarfRange,DoubleRange giantRange,DoubleRange hyperGiantRange)
        {

            if (starType == StarType.Giant.ToString()) return giantRange;
            if (starType == StarType.HyperGiant.ToString()) return hyperGiantRange;
            return dwarfRange;
        }

        /// <summary>
        /// Based on star color and star type, determine the star radius range
        /// </summary>
        /// <param name="color"></param>
        /// <param name="starType"></param>
        /// <returns></returns>
        private static DoubleRange CalculateRadiusRange(string color, string starType)
        {
            var result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(1.4, 2.1),
                        UtilitiesFactory.RetrieveDoubleRange(2.1, 10), UtilitiesFactory.RetrieveDoubleRange(10, 150));
            var determiner = SwitchColorDeterminer(color);
            switch (determiner)
            {
                case 0:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(6.6, 18),
                        UtilitiesFactory.RetrieveDoubleRange(18, 50), UtilitiesFactory.RetrieveDoubleRange(50, 350));
                    break;
                case 3:
                case 4:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(0.45, 0.8),
                        UtilitiesFactory.RetrieveDoubleRange(0.8, 10), UtilitiesFactory.RetrieveDoubleRange(10, 100));
                    break;
                case 1:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(0.08, 0.5),
                        UtilitiesFactory.RetrieveDoubleRange(0.5, 10), UtilitiesFactory.RetrieveDoubleRange(10, 100));
                    break;
                case 2:
                    result = RetrieveDoubleCalculation(starType, UtilitiesFactory.RetrieveDoubleRange(1.4, 2.1),
                        UtilitiesFactory.RetrieveDoubleRange(2.1, 10), UtilitiesFactory.RetrieveDoubleRange(10, 150));
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
        public static int DetermineSurfaceTemp(string starColor, string starType, int seed)
        {
            var temperature = CalculateTemperatureRange(starColor, starType);
            return (int)CalculateResultInRange(seed, temperature.Min, temperature.Max, 2800);
        }

        /// <summary>
        /// Determine star radiation level
        /// </summary>
        /// <param name="color"></param>
        /// <param name="seed">number between 0,10</param>
        /// <returns></returns>
        public static int DetermineStarRadiation(string color, int seed)
        {
            var radiationRange = CalculateRadiationRange(color);
            return (int)CalculateResultInRange(seed, radiationRange.Min, radiationRange.Max, 3);
        }
        /// <summary>
        /// Determine STar Mass
        /// </summary>
        /// <param name="starType"></param>
        /// <param name="color"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static double DetermineStarMass(string starType, string color, int seed)
        {
            var mass = CalculateMassRange(starType, color);
            return CalculateResultInRange(seed, mass.Min, mass.Max, 1);
        }

        /// <summary>
        /// Determine StarRadius
        /// </summary>
        /// <param name="color"></param>
        /// <param name="starType"></param>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static double DetermineStarRadius(string color, string starType, int seed)
        {
            var radius = CalculateRadiusRange(color, starType);
            return CalculateResultInRange(seed, radius.Min, radius.Max, 1);
        }

    }
}
