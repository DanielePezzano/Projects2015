using System;
using System.Collections.Generic;
using BLL.Utilities;
using SharedDto.Universe.Sector;
using System.Linq;
using BLL.Generation.StarSystem.IstanceFactory;
using DAL.Operations.IstanceFactory;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;

namespace BLL.Generation.Sector
{
    public class GenerateSector
    {
        private readonly int _alreadyPresentStars;
        private readonly Random _rnd;
        private SystemGenerationDto _systemGenerationDto;
        private int _generatedStars;
        private int _generatedPlanets;
        private int _habitabilePlanets;
        private OpFactory _opFactory;

        public GenerateSector(int alreadyPresentStars, Random rnd, SystemGenerationDto systemGenerationDto, OpFactory opFactory)
        {
            _alreadyPresentStars = alreadyPresentStars;
            _systemGenerationDto = systemGenerationDto;
            _rnd = rnd;
            _opFactory = opFactory;
        }

        #region Private Methods
        private void ResetCounters()
        {
            _generatedStars = 0;
            _generatedPlanets = 0;
            _habitabilePlanets = 0;
        }

        private static SectorGenerationDto RetrieveSectorGenerationDto(int maxStars, SectorGenerationResult result,
            int habPlanet, int totStarsAdded, int totPlanetsAdded,List<StarDto> generatedStars )
        {
            return new SectorGenerationDto()
            {
                MaxStarsAllowed = maxStars,
                GenerationResult = result,
                HabitablePlanetsAdded = habPlanet,
                StarsGenerated = totStarsAdded,
                TotalPlanetsAdded = totPlanetsAdded,
                GeneratedStarsList = generatedStars
            };
        }

        private SectorGenerationDto RetrieveResultDto(int starToGenerate,int maxStars)
        {
            var result = SectorGenerationResult.NoStarCreated;
            if (starToGenerate <= 0)
                return RetrieveSectorGenerationDto(maxStars, result, _habitabilePlanets,
                    _generatedStars, _generatedPlanets, new List<StarDto>());

            result = SectorGenerationResult.StarsAdded;
            
            return RetrieveSectorGenerationDto(maxStars, result, _habitabilePlanets,
                _generatedStars, _generatedPlanets, GenerateSystems(starToGenerate));
        }

        private List<StarDto> GenerateSystems(int starToGenerate)
        {
            var generatedStars = new List<StarDto>();

            for (var i = 0; i < starToGenerate; i++)
            {
                var systemGenerator = FactoryGenerator.RetrieveStarSystemGenerator(_rnd, _systemGenerationDto, _opFactory);
                var starToAdd = systemGenerator.Generate(_rnd, "");
                if (starToAdd == null) continue;

                generatedStars.Add(starToAdd);
                _generatedPlanets += starToAdd.Planets.Count;
                _habitabilePlanets += starToAdd.Planets.Count(c => c.HabitableSpaces>0);
            }

            return generatedStars;
        } 
        #endregion

        public SectorGenerationDto Generate(bool isTest=false)
        {
            ResetCounters();
            var whereAmI = SectorProperties.WhereAmI(_systemGenerationDto.MaxX,_systemGenerationDto.MaxY);
            var maxStars = SectorProperties.RetrieveMaxNumberOfStars(whereAmI);

            if (_alreadyPresentStars >= maxStars)
                return RetrieveSectorGenerationDto(maxStars, SectorGenerationResult.MaxStarReached, 0, 0, 0,
                    new List<StarDto>());

            var maxGenerableStars = maxStars - _alreadyPresentStars;
            var starToGenerate = RandomNumbers.RandomInt(0, maxGenerableStars, _rnd);

            return RetrieveResultDto(starToGenerate, maxStars);
        }

        public SectorGenerationDto GenerateSpecificStar()
        {
            ResetCounters();
            var whereAmI = SectorProperties.WhereAmI(_systemGenerationDto.MaxX, _systemGenerationDto.MaxY);
            var maxStars = SectorProperties.RetrieveMaxNumberOfStars(whereAmI);

            if (_alreadyPresentStars >= maxStars)
                return RetrieveSectorGenerationDto(maxStars, SectorGenerationResult.MaxStarReached, 0, 0, 0,
                    new List<StarDto>());

            return RetrieveResultDto(1, maxStars);
        }
        
    }
}
