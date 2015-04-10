using Models.Base;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace UnitOfWork.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll();
        T GetByKey(int id);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
    }
}
