using BLL.Generation.StarSystem;
using BLL.Utilities.Structs;
using Models.Universe;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation
{
    public sealed class GeneratePortion
    {
        private IntRange _RangeX;
        private IntRange _RangeY;
        private MainUow _Uow;
        private PlanetCustomConditions _Conditions = null;
        private int _GalaxyId = -1;

        public GeneratePortion(
            int minX,
            int maxX,
            int minY,
            int MaxY,
            MainUow uow,
            PlanetCustomConditions conditions,
            int galaxyId
            )
        {
            this._RangeX = new IntRange(minX, maxX);
            this._RangeY = new IntRange(minY, MaxY);
            this._Uow = uow;
            if (conditions == null) throw new ArgumentNullException("conditions");
        }

        /// <summary>
        /// Genera un intero sistema solare
        /// </summary>
        /// <returns></returns>
        public bool Generate(Random _Rnd, string cacheKey = "")
        {
            Galaxy referredGalaxy = null;
            bool result = false;
            referredGalaxy = _Uow.GalaxyRepository.GetByKey(this._GalaxyId, cacheKey);
            if (referredGalaxy != null)
            {
                try
                {
                    StarSystemGenerator generator = new StarSystemGenerator(
                                new StarGenerator(),
                                new StarPlacer(this._Uow, referredGalaxy),
                                this._RangeX,
                                this._RangeY,
                                this._Conditions);

                    generator.GenerateAndInsert(_Rnd, this._Uow, referredGalaxy, cacheKey);
                    result = true;
                }
                catch (Exception ex)
                {
                    //DA FARE: SISTEMA PER LOGGARE LE ECCEZIONI
                    result = false;
                }

            }
            return result;
        }
    }
}
