using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        
        T GetByKey(int id, string cacheKey);
        void CustomDbset(List<T> setter);
        IEnumerable<T> Get(
            string cacheKey,
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "");
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        int Count(string cacheKey);
        int Count(Expression<Func<T, bool>> predicate, string cacheKey);
    }
}
