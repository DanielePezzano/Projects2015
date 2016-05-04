using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Operations.BaseClasses;

namespace DAL.Mappers.Fleets.IstanceFactory
{
    public static class FleetMapperFactory
    {
        
        public static BaseMapper RetrieveMapper(bool isTest, string connectionString, BaseOperations operations,FleetMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case FleetMapperTypes.AntiplanetWeapons:
                    return new AntiPlanetWeaponMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.AntishipWeapons:
                    return new AntiShipWeaponMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Armor:
                    return new ArmorMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Engine:
                    return new EngineMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Fleet:
                    break;
                case FleetMapperTypes.Hull:
                    return new HullMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Shield:
                    return new ShieldMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Ship:
                    return new ShipClassMapper(isTest, connectionString, operations);
                    break;
                case FleetMapperTypes.Systems:
                    return new SystemsMapper(isTest, connectionString, operations);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}