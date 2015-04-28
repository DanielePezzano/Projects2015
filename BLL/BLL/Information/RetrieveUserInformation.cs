using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Implementations.Uows;

namespace BLL.Information
{
    public sealed class RetrieveUserInformation :IDisposable
    {
        public MainUow _MainUow { get; set; }
        private bool _Disposed = false;
        private int _ItemId = -1;
        private string _Email = string.Empty;

        public RetrieveUserInformation(MainUow uow, int itemId)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (itemId < 0) throw new ArgumentException("itemId");
            this._MainUow = uow;
            this._ItemId = itemId;
        }

        public RetrieveUserInformation(MainUow uow,string email)
        {
            if (uow == null) throw new ArgumentNullException("uow");
            if (string.IsNullOrEmpty(email)) throw new ArgumentException("email");
            this._Email = email;
            this._MainUow = uow;
        }
        /// <summary>
        /// controlla l'esistenza di un email nel database.
        /// </summary>
        /// <returns></returns>
        public bool ExistsEmail()
        {
            return this._MainUow.UserRepository.Count(c => c.Email == this._Email,"WhereEmailEqualsTo=>"+_Email) > 0 ? true : false;
        }

        public void Dispose()
        {
            if (!_Disposed)
            {
                _Disposed = true;
                if (_MainUow != null) _MainUow.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
