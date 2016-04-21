using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Models.Base;

namespace UnitOfWork.Interfaces.Repository
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    }
}