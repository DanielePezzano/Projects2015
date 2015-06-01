using AuthModel.Base;
using AutUow.Cache;
using AutUow.Implementations.Context;
using AutUow.Implementations.Repository.BaseRepository;
using AutUow.Interfaces;

namespace AutUow.Implementations.Repository
{
    public static class RepositoryFactory<T> where T : BaseEntity
    {
        #region Get Repositories

        public static IRepository<T> GetRepository(IContext context, DalCache cache)
        {
            var productionContext = context as ProductionContext;
            if (productionContext != null)
                return new RepositoryProduction<T>(productionContext, cache);
            return new RepositoryTest<T>(context as TestContext, cache, string.Empty);
        }

        #endregion
    }
}