using System;
using System.Collections.Generic;
using BLL.Engine.BaseClasses;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Enums;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet
{
    public class SocialPerformer : BasePerformer,IPerformer
    {
        public SocialPerformer(PlanetDto planetDto, PlanetUpdateSelector chosenUpdate, RaceDto raceDto,
            List<TechnologyDto> technologyDtos,
            DateTime timenow)
            : base(planetDto, chosenUpdate, raceDto, technologyDtos, timenow)
        {
            
        }

        protected override void RetrieveUpdater()
        {
            throw new NotImplementedException();
        }

        public bool Perform()
        {
            RetrieveUpdater();
            return false;
        }
    }
}