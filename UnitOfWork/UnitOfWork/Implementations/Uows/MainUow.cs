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
using System.Data.Entity.Infrastructure;
using System.Transactions;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;
using UnitOfWork.Interfaces.Repository;
using UnitOfWork.Interfaces.UnitOfWork;

namespace UnitOfWork.Implementations.Uows
{
    public class MainUow : IUnitOfWork, IDisposable
    {
        private IContext _Context = null;
        private DalCache _Cache = null;
        private bool disposed = false;

        private UowRepositoryFactories _RepoFactories = null;

        public MainUow(IContext context, DalCache cache, UowRepositoryFactories repoFactories)
        {
            if (context != null) _Context = context; else throw new ArgumentNullException("context");
            if (cache != null) _Cache = cache; else throw new ArgumentNullException("cache");
            if (repoFactories != null) _RepoFactories = repoFactories; else throw new ArgumentNullException("repoFactories");

            if (_Context.IsTest == false) CheckInitialization();
        }

        private void CheckInitialization()
        {

            if (!(_Context as ProductionContext).Database.Exists())
            {
                try
                {
                    ((IObjectContextAdapter)(_Context as ProductionContext)).ObjectContext.CreateDatabase();
                }
                catch (Exception ex)
                {
                    //DAFARE : CREARE UN PROGETTO LOGGER CHE SI OCCUPI DI PRENDERE LE ECCEZIONI E SCRIVERLE IN UN FILE APPOSITO
                    var message = ex.Message;
                    throw;
                }
            }
        }

        #region Repositories properties

        public IRepository<AntiPlanetWeapon> AntiPlanetWeaponRepository
        {
            get
            {
                return _RepoFactories.Repositories.AntiPlanetWeaponRepo;
            }
        }

        public IRepository<AntiShipWeapon> AntiShipWeaponRepository
        {
            get
            {
                return _RepoFactories.Repositories.AntiShipWeaponRepo;
            }
        }

        public IRepository<ShipSystem> ShipSystemRepository
        {
            get
            {
                return _RepoFactories.Repositories.ShipSystemRepo;
            }
        }

        public IRepository<Shield> ShieldRepository
        {
            get
            {
                return _RepoFactories.Repositories.ShieldRepo;
            }
        }

        public IRepository<Hull> HullRepository
        {
            get
            {
                return _RepoFactories.Repositories.HullRepo;
            }
        }

        public IRepository<Engine> EngineRepository
        {
            get
            {
                return _RepoFactories.Repositories.EngineRepo;
            }
        }

        public IRepository<Armor> ArmorRepository
        {
            get
            {
                return _RepoFactories.Repositories.ArmorRepo;
            }
        }

        public IRepository<ShipClass> ShipClassRepository
        {
            get
            {
                return _RepoFactories.Repositories.ShipClassRepo;
            }
        }

        public IRepository<Fleet> FleetRepository
        {
            get
            {
                return _RepoFactories.Repositories.FleetRepo;
            }
        }

        public IRepository<BuildingSpec> BuildingSpecRepository
        {
            get
            {
                return _RepoFactories.Repositories.BuildingSpecRepo;
            }
        }

        public IRepository<Building> BuildingRepository
        {
            get
            {
                return _RepoFactories.Repositories.BuildingRepo;
            }
        }

        public IRepository<GalaxyLog> GalaxyLogRepository
        {
            get
            {
                return _RepoFactories.Repositories.GalaxyLogRepo;
            }
        }

        public IRepository<UserLog> UserLogRepository
        {
            get
            {
                return _RepoFactories.Repositories.UserLogRepo;
            }
        }

        public IRepository<BuildingQueue> BuildingQueueRepository
        {
            get
            {
                return _RepoFactories.Repositories.BuildingQueueRepo;
            }
        }

        public IRepository<FleetQueue> FleetQueueRepository
        {
            get
            {
                return _RepoFactories.Repositories.FleetQueueRepo;
            }
        }

        public IRepository<ResearchQueue> ResearchQueueRepository
        {
            get
            {
                return _RepoFactories.Repositories.ResQueueRepo;
            }
        }

        public IRepository<RaceBonus> RaceBonusRepository
        {
            get
            {
                return _RepoFactories.Repositories.RaceBonusRepo;
            }
        }

        public IRepository<TechRequisiteNode> TechNodesRepository
        {
            get
            {
                return _RepoFactories.Repositories.TechNodeRepo;
            }
        }

        public IRepository<Technology> TechnologyRepository
        {
            get
            {
                return _RepoFactories.Repositories.TechnologyRepo;
            }
        }

        public IRepository<TechBonus> TechBonusRepository
        {
            get
            {
                return _RepoFactories.Repositories.TechBonusRepo;
            }
        }

        public IRepository<InternalMail> InternalMailRepository
        {
            get
            {
                return _RepoFactories.Repositories.InternalMailRepo;
            }
        }

        public IRepository<Planet> PlanetRepository
        {
            get
            {
                return _RepoFactories.Repositories.PlanetRepo;
            }
        }

        public IRepository<Satellite> SatelliteRepository
        {
            get
            {
                return _RepoFactories.Repositories.SatelliteRepo;
            }
        }

        public IRepository<Star> StarRepository
        {
            get
            {
                return _RepoFactories.Repositories.StarRepo;
            }
        }

        public IRepository<Galaxy> GalaxyRepository
        {
            get
            {
                return _RepoFactories.Repositories.GalaxyRepo;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                return _RepoFactories.Repositories.UserRepo;
            }
        }
        #endregion

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._Context = null;
                    this._RepoFactories.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Save()
        {
           ProductionContext context = _Context as ProductionContext;

           return DoSaving(context) >= 0 ? true : false;
        }

        private int DoSaving(ProductionContext context)
        {
            int rowsAffected = -1;
            try
            {
                if (context != null)
                   rowsAffected = context.SaveChanges();
                else rowsAffected = 1;
            }
            catch (Exception ex)
            {
                //DAFARE : CREARE UN PROGETTO LOGGER CHE SI OCCUPI DI PRENDERE LE ECCEZIONI E SCRIVERLE IN UN FILE APPOSITO
                var message = ex.Message;
                throw;
            }
            return rowsAffected;
        }
    }
}
