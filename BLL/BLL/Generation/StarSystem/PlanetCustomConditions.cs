using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Generation.StarSystem
{
    public sealed class PlanetCustomConditions
    {
        public bool ForceLiving {get;set;}
        public bool ForceWater {get;set;}
        public bool MineralRich {get;set;}
        public bool MineralPoor {get;set;}
        public bool FoodRich {get;set;}
        public bool FoodPoor {get;set;}
        public bool MostlyWater {get;set;}

        public PlanetCustomConditions()
        {
            this.ForceWater = false;
            this.FoodPoor = false;
            this.FoodRich = false;
            this.ForceLiving = false;
            this.MineralPoor = false;
            this.MineralRich = false;
            this.MostlyWater = false;
        }
    }
}
