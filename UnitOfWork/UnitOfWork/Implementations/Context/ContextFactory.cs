using System;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace UnitOfWork.Implementations.Context
{
    public class ContextFactory : IContextFactory,IDisposable
    {
        private bool disposed = false;
        private IContext _Context = null;

        public ContextFactory(bool isTest=false)
        {
            if (isTest) _Context = new TestContext();
            else _Context = new ProductionContext();
        }

        public IContext Retrieve()
        {
            return _Context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                if (_Context != null) _Context = null;
            }
            disposed = true;
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
