using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using Models.Universe;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation
{
    public class GeneratePortion
    {
        private IntRange _RangeX;
        private IntRange _RangeY;
        private MainUow _Uow;
        private bool _ForceLiving = false;
        private bool _ForceWater = false;
        private bool _MineralRich = false;
        private bool _MineralPoor = false;
        private bool _FoodRich = false;
        private bool _FoodPoor = false;
        private bool _MostlyWater = false;

        public GeneratePortion(int minX, int maxX, int minY, int MaxY, MainUow uow, bool forceLiving,bool forceWater, bool mostlyWater, bool mineralPoor, bool mineralrich, bool foodPoor, bool foodRich)
        {
            this._RangeX = new IntRange(minX, maxX);
            this._RangeY = new IntRange(minY, MaxY);
            this._Uow = uow;
            this._ForceLiving = forceLiving;
            this._ForceWater = forceWater;
            this._MostlyWater = mostlyWater;
            this._MineralPoor = mineralPoor;
            this._MineralRich = mineralrich;
            this._FoodPoor = foodPoor;
            this._FoodRich = foodRich;            
        }

        private bool GalaxyToBeGenerated()
        {
            return this._Uow.GalaxyRepository.Count() == 0 ? true : false;
        }

        private Galaxy GenerateGalaxyFirst()
        {
            Galaxy toAdd = new Galaxy();
            toAdd.CreatedAt = DateTime.Now;
            toAdd.UpdatedAt = DateTime.Now;
            toAdd.Users = new List<User>();
            toAdd.Stars = new List<Star>();
            toAdd.Name = "Galaxy - " + DateTime.UtcNow.ToString();

            this._Uow.GalaxyRepository.Add(toAdd);
            this._Uow.Save();
            return toAdd;
        }
        /// <summary>
        /// METTERE A POSTO SE IL METODO FUNZIONA!
        /// </summary>
        /// <returns></returns>
        public bool Generate(Random _Rnd)
        {
            Galaxy referredGalaxy = null;
            bool result = false;
            if (this.GalaxyToBeGenerated())
                referredGalaxy = this.GenerateGalaxyFirst();
            else
                referredGalaxy = _Uow.GalaxyRepository.GetAll().FirstOrDefault();

            if (referredGalaxy != null)
            {
                try
                {
                    StarSystemGenerator generator = new StarSystemGenerator(
                                new StarGenerator(),
                                new StarPlacer(this._Uow, referredGalaxy),
                                this._ForceLiving,
                                this._ForceWater,
                                this._MostlyWater,
                                this._RangeX.Min,
                                this._RangeX.Max,
                                this._RangeY.Min,
                                this._RangeY.Max,
                                this._MineralRich,
                                this._MineralPoor,
                                this._FoodRich,
                                this._FoodPoor);

                    generator.GenerateAndInsert(_Rnd, this._Uow, referredGalaxy);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }

            }
            return result;
        }
    }
}
