using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static PlanetMapper RetrievePlanetMapper(IUnitOfWork uow, bool IsTest = false)
        {
            return new PlanetMapper(uow,IsTest);
        }
    }
}