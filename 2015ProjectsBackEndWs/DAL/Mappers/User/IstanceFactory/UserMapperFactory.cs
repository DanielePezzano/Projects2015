using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.User.Enums;
using DAL.Operations.IstanceFactory;

namespace DAL.Mappers.User.IstanceFactory
{
    public static class UserMapperFactory
    {
        public static BaseMapper RetrieveMapper(OpFactory operations, UserMapperTypes mapperSelector)
        {
            switch (mapperSelector)
            {
                case UserMapperTypes.RaceBonus:
                    return new RaceBonusMapper(operations);
                case UserMapperTypes.UserType:
                    return new UserMapper( operations);
                case UserMapperTypes.ResearchQueue:
                    return new TechRequisiteMapper( operations);
                case UserMapperTypes.Technologies:
                    return new TechMapper( operations);
                case UserMapperTypes.TechBonus:
                    return new TechBonusMapper( operations);
                case UserMapperTypes.TechRequisites:
                    return new TechRequisiteMapper( operations);
                default:
                    throw new ArgumentOutOfRangeException(nameof(mapperSelector), mapperSelector, null);
            }
        }
    }
}