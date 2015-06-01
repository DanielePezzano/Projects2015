using System;
using AutUow.Cache;
using AutUow.Implementations.UnitOfWork.Dto;
using AutUow.Interfaces;

namespace AutUow.Implementations.Context
{
    public class ContextFactory : IContextFactory, IDisposable
    {
        private IContext _context;
        private bool _disposed;

        public ContextFactory(bool isTest = false)
        {
            if (isTest) _context = new TestContext();
            else _context = new ProductionContext();
        }

        public IContext Retrieve()
        {
            return _context;
        }

        public DalCache CreateCache()
        {
            return new DalCache();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                _context = null;
            }
            _disposed = true;
        }

        public UowRepositories CreateRepositories()
        {
            return new UowRepositories();
        }
    }
}