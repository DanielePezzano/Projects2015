using System;
using System.Configuration;
using BLL.Generation.Sector.Enums;

namespace BLL.Generation.Sector
{
    public static class SectorProperties
    {
        public static int RegionA { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["SectorRegionA"]);
        public static int RegionB { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["SectorRegionB"]);
        public static int RegionC { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["SectorRegionC"]);
        public static int RegionD { get; } = Convert.ToInt32(ConfigurationManager.AppSettings["SectorRegionD"]);

        public static SectorRegion WhereAmI(int maxRangeX, int maxRangeY)
        {
            if (maxRangeX <= RegionA && maxRangeY <= RegionA) return SectorRegion.Centre;
            if ((maxRangeX > RegionA && maxRangeX <= RegionB) || (maxRangeY > RegionA && maxRangeY <= RegionB))
                return SectorRegion.Average;
            if ((maxRangeX > RegionB && maxRangeX <= RegionC) || (maxRangeY > RegionB && maxRangeY <= RegionC))
                return SectorRegion.JustOutside;
            return SectorRegion.FarAway;
        }

        public static int RetrieveMaxNumberOfStars(SectorRegion sectorRegion)
        {
            switch (sectorRegion)
            {
                case SectorRegion.Centre:
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionA"]);
                    break;
                case SectorRegion.Average:
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionB"]);
                    break;
                case SectorRegion.JustOutside:
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionC"]);
                    break;
                case SectorRegion.FarAway:
                    return Convert.ToInt32(ConfigurationManager.AppSettings["MaxStarRegionD"]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(sectorRegion), sectorRegion, null);
            }
            return 0;
        }
    }
}