using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Universe.Enums;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.Universe.IstanceFactory
{
    public static class MapperFactory
    {
        public static BaseMapper RetrieveMapper(OpFactory operations,
            UniverseMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UniverseMapperTypes.stars:
                    return new StarMapper(operations);
                case UniverseMapperTypes.Planets:
                    return new PlanetMapper(operations);
                case UniverseMapperTypes.Buildings:
                    return new BuildingMapper(operations);
                case UniverseMapperTypes.BuildingSpecs:
                    return new BuildingSpecsMapper(operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}