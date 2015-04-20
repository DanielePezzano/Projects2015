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
using UnitOfWork.Implementations.Repository;
using UnitOfWork.Interfaces;
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

        private IRepository<User> _UserRepo = null;
        private IRepository<InternalMail> _InternalMailRepo = null;
        private IRepository<Galaxy> _GalaxyRepo = null;
        private IRepository<Star> _StarRepo = null;
        private IRepository<Satellite> _SatelliteRepo = null;
        private IRepository<Planet> _PlanetRepo = null;
        private IRepository<TechBonus> _TechBonusRepo = null;
        private IRepository<Technology> _TechnologyRepo = null;
        private IRepository<TechRequisiteNode> _TechNodeRepo = null;
        private IRepository<RaceBonus> _RaceBonusRepo = null;
        private IRepository<ResearchQueue> _ResQueueRepo = null;
        private IRepository<FleetQueue> _FleetQueueRepo = null;
        private IRepository<BuildingQueue> _BuildingQueueRepo = null;
        private IRepository<UserLog> _UserLogRepo = null;
        private IRepository<GalaxyLog> _GalaxyLogRepo = null;

        private IRepository<Building> _BuildingRepo = null;
        private IRepository<BuildingSpec> _BuildingSpecRepo = null;
        private IRepository<Fleet> _FleetRepo = null;
        private IRepository<ShipClass> _ShipClassRepo = null;
        private IRepository<Armor> _ArmorRepo = null;
        private IRepository<Engine> _EngineRepo = null;
        private IRepository<Hull> _HullRepo = null;
        private IRepository<Shield> _ShieldRepo = null;
        private IRepository<ShipSystem> _ShipSystemRepo = null;
        private IRepository<AntiShipWeapon> _AntiShipWeaponRepo = null;
        private IRepository<AntiPlanetWeapon> _AntiPlanetWeaponRepo = null;

        public MainUow(IContextFactory contextFactory,DalCache cache)
        {
            _Context = contextFactory.Retrieve();
            if (_Context.IsTest == false) CheckInitialization();
            _Cache = cache;
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
                if (_AntiPlanetWeaponRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _AntiPlanetWeaponRepo = factory.GetAntiPlanetWeaponRepository();
                    }
                }
                return _AntiPlanetWeaponRepo;
            }
        }

        public IRepository<AntiShipWeapon> AntiShipWeaponRepository
        {
            get
            {
                if (_AntiShipWeaponRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _AntiShipWeaponRepo = factory.GetAntiShipWeaponRepository();
                    }
                }
                return _AntiShipWeaponRepo;
            }
        }

        public IRepository<ShipSystem> ShipSystemRepository
        {
            get
            {
                if (_ShipSystemRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _ShipSystemRepo = factory.GetShipSystemRepository();
                    }
                }
                return _ShipSystemRepo;
            }
        }

        public IRepository<Shield> ShieldRepository
        {
            get
            {
                if (_ShieldRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _ShieldRepo = factory.GetShieldRepository();
                    }
                }
                return _ShieldRepo;
            }
        }

        public IRepository<Hull> HullRepository
        {
            get
            {
                if (_HullRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _HullRepo = factory.GetHullRepository();
                    }
                }
                return _HullRepo;
            }
        }


        public IRepository<Engine> EngineRepository
        {
            get
            {
                if (_EngineRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _EngineRepo = factory.GetEngineRepository();
                    }
                }
                return _EngineRepo;
            }
        }

        public IRepository<Armor> ArmorRepository
        {
            get
            {
                if (_ArmorRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _ArmorRepo = factory.GetArmorRepository();
                    }
                }
                return _ArmorRepo;
            }
        }

        public IRepository<ShipClass> ShipClassRepository
        {
            get
            {
                if (_ShipClassRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _ShipClassRepo = factory.GetShipClasstRepository();
                    }
                }
                return _ShipClassRepo;
            }
        }

        public IRepository<Fleet> FleetRepository
        {
            get
            {
                if (_FleetRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _FleetRepo = factory.GetFleetRepository();
                    }
                }
                return _FleetRepo;
            }
        }

        public IRepository<BuildingSpec> BuildingSpecRepository
        {
            get
            {
                if (_BuildingSpecRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _BuildingSpecRepo = factory.GetBuildingSpecRepository();
                    }
                }
                return _BuildingSpecRepo;
            }
        }

        public IRepository<Building> BuildingRepository
        {
            get
            {
                if (_BuildingRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _BuildingRepo = factory.GetBuildingRepository();
                    }
                }
                return _BuildingRepo;
            }
        }

        public IRepository<GalaxyLog> GalaxyLogRepository
        {
            get
            {
                if (_GalaxyLogRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _GalaxyLogRepo = factory.GalaxyLogRepository();
                    }
                }
                return _GalaxyLogRepo;
            }
        }

        public IRepository<UserLog> UserLogRepository
        {
            get
            {
                if (_UserLogRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _UserLogRepo = factory.GetUserLogRepository();
                    }
                }
                return _UserLogRepo;
            }
        }

        public IRepository<BuildingQueue> BuildingQueueRepository
        {
            get
            {
                if (_BuildingQueueRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _BuildingQueueRepo = factory.GetBuildingQueueRepository();
                    }
                }
                return _BuildingQueueRepo;
            }
        }

        public IRepository<FleetQueue> FleetQueueRepository
        {
            get
            {
                if (_FleetQueueRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _FleetQueueRepo = factory.GetFleetQueueRepository();
                    }
                }
                return _FleetQueueRepo;
            }
        }

        public IRepository<ResearchQueue> ResearchQueueRepository
        {
            get
            {
                if (_ResQueueRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _ResQueueRepo = factory.GetResearchQueueRepository();
                    }
                }
                return _ResQueueRepo;
            }
        }

        public IRepository<RaceBonus> RaceBonusRepository
        {
            get
            {
                if (_RaceBonusRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _RaceBonusRepo = factory.GetRaceBonusRepository();
                    }
                }
                return _RaceBonusRepo;
            }
        }
        
        public IRepository<TechRequisiteNode> TechNodesRepository
        {
            get
            {
                if (_TechNodeRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _TechNodeRepo = factory.GetTechNodeRepository();
                    }
                }
                return _TechNodeRepo;
            }
        }

        public IRepository<Technology> TechnologyRepository
        {
            get
            {
                if (_TechnologyRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _TechnologyRepo = factory.GetTechnologyRepository();
                    }
                }
                return _TechnologyRepo;
            }
        }

        public IRepository<TechBonus> TechBonusRepository
        {
            get
            {
                if (_TechBonusRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _TechBonusRepo = factory.GetTechBonusRepository();
                    }
                }
                return _TechBonusRepo;
            }
        }

        public IRepository<InternalMail> InternalMailRepository
        {
            get
            {
                if (_InternalMailRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _InternalMailRepo = factory.GetInternalMailRepository();
                    }
                }
                return _InternalMailRepo;
            }
        }

        public IRepository<Planet> PlanetRepository
        {
            get
            {
                if (_PlanetRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _PlanetRepo = factory.GetPlanetRepository();
                    }
                }
                return _PlanetRepo;
            }
        }

        public IRepository<Satellite> SatelliteRepository
        {
            get
            {
                if (_SatelliteRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _SatelliteRepo = factory.GetSatelliteRepository();
                    }
                }
                return _SatelliteRepo;
            }
        }

        public IRepository<Star> StarRepository
        {
            get
            {
                if (_StarRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _StarRepo = factory.GetStarRepository();
                    }
                }
                return _StarRepo;
            }
        }

        public IRepository<Galaxy> GalaxyRepository
        {
            get
            {
                if (_GalaxyRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _GalaxyRepo = factory.GetGalaxyRepository();
                    }
                }
                return _GalaxyRepo;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (_UserRepo == null)
                {
                    using (RepositoryFactory factory = new RepositoryFactory(this._Context, _Cache))
                    {
                        _UserRepo = factory.GetUserRepository();
                    }
                }
                return _UserRepo;
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
                    this._UserRepo = null;
                    this._AntiPlanetWeaponRepo = null;
                    this._AntiShipWeaponRepo = null;
                    this._ArmorRepo = null;
                    this._BuildingQueueRepo = null;
                    this._BuildingRepo = null;
                    this._BuildingSpecRepo = null;
                    this._Cache = null;
                    this._Context = null;
                    this._EngineRepo = null;
                    this._FleetQueueRepo = null;
                    this._FleetRepo = null;
                    this._GalaxyLogRepo = null;
                    this._GalaxyRepo = null;
                    this._HullRepo = null;
                    this._InternalMailRepo = null;
                    this._PlanetRepo = null;
                    this._RaceBonusRepo = null;
                    this._ResQueueRepo = null;
                    this._SatelliteRepo = null;
                    this._ShieldRepo = null;
                    this._ShipClassRepo = null;
                    this._ShipSystemRepo = null;
                    this._StarRepo = null;
                    this._TechBonusRepo = null;
                    this._TechNodeRepo = null;
                    this._TechnologyRepo = null;
                    this._UserLogRepo = null;
                    this._UserRepo = null;
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
            bool result = true;
            using (TransactionScope t = new TransactionScope())
            {                
                try
                {
                    if (_Context as ProductionContext != null)
                        (_Context as ProductionContext).SaveChanges();
                    else result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    var message = ex.Message;
                    throw;
                }
            }
            return result;
        }
    }
}
