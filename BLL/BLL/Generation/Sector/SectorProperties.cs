using BLL.Generation.Sector.Enums;

namespace BLL.Generation.Sector
{
    public static class SectorProperties
    {
        public static int RegionA { get; } = 300;
        public static int RegionB { get; } = 500;
        public static int RegionC { get; } = 800;
        public static int RegionD { get; } = 1300;

        public static SectorRegion WhereAmI(int maxRangeX, int maxRangeY)
        {
            if (maxRangeX <= RegionA && maxRangeY <= RegionA) return SectorRegion.Centre;
            if ((maxRangeX > RegionA && maxRangeX <= RegionB) || (maxRangeY > RegionA && maxRangeY <= RegionB))
                return SectorRegion.Average;
            if((maxRangeX > RegionB && maxRangeX <= RegionC) || (maxRangeY > RegionB && maxRangeY <= RegionC))
                return SectorRegion.JustOutside;
            return SectorRegion.FarAway;
        }
    }
}