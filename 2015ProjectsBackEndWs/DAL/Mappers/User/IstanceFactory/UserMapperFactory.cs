using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.User.Enums;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.User.IstanceFactory
{
    public static class UserMapperFactory
    {
        public static BaseMapper RetrieveMapper(string connectionString, OpFactory operations, UserMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UserMapperTypes.RaceBonus:
                    return new RaceBonusMapper(connectionString, operations);
                case UserMapperTypes.UserType:
                    return new UserMapper(connectionString, operations);
                case UserMapperTypes.ResearchQueue:
                    return new TechRequisiteMapper(connectionString, operations);
                case UserMapperTypes.Technologies:
                    return new TechMapper(connectionString, operations);
                case UserMapperTypes.TechBonus:
                    return new TechBonusMapper(connectionString, operations);
                case UserMapperTypes.TechRequisites:
                    return new TechRequisiteMapper(connectionString, operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}