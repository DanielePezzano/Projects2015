using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AuthModel.Base;
using AutUow.Cache;
using AutUow.Implementations.Context;
using AutUow.Interfaces;

namespace AutUow.Implementations.Repository.BaseRepository
{
    public class RepositoryTest<T> : IRepository<T> where T : BaseEntity
    {
        internal TestContext Context;
        internal List<T> DbSet;
        internal DalCache RepoCache;
        internal string RepoEntitySign = string.Empty;

        public RepositoryTest(TestContext context, DalCache cache, string repoEntitySign)
        {
            Context = context;
            RepoCache = cache;
            RepoCache.GetMyCachedItem(repoEntitySign);
        }

        public void CustomDbset(List<T> setter)
        {
            DbSet = setter;
        }

        public IQueryable<T> GetAll(string cacheKey)
        {
            return (IQueryable<T>) DbSet;
        }

        public T GetByKey(int id, string cacheKey)
        {
            return DbSet.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            return (IQueryable<T>) (DbSet.Where(predicate.Compile()).ToList());
        }

        public IEnumerable<T> Get(
            string cacheKey,
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "")
        {
            var query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter.Compile()).ToList();
            }

            return query;
        }

        public void Add(T entity)
        {
            if (DbSet == null) DbSet = new List<T>();
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            DbSet.Remove(entity);
            DbSet.Add(entity);
        }

        public int Count(string cacheKey)
        {
            return DbSet.Count;
        }

        public int Count(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            return DbSet.Count(predicate.Compile());
        }
    }
}