using System;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Information
{
    public sealed class RetrieveUserInformation : IDisposable
    {
        private readonly string _email = string.Empty;
        private BaseOperations _operations;
        private bool _disposed;
        private bool _isTest;

        public IUnitOfWork MainUow { get; set; }

        public RetrieveUserInformation(IUnitOfWork uow, BaseOperations operations, bool isTest = false)
        {
            if (uow == null) throw new ArgumentNullException(nameof(uow));
            MainUow = uow;
            _operations = operations;
            _isTest = isTest;
        }

        public RetrieveUserInformation(IUnitOfWork uow, string email, UserOperations operations, bool isTest = false):
            this(uow, operations, isTest)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");
            _email = email;
        }

        

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            if (_isTest) ((ProductionUow)MainUow)?.Dispose();
            else ((TestUow)MainUow)?.Dispose();
        }

        /// <summary>
        ///     controlla l'esistenza di un email nel database.
        /// </summary>
        /// <returns></returns>
        public bool ExistsEmail()
        {
            var cacheKey = $"WhereEmailEqualsTo=>{_email}";
            
            return (_isTest)?
                ((TestUow)MainUow)?.UserRepository.Count(c => c.Email == _email, "" + _email) > 0 :
                ((ProductionUow)MainUow)?.UserRepository.Count(c => c.Email == _email, "WhereEmailEqualsTo=>" + _email) > 0;
        }
    }
}