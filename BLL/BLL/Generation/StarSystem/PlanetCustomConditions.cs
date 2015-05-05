namespace BLL.Generation.StarSystem
{
    public sealed class PlanetCustomConditions
    {
        public PlanetCustomConditions()
        {
            ForceWater = false;
            FoodPoor = false;
            FoodRich = false;
            ForceLiving = false;
            MineralPoor = false;
            MineralRich = false;
            MostlyWater = false;
        }

        public bool ForceLiving { get; set; }
        public bool ForceWater { get; set; }
        public bool MineralRich { get; set; }
        public bool MineralPoor { get; set; }
        public bool FoodRich { get; set; }
        public bool FoodPoor { get; set; }
        public bool MostlyWater { get; set; }
    }
}