using DAL.Operations.BaseClasses;
using DAL.Operations.IstanceFactory;
using UnitOfWork.Implementations.Uows;

namespace DAL.Operations.Extensions
{
    public static class OpExtensions
    {
        public static ProductionUow SetProductionUow(this BaseOperations operations)
        {
            var cf = OperationsFactory.RetrieveContextFactory(operations.ConnectionString, false);
            var context = cf.Retrieve();
            var repoFactories = OperationsFactory.RetrieveRepositoryFactories(context,
                OperationsFactory.RetrieveDalCache(), OperationsFactory.RetrieveRepositories());
            
            return OperationsFactory.RetrieveProductionUow(context,repoFactories);
        }

        public static TestUow SetTestUow(this BaseOperations operations)
        {
            var cf = OperationsFactory.RetrieveContextFactory(operations.ConnectionString, false);
            var context = cf.Retrieve();
            var repoFactories = OperationsFactory.RetrieveRepositoryFactories(context,
                OperationsFactory.RetrieveDalCache(), OperationsFactory.RetrieveRepositories());

            return OperationsFactory.RetrieveTestUow(context, repoFactories);
        }
    }
}