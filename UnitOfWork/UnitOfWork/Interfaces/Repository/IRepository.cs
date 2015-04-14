using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetByKey(int id);
        void CustomDbset(List<T> setter);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
    }
}
