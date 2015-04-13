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
                    if (seed >40 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.Red:
                    if (seed <= 70) result = StarType.Dwarf;
                    if (seed >70 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.White:
                    if (seed <= 80) result = StarType.Dwarf;
                    if (seed >80 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    if (seed <= 60) result = StarType.Dwarf;
                    if (seed >60 && seed <= 90) result = StarType.Giant;
                    if (seed > 90) result = StarType.HyperGiant;
                    break;
                default:
                    result = StarType.Dwarf;
                    break;
            }
            return result;
        }

        public static int DetermineSurfaceTemp(StarColor starColor, StarType starType, Random rnd)
        {
            int result = 2800;
            switch (starColor)
            {
                case StarColor.Blue:
                    if (starType == StarType.Dwarf)
                    {
                        result = rnd.Next(10000, 20000);
                    }
                    if (starType == StarType.Giant)
                    {
                        result = rnd.Next(10000, 20000);
                    }
                    if (starType == StarType.HyperGiant)
                    {
                        result = rnd.Next(20000, 40000);
                    }
                    break;
                case StarColor.Orange:
                    if (starType == StarType.Dwarf)
                    {
                        result = rnd.Next(3700, 4000);
                    }
                    if (starType == StarType.Giant)
                    {
                        result = rnd.Next(4000, 5000);
                    }
                    if (starType == StarType.HyperGiant)
                    {
                        result = rnd.Next(5000, 5200);
                    }
                    break;
                case StarColor.Red:
                    if (starType == StarType.Dwarf)
                    {
                        result = rnd.Next(2800, 3000);
                    }
                    if (starType == StarType.Giant)
                    {
                        result = rnd.Next(3000, 3200);
                    }
                    if (starType == StarType.HyperGiant)
                    {
                        result = rnd.Next(3200, 3700);
                    }
                    break;
                case StarColor.White:
                    if (starType == StarType.Dwarf)
                    {
                        result = rnd.Next(7500, 9000);
                    }
                    if (starType == StarType.Giant)
                    {
                        result = rnd.Next(9000, 9500);
                    }
                    if (starType == StarType.HyperGiant)
                    {
                        result = rnd.Next(9500, 10000);
                    }
                    break;
                case StarColor.Yellow:
                    if (starType == StarType.Dwarf)
                    {
                        result = rnd.Next(5200, 5500);
                    }
                    if (starType == StarType.Giant)
                    {
                        result = rnd.Next(5500, 5800);
                    }
                    if (starType == StarType.HyperGiant)
                    {
                        result = rnd.Next(5800, 6000);
                    }
                    break;
                default:
                    break;
            }
            return result;
        }

        public static int DetermineStarRadiation(StarColor starColor, Random rnd)
        {
            int result = 3;
            switch (starColor)
            {
                case StarColor.Blue:
                    result = rnd.Next(10, 15);
                    break;
                case StarColor.Orange:
                    result = rnd.Next(3, 5);
                    break;
                case StarColor.Red:
                    result = rnd.Next(5, 7);
                    break;
                case StarColor.White:
                    result = rnd.Next(7, 10);
                    break;
                case StarColor.Yellow:
                    result = rnd.Next(3, 4);
                    break;
                default:
                    break;
            }
            return result;
        }

        public static double DetermineStarMass(StarType starType, StarColor starColor, int p)
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
