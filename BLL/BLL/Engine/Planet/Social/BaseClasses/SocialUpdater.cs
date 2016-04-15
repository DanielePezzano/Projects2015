using System;
using System.Collections.Generic;
using BLL.Engine.Planet.BaseClasses;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Social.BaseClasses
{
    public abstract class SocialUpdater : BaseUpdater
    {
        protected SocialUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDto, DateTime nowTime)
            : base(referredPlanetDto, raceDto, technologyDto, nowTime)
        {
            
        }

        #region Private MEthods

        #endregion

        protected override void AdjustByActivePopulation()
        {
            throw new NotImplementedException();
        }

        protected override double AdjustByStatus(double quantityToAdjust, bool increaseOnOptimum = true)
        {
            throw new NotImplementedException();
        }
        
        protected abstract void AdjustByBuildings();
        protected abstract void AdjustByTechnology();
        protected abstract void AdjustBySocial();
    }
}