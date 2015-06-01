using System;
using AuthModel;
using AutUow.Interfaces;

namespace AutUow.Implementations.UnitOfWork.Dto
{
    public class UowRepositories : IDisposable
    {
        private bool _disposed;
        public IRepository<UserProfile> UserProfileRepository { get; set; }
        public IRepository<Roles> UserRolesRepository { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;
            _disposed = true;
            if (UserProfileRepository != null) UserProfileRepository = null;
            if (UserRolesRepository != null) UserRolesRepository = null;
        }
    }
}