using DAL.Operations.BaseClasses;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static PlanetMapper RetrievePlanetMapper(string connectionString, BaseOperations operations, bool isTest = false)
        {
            return new PlanetMapper(connectionString,operations,isTest);
        }

        public static BuildingMapper RetrieveBuildingMapper(string connectionString, BaseOperations operations, bool isTest = false)
        {
            return new BuildingMapper(connectionString, operations, isTest);
        }

        public static BuildingSpecsMapper RetrieveBuildingSpecsMapper(string connectionString,BaseOperations operations, bool isTest = false)
        {
            return new BuildingSpecsMapper(connectionString, operations, isTest);
        }
    }
}