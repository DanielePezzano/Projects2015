using System;
using UnitOfWork.Implementations.Uows.UowDto;
using UnitOfWork.Interfaces.Context;

namespace UnitOfWork.Implementations.Uows.BaseClass
{
    public abstract class BaseUow : IDisposable
    {
        protected readonly UowRepositoryFactories _repoFactories;
        protected IContext _context;
        protected bool _disposed;

        protected BaseUow(IContext context, UowRepositoryFactories repoFactories)
        {
            if (context != null) _context = context;
            else throw new ArgumentNullException(nameof(context));
            if (repoFactories != null) _repoFactories = repoFactories;
            else throw new ArgumentNullException(nameof(repoFactories));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void CheckInitialization();

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
    }
}