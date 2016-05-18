using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets.Enums;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.Fleets.IstanceFactory
{
    public static class FleetMapperFactory
    {

        public static BaseMapper RetrieveMapper(OpFactory operations, FleetMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case FleetMapperTypes.AntiplanetWeapons:
                    return new AntiPlanetWeaponMapper(operations);
                case FleetMapperTypes.AntishipWeapons:
                    return new AntiShipWeaponMapper(operations);
                case FleetMapperTypes.Armor:
                    return new ArmorMapper(operations);
                case FleetMapperTypes.Engine:
                    return new EngineMapper(operations);
                case FleetMapperTypes.Fleet:
                    return new FleetMapper(operations);
                case FleetMapperTypes.Hull:
                    return new HullMapper(operations);
                case FleetMapperTypes.Shield:
                    return new ShieldMapper(operations);
                case FleetMapperTypes.Ship:
                    return new ShipClassMapper(operations);
                case FleetMapperTypes.Systems:
                    return new SystemsMapper(operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}