using System;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace UnitOfWork.Implementations.Context
{
    public class ContextFactory : IContextFactory,IDisposable
    {
        private bool _disposed;
        private IContext _context;

        public ContextFactory(bool isTest=false)
        {
            if (isTest) _context = new TestContext();
            else _context = new ProductionContext();
        }

        public IContext Retrieve()
        {
            return _context;
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

        public DalCache CreateCache()
        {
            return new DalCache();
        }
    }
}
