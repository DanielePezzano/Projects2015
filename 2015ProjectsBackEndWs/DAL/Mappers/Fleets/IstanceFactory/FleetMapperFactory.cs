using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.Fleets.IstanceFactory
{
    public static class FleetMapperFactory
    {
        
        public static BaseMapper RetrieveMapper(string connectionString, OpFactory operations,FleetMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case FleetMapperTypes.AntiplanetWeapons:
                    return new AntiPlanetWeaponMapper(connectionString, operations);
                case FleetMapperTypes.AntishipWeapons:
                    return new AntiShipWeaponMapper(connectionString, operations);
                case FleetMapperTypes.Armor:
                    return new ArmorMapper(connectionString, operations);
                case FleetMapperTypes.Engine:
                    return new EngineMapper(connectionString, operations);
                case FleetMapperTypes.Fleet:
                    return new FleetMapper(connectionString,operations);
                case FleetMapperTypes.Hull:
                    return new HullMapper(connectionString, operations);
                case FleetMapperTypes.Shield:
                    return new ShieldMapper(connectionString, operations);
                case FleetMapperTypes.Ship:
                    return new ShipClassMapper(connectionString, operations);
                case FleetMapperTypes.Systems:
                    return new SystemsMapper(connectionString, operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}