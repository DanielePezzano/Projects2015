using System;
using System.Linq;
using System.Linq.Expressions;
using BaseModels;

namespace UnitOfWork.Interfaces.Repository
{
    public interface IQueryableRepository<T> where T:BaseEntity
    {
        IQueryable<T> GetAll(string cacheKey);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string cacheKey);
    }
}