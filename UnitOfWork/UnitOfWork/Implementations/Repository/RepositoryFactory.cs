using Models.Base;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Repository.BaseRepository;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository
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