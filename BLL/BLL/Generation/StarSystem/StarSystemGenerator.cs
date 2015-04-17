using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Generation.StarSystem
{
    public sealed class StarSystemGenerator
    {
        private StarGenerator _StarGenerator;
        private PlanetGenerator _PlanetGenerator;
        private StarPlacer _StarPlacer;
        private bool _ForceLiving;
        private bool _ForceWater;
        private bool _MostlyWater;
        private bool _MineralProductionRich;
        private bool _MineralProductionPoor;
        private bool _FoodProductionRich;
        private bool _FoodProductionPoor;
        private int _MinX;
        private int _MaxX;
        private int _MinY;
        private int _MaxY;
        private DoubleRange _CloseRange = new DoubleRange(0.1, 0.7);
        private static Random _Rnd;

        public StarSystemGenerator(StarGenerator starGenerator,
            StarPlacer starPlacer, bool forceLiving, bool forceWater, bool mostlyWater
            , int minX, int maxX, int minY, int maxY,
            bool mineralProductionRich, bool mineralProductionPoor,
            bool foodProductionRich, bool foodProductionPoor)
        {
            this._StarGenerator = starGenerator;
            this._StarPlacer = starPlacer;
            this._ForceLiving = forceLiving;
            this._ForceWater = forceWater;
            this._MostlyWater = mostlyWater;

            this._MinX = minX;
            this._MaxX = maxX;
            this._MinY = minY;
            this._MaxY = maxY;

            this._MineralProductionPoor = mineralProductionPoor;
            this._MineralProductionRich = mineralProductionRich;
            this._FoodProductionPoor = foodProductionPoor;
            this._FoodProductionRich = foodProductionRich;


            _Rnd = new Random();
        }
        /// <summary>
        /// Determine the probability range of planet existence, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private IntRange CalculatePlanetProbability(Star star)
        {
            IntRange myRange = new IntRange();
            myRange.Min = 0;
            switch (star.StarColor)
            {
                case Models.Universe.Enum.StarColor.Blue:
                    myRange.Max = 5;
                    break;
                case Models.Universe.Enum.StarColor.Orange:
                    myRange.Max = 60;
                    break;
                case Models.Universe.Enum.StarColor.Red:
                    myRange.Max = 50;
                    break;
                case Models.Universe.Enum.StarColor.White:
                    myRange.Max = 20;
                    break;
                case Models.Universe.Enum.StarColor.Yellow:
                    myRange.Max = 70;
                    break;
                default:
                    break;
            }
            return myRange;
        }
        /// <summary>
        /// Determine if there are planets
        /// </summary>
        /// <param name="planetProb"></param>
        /// <returns></returns>
        private bool HasPlanets(IntRange planetProb)
        {
            int seed = RandomNumbers.RandomInt(0, 100, _Rnd);
            return (seed <= planetProb.Max) ? true : false;
        }
        /// <summary>
        /// Determine the number of present planets
        /// </summary>
        /// <param name="planetRage"></param>
        /// <returns></returns>
        private int CalculateNumberOfPlanets(IntRange planetRage)
        {
            int result = 0;
            using (ScaleConversion conversion = new ScaleConversion(100, (planetRage.Max - planetRage.Min)))
            {
                result = (int)conversion.Convert(RandomNumbers.RandomInt(planetRage.Min, planetRage.Max, _Rnd));
            }
            return result;
        }
        /// <summary>
        /// Determine the max number of possible plantes, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private IntRange CalculateMaxNumberOfPlanet(Star star)
        {
            IntRange myRange = new IntRange();
            myRange.Min = 0;

            switch (star.StarColor)
            {
                case Models.Universe.Enum.StarColor.Blue:
                case Models.Universe.Enum.StarColor.White:
                    myRange.Max = 2;
                    break;
                case Models.Universe.Enum.StarColor.Orange:
                case Models.Universe.Enum.StarColor.Yellow:
                    myRange.Max = 9;
                    break;
                case Models.Universe.Enum.StarColor.Red:
                    myRange.Max = 5;
                    break;
                default:
                    myRange.Max = 1;
                    break;
            }
            return myRange;
        }
        /// <summary>
        /// Generate a RandomPlanet
        /// </summary>
        /// <param name="star"></param>
        /// <param name="forceLiving"></param>
        /// <param name="forceWater"></param>
        /// <param name="mostlyWater"></param>
        /// <param name="mineralProductionRich"></param>
        /// <param name="foodProductionRich"></param>
        /// <param name="mineralProductionPoor"></param>
        /// <param name="foodProductionPoor"></param>
        private void GeneratePlanet(Star star, bool forceLiving, bool forceWater, bool mostlyWater, bool mineralProductionRich, bool foodProductionRich, bool mineralProductionPoor, bool foodProductionPoor)
        {
            using (PlanetGenerator generator = new PlanetGenerator(star, forceLiving, forceWater, mostlyWater, mineralProductionRich, foodProductionRich, mineralProductionPoor, foodProductionPoor))
            {
                Planet habitablePlanet = generator.CreateBrandNewPlanet();
                if (habitablePlanet != null)
                {
                    using (OrbitGenerator orbigGenerator = new OrbitGenerator(star, habitablePlanet.Mass, habitablePlanet.Radius, _CloseRange, _ForceLiving))
                    {
                        generator.CompletePlanetGeneration(habitablePlanet, orbigGenerator, _CloseRange);
                    }
                    star.Planets.Add(habitablePlanet);
                }
            }
        }
        /// <summary>
        /// This Generate a complete new star system
        /// </summary>
        /// <returns></returns>
        public Star Generate()
        {
            Star star = this._StarGenerator.CreateBrandNewStar();
            _StarPlacer.Place(star, this._MinX, this._MaxX, this._MinY, this._MaxY);

            IntRange maxNumberOfPlanets = this.CalculateMaxNumberOfPlanet(star);
            IntRange planetProbability = this.CalculatePlanetProbability(star);

            if (this.HasPlanets(planetProbability) || _ForceLiving)
            {
                int numberOfPlanets = this.CalculateNumberOfPlanets(maxNumberOfPlanets);
                if (_ForceLiving)
                {
                    this.GeneratePlanet(star, _ForceLiving, _ForceWater, _MostlyWater, _MineralProductionRich, _FoodProductionRich, _MineralProductionPoor, _FoodProductionPoor);
                    numberOfPlanets = (numberOfPlanets - 1 < 0) ? 0 : numberOfPlanets - 1;
                }
                for (int index = 0; index < numberOfPlanets; index++)
                {
                    this.GeneratePlanet(star, false, false, false, false, false, false, false);
                }
            }
            return star;
        }
    }
}
