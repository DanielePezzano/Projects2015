using DAL.Operations.BaseClasses;
using DAL.Operations.IstanceFactory;
using UnitOfWork.Implementations.Uows;

namespace DAL.Operations.Extensions
{
    public static class OpExtensions
    {

        public static ProductionUow SetProductionUow(this BaseOpAbstract operations,string connectionString) 
        {
            var cf = IstancesCreator.RetrieveContextFactory(connectionString, false);
            var context = cf.Retrieve();
            var repoFactories = IstancesCreator.RetrieveRepositoryFactories(context,
                IstancesCreator.RetrieveDalCache(), IstancesCreator.RetrieveRepositories());

            return IstancesCreator.RetrieveProductionUow(context, repoFactories);
        }


        public static TestUow SetTestUow(this BaseOpAbstract operations, string connectionString) 
        {
            var cf = IstancesCreator.RetrieveContextFactory(connectionString, true);
            var context = cf.Retrieve();
            var repoFactories = IstancesCreator.RetrieveRepositoryFactories(context,
                IstancesCreator.RetrieveDalCache(), IstancesCreator.RetrieveRepositories());

            return IstancesCreator.RetrieveTestUow(context, repoFactories);
        }

    }
}