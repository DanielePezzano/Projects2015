using System;
using System.Collections.Generic;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Social.BaseClasses;
using BLL.Engine.Planet.Social.Interfaces;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Social
{
    public class PopulationUpdater : SocialUpdater, ISocialUpdater, IUpdater
    {

        public bool UpdateToDo { get; set; }

        public PopulationUpdater(PlanetDto referredPlanetDto,RaceDto raceDto, List<TechnologyDto> technologyDtos,DateTime nowTime ):base
            (referredPlanetDto,raceDto,technologyDtos,nowTime)
        {
            
        }

        #region private methods
        #endregion

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void CheckTimeDifference()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustByBuildings()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustBySocial()
        {
            throw new NotImplementedException();
        }

        protected override void AdjustByTechnology()
        {
            throw new NotImplementedException();
        }
    }
}