using System;
using System.Collections.Generic;
using BLL.Engine.Interfaces;
using BLL.Engine.Planet.Social.BaseClasses;
using BLL.Engine.Planet.Social.Interfaces;
using BLL.Engine.Planet.Structs;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace BLL.Engine.Planet.Social
{
    public class PopulationUpdater : SocialUpdater, ISocialUpdater, IUpdater
    {

        public bool UpdateToDo { get; set; }
        public StatusCheckResult ConsistencyCheckPop { get; set; }

        public PopulationUpdater(PlanetDto referredPlanetDto, RaceDto raceDto, List<TechnologyDto> technologyDtos, DateTime nowTime) :
            base(referredPlanetDto, raceDto, technologyDtos, nowTime)
        {

        }

        #region private methods

        private void DetermineBaseIncrement()
        {
            double oneThird = (double)ReferredPlanetDto.MaxPopulation/3;
            
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
        #endregion
        


        public void Update()
        {
            if (ConsistencyCheckPop.ConsistencyCheck == false || Product < 1) return;
            UpdateToDo = true;
        }

        public void CheckTimeDifference()
        {
            if (Diff.Hours <= 0) return;
            Product = Diff.Hours;
        }
    }
}