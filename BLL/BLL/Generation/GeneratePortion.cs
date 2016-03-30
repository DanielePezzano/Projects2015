using System;
using BLL.Generation.StarSystem;
using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using UnitOfWork.Implementations.Uows;

namespace BLL.Generation
{
    public sealed class GeneratePortion
    {
        private readonly PlanetCustomConditions _conditions = null;
        private readonly int _galaxyId;
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private readonly MainUow _uow;

        public GeneratePortion(
            int minX,
            int maxX,
            int minY,
            int maxY,
            MainUow uow,
            PlanetCustomConditions conditions,
            int galaxyId
            )
        {
            _rangeX = new IntRange(minX, maxX);
            _rangeY = new IntRange(minY, maxY);
            _uow = uow;
            _galaxyId = galaxyId;
            if (conditions == null) throw new ArgumentNullException("conditions");
        }

        /// <summary>
        ///     Genera un intero sistema solare
        /// </summary>
        /// <returns></returns>
        public bool Generate(Random rnd, string cacheKey = "")
        {
            bool result;
            var referredGalaxy = _uow.GalaxyRepository.GetByKey(_galaxyId, cacheKey);
            if (referredGalaxy == null) return false;
            try
            {
                var generator = new StarSystemGenerator(
                    new StarBuilder(),
                    new StarPlacer(_uow),
                    _rangeX,
                    _rangeY,
                    _conditions);

                generator.GenerateAndInsert(rnd, _uow, referredGalaxy, cacheKey);
                result = true;
            }
            catch (Exception)
            {
                //DA FARE: SISTEMA PER LOGGARE LE ECCEZIONI
                result = false;
            }
            return result;
        }
    }
}