using DAL.Operations.BaseClasses;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace DAL.Operations.IstanceFactory
{
    public static class OperationsFactory
    {
        public static UserOperations RetrieveBaseOperations(string connectionString, bool isTest = false)
        {
            return new UserOperations(connectionString,isTest);
        }

        public static ContextFactory RetrieveContextFactory(string connectionString, bool isTest)
        {
            return new ContextFactory(connectionString,isTest);
        }

        public static DalCache RetrieveDalCache()
        {
            return new DalCache();
        }

        public static UowRepositories RetrieveRepositories()
        {
            return new UowRepositories();
        }

        public static UowRepositoryFactories RetrieveRepositoryFactories(IContext context,DalCache cache, UowRepositories repositories)
        {
            return new UowRepositoryFactories(context,cache,repositories);
        }

        public static ProductionUow RetrieveProductionUow(IContext context, UowRepositoryFactories factory)
        {
            return new ProductionUow(context,factory);
        }

        public static TestUow RetrieveTestUow(IContext context, UowRepositoryFactories factory)
        {
            return new TestUow(context, factory);
        }
    }
}