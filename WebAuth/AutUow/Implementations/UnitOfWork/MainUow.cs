using System;
using System.Data.Entity.Infrastructure;
using AuthModel;
using AutUow.Implementations.Context;
using AutUow.Implementations.UnitOfWork.Dto;
using AutUow.Interfaces;

namespace AutUow.Implementations.UnitOfWork
{
    public class MainUow : IUnitOfWork, IDisposable
    {
        private readonly UowRepositoryFactories _repoFactories;
        private IContext _context;
        private bool _disposed;

        public MainUow(IContext context, UowRepositoryFactories repoFactories)
        {
            if (context != null) _context = context;
            else throw new ArgumentNullException("context");
            if (repoFactories != null) _repoFactories = repoFactories;
            else throw new ArgumentNullException("repoFactories");

            if (_context.IsTest == false) CheckInitialization();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public bool Save()
        {
            return DoSaving(_context as ProductionContext) >= 0;
        }

        private void CheckInitialization()
        {
            var context = _context as ProductionContext;
            if (context == null || context.Database.Exists()) return;
            ((IObjectContextAdapter) ((ProductionContext) _context)).ObjectContext.CreateDatabase();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context = null;
                    _repoFactories.Dispose();
                }
            }
            _disposed = true;
        }

        private static int DoSaving(ProductionContext context)
        {
            return context != null ? context.SaveChanges() : 1;
        }

        #region Repositories properties

        public IRepository<UserProfile> UserProfileRepository
        {
            get { return _repoFactories.Repositories.UserProfileRepository; }
        }

        public IRepository<Roles> RolesRepository
        {
            get { return _repoFactories.Repositories.UserRolesRepository; }
        }

        #endregion
    }
}