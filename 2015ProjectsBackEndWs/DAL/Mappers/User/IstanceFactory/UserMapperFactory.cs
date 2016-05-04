using DAL.Operations.BaseClasses;

namespace DAL.Mappers.User.IstanceFactory
{
    public static class UserMapperFactory
    {
        public static RaceBonusMapper RetrieveBonusMapper(string connectionString, BaseOperations operations,
            bool isTest = false)
        {
            return new RaceBonusMapper(isTest, connectionString, operations);
        }
    }
}