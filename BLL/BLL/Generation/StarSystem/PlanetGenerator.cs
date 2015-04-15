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

        public PlanetGenerator(Star star, bool force = false)
        {
            this._Star = star;
            this._ForceLiving = force;
        }

        public Planet Generate()
        {
            throw new NotImplementedException();
        }
    }
}
