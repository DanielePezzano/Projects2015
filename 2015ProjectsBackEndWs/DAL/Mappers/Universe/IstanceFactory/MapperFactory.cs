using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Universe.Enums;
using DAL.Operations.BaseClasses;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static BaseMapper RetrieveMapper(string connectionString, OpFactory operations,
            UniverseMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UniverseMapperTypes.Planets:
                    return new PlanetMapper(connectionString, operations);
                    break;
                case UniverseMapperTypes.Buildings:
                    return new BuildingMapper(connectionString, operations);
                    break;
                case UniverseMapperTypes.BuildingSpecs:
                    return new BuildingSpecsMapper(connectionString, operations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}