using System;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrieveUserInformation : IDisposable
    {
        private readonly string _email = string.Empty;
        private bool _disposed;

        public MainUow MainUow { get; set; }

        public RetrieveUserInformation(MainUow uow)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            MainUow = uow;
        }

        public RetrieveUserInformation(MainUow uow, string email)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");
            _email = email;
            MainUow = uow;
        }

        

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                if (MainUow != null) MainUow.Dispose();
            }
            //GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     controlla l'esistenza di un email nel database.
        /// </summary>
        /// <returns></returns>
        public bool ExistsEmail()
        {
            return MainUow.UserRepository.Count(c => c.Email == _email, "WhereEmailEqualsTo=>" + _email) > 0;
        }
    }
}