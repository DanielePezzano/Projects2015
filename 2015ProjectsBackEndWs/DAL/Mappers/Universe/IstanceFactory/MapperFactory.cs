using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Universe.Enums;
using DAL.Operations.BaseClasses;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static BaseMapper RetrieveMapper(bool isTest, string connectionString, BaseOperations operations,
            UniverseMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UniverseMapperTypes.Planets:
                    return new PlanetMapper(connectionString, operations, isTest);
                    break;
                case UniverseMapperTypes.Buildings:
                    return new BuildingMapper(connectionString, operations, isTest);
                    break;
                case UniverseMapperTypes.BuildingSpecs:
                    return new BuildingSpecsMapper(connectionString, operations, isTest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}