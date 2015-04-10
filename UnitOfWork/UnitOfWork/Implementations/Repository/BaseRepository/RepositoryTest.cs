using Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository.BaseRepository
{
    public class RepositoryTest<T> : IRepository<T> where T : BaseEntity
    {
        internal TestContext Context = null;
        internal List<T> dbSet;
        internal DalCache RepoCache = null;
        internal string RepoEntitySign = string.Empty;

        public RepositoryTest(TestContext context, DalCache cache, string repoEntitySign)
        {
            this.Context = context;
        }

        public IQueryable<T> GetAll()
        {
            return (IQueryable<T>)dbSet;
        }

        public T GetByKey(int id)
        {
            return dbSet.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = (IQueryable<T>)dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public void Add(T entity)
        {
            if (dbSet == null) dbSet = new List<T>();
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Remove(entity);
            dbSet.Add(entity);
        }

        public int Count()
        {
            return dbSet.Count;
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
