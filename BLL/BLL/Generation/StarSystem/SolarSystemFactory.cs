using System;
using System.Collections.Generic;
using BLL.Generation.StarSystem.Builders;
using BLL.Generation.StarSystem.Interfaces;
using BLL.Generation.StarSystem.IstanceFactory;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Stars;
using SharedDto.UtilityDto;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public class SolarSystemFactory
    {
        private readonly SystemGenerationDto _conditions;
        private StarDto _associatedStar;
        private IBuilder _myPlanetBuilder;
        private IBuilder _mySatBuilder;
        private readonly Random _rnd;
        private OrbitGenerator _orbitGenerator;
        private readonly DoubleRange _closeRange = new DoubleRange(0.1, 0.7);
        private int _numberOfPlanets;
        private List<PlanetDto> _generatedPlanets;

        public SolarSystemFactory(StarDto associatedStar, SystemGenerationDto systemGenerationDto, Random rnd, OrbitGenerator generator, int numberOfPlanets)
        {
            _conditions = systemGenerationDto;
            _associatedStar = associatedStar;
            _rnd = rnd;
            _numberOfPlanets = numberOfPlanets;
            _orbitGenerator = generator;
            
        }

        public SolarSystemFactory(SystemGenerationDto systemGenerationDto, Random rnd)
        {
            _conditions = systemGenerationDto;
            _rnd = rnd;
        }

        #region Private Methods

        private static int CalculateNumberOfSatellite(double mass, Random rnd, double satelliteFactor = 0.1)
        {
            var maxNumb = 0;
            if (mass < BasicConstants.MinMassForSatellite) return maxNumb;

            if (mass >= BasicConstants.MinMassForSatellite && mass <= 2*BasicConstants.EarthMass) maxNumb = 2;
            if (mass > 2*BasicConstants.EarthMass) maxNumb = (int) (satelliteFactor*mass);
            return RandomNumbers.RandomInt(0, maxNumb, rnd);
        }

        private void AddSatellites(PlanetDto planet)
        {
            var nos = CalculateNumberOfSatellite(planet.Mass, _rnd);
            for (var i = 0; i < nos; i++)
            {
                _mySatBuilder = FactoryGenerator.RetrievePlanetBuilder();
                var toAdd =
                    _mySatBuilder.Build(_associatedStar,
                        FactoryGenerator.RetrieveSystemGenerationDto(false, false, false, false, false, false, false, 0,
                            0, 0, 0), _rnd, _orbitGenerator, _closeRange);
                planet.Satellites.Add(toAdd);
            }
        }

        private void AddPlanets()
        {
            _generatedPlanets = new List<PlanetDto>();

            for (var x = 0; x < _numberOfPlanets; x++)
            {
                _myPlanetBuilder = FactoryGenerator.RetrievePlanetBuilder();
                var toAdd =
                    _myPlanetBuilder.Build(_associatedStar, _conditions, _rnd, _orbitGenerator, _closeRange);
                _orbitGenerator.AssignPlanetRadius(toAdd.Radius);
                AddSatellites(toAdd);
                _generatedPlanets.Add(toAdd);
                ResetForcingConditions();
            }

            _associatedStar.Planets = _generatedPlanets;
        }

        private void ResetForcingConditions()
        {
            _conditions.ForceLiving = false;
            _conditions.FoodPoor = false;
            _conditions.FoodRich = false;
            _conditions.FoodRich = false;
            _conditions.ForceWater = false;
            _conditions.MineralPoor = false;
            _conditions.MineralRich = false;
            _conditions.MostlyWater = false;
            _conditions.IsHomePlanet = false;
        }

        private void CalculateNumberOfPlanets(StarBuilder starGenerator)
        {
            _numberOfPlanets = starGenerator.CalculateNumberOfPlanets(starGenerator.CalculateMaxNumberOfPlanet(_associatedStar),
                _rnd, _conditions);
        }

        #endregion
        

        public List<PlanetDto> RetrievePlanets()
        {
            return _generatedPlanets;
        }

        public StarDto Constuct(StarBuilder starGenerator, StarPlacer starPlacer, IntRange rangeX, IntRange rangeY,int galaxyId, IUnitOfWork uow = null)
        {
            if (_conditions == null) throw new NullReferenceException("_Conditions must have a value");

            _associatedStar = starGenerator.CreateBrandNewStar(galaxyId);
            _orbitGenerator = FactoryGenerator.RetrieveOrbitGenerator(_associatedStar, _closeRange, _conditions);

            starPlacer.Place(_associatedStar, rangeX, rangeY, _rnd, uow);

            if (!starGenerator.HasPlanets(starGenerator.CalculatePlanetProbability(_associatedStar), _rnd) && !
                (_conditions.ForceLiving || _conditions.ForceWater || _conditions.MostlyWater)) return _associatedStar;

            CalculateNumberOfPlanets(starGenerator);

            AddPlanets();
            return _associatedStar;
        }

        
    }
}