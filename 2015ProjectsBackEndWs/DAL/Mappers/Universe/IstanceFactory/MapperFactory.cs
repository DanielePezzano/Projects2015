using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static PlanetMapper RetrievePlanetMapper(IUnitOfWork uow, bool IsTest = false)
        {
            return new PlanetMapper(uow,IsTest);
        }

        public static BuildingMapper RetrieveBuildingMapper(IUnitOfWork uow, bool isTest = false)
        {
            return new BuildingMapper(uow, isTest);
        }

        public static BuildingSpecsMapper RetrieveBuildingSpecsMapper(IUnitOfWork uow, bool isTest = false)
        {
            return new BuildingSpecsMapper(uow, isTest);
        }
    }
}