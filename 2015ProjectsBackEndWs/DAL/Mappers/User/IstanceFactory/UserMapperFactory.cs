using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.User.Enums;
using DAL.Operations.BaseClasses;

namespace DAL.Mappers.User.IstanceFactory
{
    public static class UserMapperFactory
    {
        public static BaseMapper RetrieveMapper(bool isTest, string connectionString, BaseOperations operations,
            UserMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UserMapperTypes.RaceBonus:
                    return new RaceBonusMapper(isTest, connectionString, operations);
                case UserMapperTypes.UserType:
                    return new UserMapper(connectionString, operations, isTest);
                case UserMapperTypes.ResearchQueue:
                    return new TechRequisiteMapper(isTest, connectionString, operations);
                case UserMapperTypes.Technologies:
                    return new TechMapper(isTest, connectionString, operations);
                case UserMapperTypes.TechBonus:
                    return new TechBonusMapper(isTest, connectionString, operations);
                case UserMapperTypes.TechRequisites:
                    return new TechRequisiteMapper(isTest, connectionString, operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}