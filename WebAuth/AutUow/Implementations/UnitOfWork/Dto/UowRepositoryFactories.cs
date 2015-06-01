using System;
using AuthModel;
using AutUow.Cache;
using AutUow.Implementations.Repository;
using AutUow.Interfaces;

namespace AutUow.Implementations.UnitOfWork.Dto
{
    public class UowRepositoryFactories : IDisposable
    {
        private readonly DalCache _cache;
        private readonly IContext _context;
        private bool _disposed;

        public UowRepositoryFactories(IContext context, DalCache cache, UowRepositories repositories)
        {
            if (context != null)
                _context = context;
            else
                throw new ArgumentNullException("context");
            if (cache != null)
                _cache = cache;
            else throw new ArgumentNullException("cache");
            if (repositories != null) Repositories = repositories;
            else throw new ArgumentNullException("repositories");
            InitializeRepositories();
        }

        public UowRepositories Repositories { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void InitializeRepositories()
        {
            if (Repositories.UserProfileRepository == null)
                Repositories.UserProfileRepository = RepositoryFactory<UserProfile>.GetRepository(_context, _cache);
            if (Repositories.UserRolesRepository == null)
                Repositories.UserRolesRepository = RepositoryFactory<Roles>.GetRepository(_context, _cache);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            Repositories.Dispose();
        }
    }
}