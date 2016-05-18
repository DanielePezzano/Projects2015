using BLL.Generation.StarSystem.Builders;
using BLL.Utilities.Structs;
using SharedDto.Universe.Stars;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Generation.StarSystem
{
    public sealed class StarSystemGenerator
    {
        private readonly IntRange _rangeX;
        private readonly IntRange _rangeY;
        private readonly StarBuilder _starGenerator;
        private readonly StarPlacer _starPlacer;
        private readonly SolarSystemFactory _solarSystemFactory;

        public StarSystemGenerator(SolarSystemFactory factory,StarBuilder starGenerator,
            StarPlacer starPlacer,
            IntRange rangeX,
            IntRange rangeY)
        {
            _solarSystemFactory = factory;
            _starGenerator = starGenerator;
            _starPlacer = starPlacer;
            _rangeX = rangeX;
            _rangeY = rangeY;
        }
        
        public StarDto Generate(int galaxyId, string cacheKey,IUnitOfWork uow=null)
        {
           return _solarSystemFactory.Constuct(_starGenerator, _starPlacer, _rangeX, _rangeY, galaxyId, uow);
        }
    }
}