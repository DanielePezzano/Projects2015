using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Models.Base;
using UnitOfWork.Cache;
using UnitOfWork.Cache.Enum;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository.BaseRepository
{
    public class RepositoryProduction<T> : IRepository<T> where T : BaseEntity
    {
        internal ProductionContext Context;
        internal DbSet<T> DbSet;
        internal DalCache RepoCache;
        internal string RepoEntitySign = string.Empty;

        public RepositoryProduction(ProductionContext context, DalCache cache)
        {
            Context = context;
            RepoCache = cache;
            DbSet = Context.Set<T>();
        }

        public IQueryable<T> GetAll(string cacheKey)
        {
            IQueryable<T> result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = DbSet;
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else
                    result = (IQueryable<T>) cached;
            }
            else result = DbSet;
            return result;
        }

        public T GetByKey(int id, string cacheKey)
        {
            T result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = DbSet.SingleOrDefault(c => c.Id == id);
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else result = (T) cached;
            }
            else result = DbSet.FirstOrDefault(c => c.Id == id);
            return result;
        }

        /// <summary>
        ///     Restituisce il risultato filtrato, eventualmente ordinato ed eventualmente con
        ///     già incluse le sottocategorie.
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="filter">es. student => student.LastName == "Smith"</param>
        /// <param name="includeProperties">es. Student,StudentInfo</param>
        /// <returns></returns>
        public IEnumerable<T> Get(
            string cacheKey,
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "")
        {
            IEnumerable<T> result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = ProcessGet(filter, includeProperties);
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else
                    result = (IEnumerable<T>) cached;
            }
            else
            {
                result = ProcessGet(filter, includeProperties);
            }
            return result;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            IQueryable<T> result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = DbSet.Where(predicate);
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else
                    result = (IQueryable<T>) cached;
            }
            else
                result = DbSet.Where(predicate);
            return result;
        }

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }
            DbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            DbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public int Count(string cacheKey)
        {
            int result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = DbSet.Count();
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else result = (int) cached;
            }
            else
                result = DbSet.Count();
            return result;
        }

        public int Count(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            int result;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = DbSet.Count(predicate);
                    RepoCache.AddToMyCache(cacheKey, result, DalCachePriority.Default);
                }
                else
                    result = (int) cached;
            }
            else
                result = DbSet.Count(predicate);
            return result;
        }

        public void CustomDbset(List<T> setter)
        {
        }

        private IEnumerable<T> ProcessGet(Expression<Func<T, bool>> filter, string includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            IEnumerable<T> result = query.ToList();
            return result;
        }
    }
}