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
        private static Random _Rnd;

        public PlanetGenerator(Star star, bool force = false)
        {
            this._Star = star;
            this._ForceLiving = force;
            _Rnd = new Random();
        }

        public Planet CreateBrandNewPlanet()
        {
            Planet result = new Planet();
            result.CreatedAt = DateTime.Now;
            result.UpdatedAt = DateTime.Now;
            result.Star = _Star;
            result.Name = "PL" + DateTime.Now.ToFileTimeUtc();
            
            return result;
        }
    }
}
