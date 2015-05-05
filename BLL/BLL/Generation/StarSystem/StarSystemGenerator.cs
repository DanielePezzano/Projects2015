using System;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;
using Models.Universe.Enum;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation.StarSystem
{
    public sealed class StarSystemGenerator
    {
        private readonly DoubleRange _closeRange = new DoubleRange(0.1, 0.7);
        private readonly PlanetCustomConditions _conditions;
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private readonly StarGenerator _starGenerator;
        private readonly StarPlacer _starPlacer;

        public StarSystemGenerator(
            StarGenerator starGenerator,
            StarPlacer starPlacer,
            IntRange rangeX,
            IntRange rangeY,
            PlanetCustomConditions conditions
            )
        {
            _starGenerator = starGenerator;
            _starPlacer = starPlacer;
            _rangeX = rangeX;
            _rangeY = rangeY;
            if (conditions == null)
                throw new ArgumentNullException("conditions");
            _conditions = conditions;
        }

        /// <summary>
        ///     Determine the probability range of planet existence, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private IntRange CalculatePlanetProbability(Star star)
        {
            var myRange = new IntRange {Min = 0};
            switch (star.StarColor)
            {
                case StarColor.Blue:
                    myRange.Max = 5;
                    break;
                case StarColor.Orange:
                    myRange.Max = 60;
                    break;
                case StarColor.Red:
                    myRange.Max = 50;
                    break;
                case StarColor.White:
                    myRange.Max = 20;
                    break;
                case StarColor.Yellow:
                    myRange.Max = 70;
                    break;
            }
            return myRange;
        }

        /// <summary>
        ///     Determine if there are planets
        /// </summary>
        /// <param name="planetProb"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private bool HasPlanets(IntRange planetProb, Random rnd)
        {
            var seed = RandomNumbers.RandomInt(0, 100, rnd);
            return (seed <= planetProb.Max);
        }

        /// <summary>
        ///     Determine the number of present planets
        /// </summary>
        /// <param name="planetRage"></param>
        /// <param name="rnd"></param>
        /// <returns></returns>
        private static int CalculateNumberOfPlanets(IntRange planetRage, Random rnd)
        {
            int result;
            using (var conversion = new ScaleConversion(100, (planetRage.Max - planetRage.Min)))
            {
                result = (int) conversion.Convert(RandomNumbers.RandomInt(0, 100, rnd));
            }
            return result;
        }

        /// <summary>
        ///     Determine the max number of possible plantes, based on the star color
        /// </summary>
        /// <param name="star"></param>
        /// <returns></returns>
        private IntRange CalculateMaxNumberOfPlanet(Star star)
        {
            var myRange = new IntRange {Min = 0};

            switch (star.StarColor)
            {
                case StarColor.Blue:
                case StarColor.White:
                    myRange.Max = 2;
                    break;
                case StarColor.Orange:
                case StarColor.Yellow:
                    myRange.Max = 9;
                    break;
                case StarColor.Red:
                    myRange.Max = 5;
                    break;
                default:
                    myRange.Max = 1;
                    break;
            }
            return myRange;
        }

        /// <summary>
        ///     Generate a RandomPlanet
        /// </summary>
        /// <param name="star"></param>
        /// <param name="conditions"></param>
        /// <param name="rnd"></param>
        private void GeneratePlanet(Star star, PlanetCustomConditions conditions, Random rnd)
        {
            using (var generator = new PlanetGenerator(star, conditions))
            {
                var habitablePlanet = generator.CreateBrandNewPlanet(rnd);
                if (habitablePlanet != null)
                {
                    using (
                        var orbigGenerator = new OrbitGenerator(star, habitablePlanet.Radius, _closeRange,
                            _conditions.ForceLiving))
                    {
                        generator.CompletePlanetGeneration(habitablePlanet, orbigGenerator, _closeRange, rnd);
                    }
                    star.Planets.Add(habitablePlanet);
                }
            }
        }

        /// <summary>
        ///     this Generate a completly new star system
        /// </summary>
        /// <param name="rnd">Random Seeder</param>
        /// <param name="cacheKey">Placer query cachekey</param>
        /// <returns></returns>
        public Star Generate(Random rnd, string cacheKey)
        {
            var star = _starGenerator.CreateBrandNewStar();
            _starPlacer.Place(star, _rangeX, _rangeY, rnd, cacheKey);

            var maxNumberOfPlanets = CalculateMaxNumberOfPlanet(star);
            var planetProbability = CalculatePlanetProbability(star);

            if (HasPlanets(planetProbability, rnd) || _conditions.ForceLiving)
            {
                var numberOfPlanets = CalculateNumberOfPlanets(maxNumberOfPlanets, rnd);
                if (_conditions.ForceLiving)
                {
                    GeneratePlanet(star, _conditions, rnd);
                    numberOfPlanets = (numberOfPlanets - 1 < 0) ? 0 : numberOfPlanets - 1;
                }
                for (var index = 0; index < numberOfPlanets; index++)
                {
                    GeneratePlanet(star, new PlanetCustomConditions(), rnd);
                }
            }
            return star;
        }

        /// <summary>
        ///     Genera ed inserisce nel db un nuovo sistema solare
        /// </summary>
        /// <param name="rnd"></param>
        /// <param name="uow"></param>
        /// <param name="galaxy"></param>
        /// <param name="cacheKey"></param>
        public void GenerateAndInsert(Random rnd, MainUow uow, Galaxy galaxy, string cacheKey)
        {
            var generatedStarSystem = Generate(rnd, cacheKey);
            if (generatedStarSystem != null)
            {
                generatedStarSystem.Galaxy = galaxy;
                uow.StarRepository.Add(generatedStarSystem);
                foreach (var planet in generatedStarSystem.Planets)
                {
                    uow.PlanetRepository.Add(planet);
                    foreach (var satellite in planet.Satellites)
                    {
                        uow.SatelliteRepository.Add(satellite);
                    }
                }
                uow.Save();
            }
        }
    }
}