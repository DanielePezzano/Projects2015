using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation.StarSystem
{
    public sealed class StarSystemGenerator
    {
        private StarGenerator _StarGenerator;
        private StarPlacer _StarPlacer;
        private PlanetCustomConditions _Conditions = null;
        private IntRange _RangeX;
        private IntRange _RangeY;
        private DoubleRange _CloseRange = new DoubleRange(0.1, 0.7);

        public StarSystemGenerator(
            StarGenerator starGenerator,
            StarPlacer starPlacer,
            IntRange rangeX,
            IntRange rangeY,
            PlanetCustomConditions conditions
            )
        {
            this._StarGenerator = starGenerator;
            this._StarPlacer = starPlacer;
            this._RangeX = rangeX;
            this._RangeY = rangeY;
            if (conditions == null)
                throw new ArgumentNullException("conditions");
            this._Conditions = conditions;
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
        private bool HasPlanets(IntRange planetProb, Random _Rnd)
        {
            int seed = RandomNumbers.RandomInt(0, 100, _Rnd);
            return (seed <= planetProb.Max) ? true : false;
        }
        /// <summary>
        /// Determine the number of present planets
        /// </summary>
        /// <param name="planetRage"></param>
        /// <returns></returns>
        private int CalculateNumberOfPlanets(IntRange planetRage, Random _Rnd)
        {
            int result = 0;
            using (ScaleConversion conversion = new ScaleConversion(100, (planetRage.Max - planetRage.Min)))
            {
                result = (int)conversion.Convert(RandomNumbers.RandomInt(0, 100, _Rnd));
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
        private void GeneratePlanet(Star star, PlanetCustomConditions conditions, Random _Rnd)
        {
            using (PlanetGenerator generator = new PlanetGenerator(star, conditions))
            {
                Planet habitablePlanet = generator.CreateBrandNewPlanet(_Rnd);
                if (habitablePlanet != null)
                {
                    using (OrbitGenerator orbigGenerator = new OrbitGenerator(star, habitablePlanet.Mass, habitablePlanet.Radius, _CloseRange, _Conditions.ForceLiving))
                    {
                        generator.CompletePlanetGeneration(habitablePlanet, orbigGenerator, _CloseRange, _Rnd);
                    }
                    star.Planets.Add(habitablePlanet);
                }
            }
        }
        /// <summary>
        /// this Generate a completly new star system
        /// </summary>
        /// <param name="_Rnd">Random Seeder</param>
        /// <param name="cacheKey">Placer query cachekey</param>
        /// <returns></returns>
        public Star Generate(Random _Rnd, string cacheKey)
        {
            Star star = this._StarGenerator.CreateBrandNewStar();
            _StarPlacer.Place(star, this._RangeX, this._RangeY, _Rnd, cacheKey);

            IntRange maxNumberOfPlanets = this.CalculateMaxNumberOfPlanet(star);
            IntRange planetProbability = this.CalculatePlanetProbability(star);

            if (this.HasPlanets(planetProbability, _Rnd) || _Conditions.ForceLiving)
            {
                int numberOfPlanets = this.CalculateNumberOfPlanets(maxNumberOfPlanets, _Rnd);
                if (_Conditions.ForceLiving)
                {
                    this.GeneratePlanet(star, _Conditions, _Rnd);
                    numberOfPlanets = (numberOfPlanets - 1 < 0) ? 0 : numberOfPlanets - 1;
                }
                for (int index = 0; index < numberOfPlanets; index++)
                {
                    this.GeneratePlanet(star, new PlanetCustomConditions(), _Rnd);
                }
            }
            return star;
        }
        /// <summary>
        /// Genera ed inserisce nel db un nuovo sistema solare
        /// </summary>
        /// <param name="_rnd"></param>
        /// <param name="uow"></param>
        /// <param name="galaxy"></param>
        /// <param name="cacheKey"></param>
        public void GenerateAndInsert(Random _rnd, MainUow uow, Galaxy galaxy, string cacheKey)
        {
            Star generatedStarSystem = this.Generate(_rnd, cacheKey);
            if (generatedStarSystem != null)
            {
                generatedStarSystem.Galaxy = galaxy;
                uow.StarRepository.Add(generatedStarSystem);
                foreach (Planet planet in generatedStarSystem.Planets)
                {
                    uow.PlanetRepository.Add(planet);
                    foreach (Satellite satellite in planet.Satellites)
                    {
                        uow.SatelliteRepository.Add(satellite);
                    }
                }
                uow.Save();
            }
        }
    }
}
