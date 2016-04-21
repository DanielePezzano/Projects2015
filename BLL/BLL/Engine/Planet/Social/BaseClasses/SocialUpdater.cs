using System;
using System.Collections.Generic;
using BLL.Engine.Planet.BaseClasses;
using BLL.Engine.Planet.Structs;
using Models.Universe.Enum;
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

        #region Private Methods

        #endregion

        protected override void AdjustByActivePopulation()
        {
            throw new NotImplementedException();
        }

        protected override StatusCheckResult AdjustByStatus(double quantityToAdjust, bool increaseOnOptimum = true)
        {
            var result = new StatusCheckResult() { Value = quantityToAdjust };
            switch (ReferredPlanetDto.Status)
            {
                case SatelliteStatus.Uncolonizable:
                case SatelliteStatus.Uncolonized:
                case SatelliteStatus.Abandoned:
                    result.Value = 0;
                    break;
                case SatelliteStatus.Colonized:
                    result.ConsistencyCheck = true;
                    break;
                case SatelliteStatus.Blocked:
                    if (increaseOnOptimum) result.Value -= result.Value * 0.8;
                    else result.Value += result.Value * 0.8;
                    result.ConsistencyCheck = true;
                    break;
                case SatelliteStatus.Starvation:
                    if (increaseOnOptimum) result.Value -= result.Value * 0.5;
                    else result.Value += result.Value * 0.5;
                    result.ConsistencyCheck = true;
                    break;
                case SatelliteStatus.Revolt:
                    if (increaseOnOptimum) result.Value -= result.Value * 0.4;
                    else result.Value += result.Value * 0.4;
                    result.ConsistencyCheck = true;
                    break;
                case SatelliteStatus.Optimum:
                    if (increaseOnOptimum) result.Value += result.Value * 0.15;
                    else result.Value -= result.Value * 0.15;
                    result.ConsistencyCheck = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }
        
        protected abstract void AdjustByBuildings();
        protected abstract void AdjustByTechnology();
        protected abstract void AdjustBySocial();
    }
}