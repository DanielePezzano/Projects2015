using BLL.Utilities;
using Models.Universe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Generation.StarSystem
{
    public sealed class PlanetGenerator
    {
        private Star _Star;
        private bool _ForceLiving = false;
        private double _MediumDensity = 5.5; //densità media terrestre --> se densità calcolata <=3 probabilmente è gassoso
        private const double _MinAtmosphereDistance = 0.7;
        private static Random _Rnd;

        public PlanetGenerator(Star star, bool force = false)
        {
            this._Star = star;
            this._ForceLiving = force;
            _Rnd = new Random();
        }

        private int CalculateRadiationLevel(bool atmospherePresent, int starRadiation, double distance)
        {
            if (_ForceLiving) return 1;
            int result = 1;
            if (distance <= 1)
            {
                result += starRadiation - (int)(starRadiation * (1 - distance));
            }
            else
            {
                int temp =starRadiation - (int)(starRadiation * (distance - 1));
                result += (temp > 0) ? temp : 3;
            }
            return (atmospherePresent) ? result / 2 : result;
        }

        private bool IsAtmospherePresent(double distance)
        {
            if (_ForceLiving) return true;
            if (distance >= _MinAtmosphereDistance)
            {
                return (RandomNumbers.RandomInt(0, 100, _Rnd) <= 30) ? true : false;
            }
            else return false;
        }

        #region public wrapper  for test purpouse
        public int CalculateRadiationLevelTest(bool atmospherePresent, int starRadiation, double distance)
        {
            return this.CalculateRadiationLevel(atmospherePresent, starRadiation, distance);
        }
        #endregion

        public Planet CreateBrandNewPlanet()
        {
            Planet result = new Planet();
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Star = _Star;
            result.Name = "PL" + DateTime.Now.ToFileTimeUtc();
            result.Mass = Math.Truncate(RandomNumbers.RandomDouble(0.04, 180, _Rnd) * 100) / 100;
            
            return result;
        }

        public void AddOrbitGenerationAndCollateral(Planet planet, OrbitGenerator generator)
        {
            planet.Orbit = generator.Generate();
            planet.AtmospherePresent = this.IsAtmospherePresent(planet.Orbit.DistanceR);
            planet.RadiationLevel = this.CalculateRadiationLevel(planet.AtmospherePresent, _Star.RadiationLevel, planet.Orbit.DistanceR);
        }
    }
}
