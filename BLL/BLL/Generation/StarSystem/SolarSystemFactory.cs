using System;
using System.Collections.Generic;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities;
using BLL.Utilities.Structs;
using Models.Universe;

namespace BLL.Generation.StarSystem
{
    public class SolarSystemFactory
    {
        private readonly PlanetCustomConditions _conditions;
        private Star _associatedStar;
        private IBuilder _myPlanetBuilder;
        private IBuilder _mySatBuilder;
        private Random _rnd;
        private OrbitGenerator _orbitGenerator;
        private readonly DoubleRange _closeRange = new DoubleRange(0.1, 0.7);
        private int _numberOfPlanets;
        private List<Planet> _generatedPlanets;

        public SolarSystemFactory(Star associatedStar, PlanetCustomConditions conditions, Random rnd, OrbitGenerator generator,int numberOfPlanets)
        {
            _conditions = conditions;
            _associatedStar = associatedStar;
            _rnd = rnd;
            _numberOfPlanets = numberOfPlanets;
            _orbitGenerator = generator;
            
        }

        public SolarSystemFactory(PlanetCustomConditions conditions, Random rnd)
        {
            _conditions = conditions;
            _rnd = rnd;
        }

        #region Private Methods

        private int CalculateNumberOfSatellite(double mass, Random rnd, double satelliteFactor = 0.1)
        {
            var maxNumb = 0;
            if (mass < BasicConstants.MinMassForSatellite) return maxNumb;

            if (mass >= BasicConstants.MinMassForSatellite && mass <= (2*BasicConstants.EarthMass)) maxNumb = 2;
            if (mass > (2*BasicConstants.EarthMass)) maxNumb = (int) (satelliteFactor*mass);
            return RandomNumbers.RandomInt(0, maxNumb, rnd);
        }

        private void AddSatellites(Planet planet)
        {
            var nos = CalculateNumberOfSatellite(planet.Mass, _rnd);
            for (var i = 0; i < nos; i++)
            {
                _mySatBuilder = new SatelliteBuilder();
                Satellite toAdd =
                    (Satellite)
                        _mySatBuilder.Build(_associatedStar, new PlanetCustomConditions(), _rnd, _orbitGenerator,
                            _closeRange);
                toAdd.Planet = planet;
                planet.Satellites.Add(toAdd);
            }
        }

        private void AddPlanets()
        {
            for (var x = 0; x < _numberOfPlanets; x++)
            {
                _myPlanetBuilder = new PlanetBuilder();
                var toAdd =
                    (Planet)
                        _myPlanetBuilder.Build(_associatedStar, _conditions, _rnd, _orbitGenerator, _closeRange);
                _orbitGenerator.AssignPlanetRadius(toAdd.Radius);
                AddSatellites(toAdd);
                if (_associatedStar.Planets != null) _associatedStar.Planets.Add(toAdd);
                _generatedPlanets.Add(toAdd);
                ResetForcingConditions();
            }
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
        }

        #endregion

        

        public List<Planet> RetrievePlanets()
        {
            return _generatedPlanets;
        }

        public void Construct()
        {
            _generatedPlanets = new List<Planet>();

            if (_associatedStar != null)
            {
                AddPlanets();
            }
        }

        public Star ConstuctWholeSystem(StarBuilder starGenerator, StarPlacer starPlacer, IntRange rangeX, IntRange rangeY, string cacheKey)
        {
            if (_conditions == null) throw new NullReferenceException("_Conditions must have a value");

            var star = starGenerator.CreateBrandNewStar();
            _associatedStar = star;
            _orbitGenerator = new OrbitGenerator(star, _closeRange, _conditions.ForceLiving);

            starPlacer.Place(star, rangeX, rangeY, _rnd, cacheKey);

            if (!starGenerator.HasPlanets(starGenerator.CalculatePlanetProbability(star), _rnd) && !_conditions.ForceLiving) return star;

            _numberOfPlanets = starGenerator.CalculateNumberOfPlanets(starGenerator.CalculateMaxNumberOfPlanet(star), _rnd);
            
            Construct();
            return star;
        }
    }
}