using System;
using DAL.Operations;
using DAL.Operations.Enums;
using DAL.Operations.IstanceFactory;
using Models.Users;
using UnitOfWork.Interfaces.UnitOfWork;

namespace BLL.Information
{
    public sealed class RetrieveUserInformation : IDisposable
    {
        private readonly string _email = string.Empty;
        private OpFactory _opFactory;
        private bool _disposed;

        public IUnitOfWork MainUow { get; set; }

        public RetrieveUserInformation(OpFactory opFactory)
        {
            _opFactory = opFactory;
        }

        public RetrieveUserInformation(string email, OpFactory opFactory) :
            this(opFactory)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");
            _email = email;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
        }

        /// <summary>
        ///     controlla l'esistenza di un email nel database.
        /// </summary>
        /// <returns></returns>
        public bool ExistsEmail(IUnitOfWork uow=null)
        {
            var cacheKey = $"WhereEmailEqualsTo=>{_email}";
            return _opFactory.SetOperation<User>(MappedRepositories.UserRepository, MappedOperations.FindByEmail, cacheKey,c=>c.Email==_email,uow).CheckResult;
        }
    }
}