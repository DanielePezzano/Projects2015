using System;
using BLL.Generation.StarSystem.Builders;
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
        private readonly StarBuilder _starGenerator;
        private readonly StarPlacer _starPlacer;

        public StarSystemGenerator(
            StarBuilder starGenerator,
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

        private void GeneratePlanets(Star star, PlanetCustomConditions conditions, Random rnd,int numberOfPlanetsToAdd)
        {
            var factory = new SolarSystemFactory(star, conditions, rnd, new OrbitGenerator(star,_closeRange), numberOfPlanetsToAdd);
            factory.Construct();
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

            var maxNumberOfPlanets = _starGenerator.CalculateMaxNumberOfPlanet(star);
            var planetProbability = _starGenerator.CalculatePlanetProbability(star);

            AddPlanets(rnd, planetProbability, maxNumberOfPlanets, star);
            return star;
        }

        private void AddPlanets(Random rnd, IntRange planetProbability, IntRange maxNumberOfPlanets, Star star)
        {
            if (!_starGenerator.HasPlanets(planetProbability, rnd) && !_conditions.ForceLiving) return;

            var numberOfPlanets = _starGenerator.CalculateNumberOfPlanets(maxNumberOfPlanets, rnd);
            if (_conditions.ForceLiving)
            {
                GeneratePlanets(star,_conditions,rnd,1);
                numberOfPlanets = (numberOfPlanets - 1 < 0) ? 0 : numberOfPlanets - 1;
            }
            GeneratePlanets(star, _conditions, rnd, numberOfPlanets);
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