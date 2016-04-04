using System;
using BLL.Generation.StarSystem.Factories;
using BLL.Utilities;
using BLL.Utilities.Structs;
using SharedDto.Universe.Sector;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;
using System.Linq;

namespace BLL.Generation.Sector
{
    public class GenerateSector
    {
        private readonly int _alreadyPresentStars;
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private IUnitOfWork _uow;
        private readonly int _galaxyId;
        private readonly Random _rnd;

        private int _generatedStars;
        private int _generatedPlanets;
        private int _habitabilePlanets;

        public GenerateSector(int alreadyPresentStars, int minX, int maxX, int minY, int maxY, IUnitOfWork uow, Random rnd,int galaxyId)
        {
            _alreadyPresentStars = alreadyPresentStars;
            _rangeX = FactoryGenerator.RetrieveIntRange(minX, maxX);
            _rangeY = FactoryGenerator.RetrieveIntRange(minY, maxY);
            _uow = uow;
            _rnd = rnd;
            _galaxyId = galaxyId;
        }

        #region Private Methods
        private void ResetCounters()
        {
            _generatedStars = 0;
            _generatedPlanets = 0;
            _habitabilePlanets = 0;
        }

        private static SectorGenerationDto RetrieveSectorGenerationDto(int maxStars, SectorGenerationResult result,
            int habPlanet, int totStarsAdded, int totPlanetsAdded)
        {
            return new SectorGenerationDto()
            {
                MaxStarsAllowed = maxStars,
                GenerationResult = result,
                HabitablePlanetsAdded = habPlanet,
                StarsGenerated = totStarsAdded,
                TotalPlanetsAdded = totPlanetsAdded
            };
        }

        private SectorGenerationDto RetrieveResultDto(bool isTest, int starToGenerate,int maxStars)
        {
            var result = SectorGenerationResult.NoStarCreated;
            if (starToGenerate <= 0)
                return RetrieveSectorGenerationDto(maxStars, result, _habitabilePlanets,
                    _generatedStars, _generatedPlanets);

            result = SectorGenerationResult.StarsAdded;
            GenerateSystems(isTest, starToGenerate);
            return RetrieveSectorGenerationDto(maxStars, result, _habitabilePlanets,
                _generatedStars, _generatedPlanets);
        }

        private void GenerateSystems(bool isTest, int starToGenerate)
        {
            for (var i = 0; i < starToGenerate; i++)
            {
                var systemGenerator = FactoryGenerator.RetrieveStarSystemGenerator(
                    FactoryGenerator.RetrieveConditions(false, false, false, false, false, false, false),
                    _rnd,
                    _uow,
                    _rangeX,
                    _rangeY
                    );
                var starToAdd = systemGenerator.Generate(_rnd, (MainUow)_uow, "");
                if (starToAdd == null) continue;

                if (!isTest) systemGenerator.WriteToRepository((MainUow)_uow, starToAdd, _galaxyId);
                _generatedStars += 1;
                _generatedPlanets += starToAdd.Planets.Count;
                _habitabilePlanets += starToAdd.Planets.Count(c => c.IsHabitable);
            }
        } 
        #endregion

        public SectorGenerationDto Generate(bool isTest=false)
        {
            ResetCounters();
            var whereAmI = SectorProperties.WhereAmI(_rangeX.Max, _rangeY.Max);
            var maxStars = SectorProperties.RetrieveMaxNumberOfStars(whereAmI);

            if (_alreadyPresentStars >= maxStars) return RetrieveSectorGenerationDto(maxStars, SectorGenerationResult.MaxStarReached, 0, 0, 0);

            var maxGenerableStars = maxStars - _alreadyPresentStars;
            var starToGenerate = RandomNumbers.RandomInt(0, maxGenerableStars, _rnd);

            return RetrieveResultDto(isTest, starToGenerate, maxStars);
        }

        
    }
}
