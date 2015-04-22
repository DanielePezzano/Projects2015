using Models.Base;
using Models.Buildings;
using Models.Fleets;
using Models.Fleets.ShipClasses;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Hulls;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using Models.Logs;
using Models.Queues;
using Models.Races;
using Models.Tech;
using Models.Universe;
using Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Repository.BaseRepository;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository
{
    public static class RepositoryFactory<T> where T:BaseEntity
    {
        #region Get Repositories
        public static IRepository<T> GetRepository(IContext context, DalCache cache)
        {
            if (context as ProductionContext != null)
                return new RepositoryProduction<T>(context as ProductionContext, cache, string.Empty);
            return new RepositoryTest<T>(context as TestContext, cache, string.Empty);
        }
        #endregion

        
    }
}
