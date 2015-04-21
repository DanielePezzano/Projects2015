using Models.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using UnitOfWork.Cache;
using UnitOfWork.Implementations.Context;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Implementations.Repository.BaseRepository
{
    public class RepositoryProduction<T> : IRepository<T> where T : BaseEntity
    {
        internal ProductionContext Context = null;
        internal DbSet<T> dbSet;
        internal DalCache RepoCache= null;
        internal string RepoEntitySign = string.Empty;

        public RepositoryProduction(ProductionContext context, DalCache cache, string repoEntitySign)
        {
            this.Context = context;
            this.RepoCache = cache;
            this.dbSet = this.Context.Set<T>();
        }

        public IQueryable<T> GetAll(string cacheKey)
        {
            IQueryable<T> result = null;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = this.dbSet;
                    RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                }
                else
                    result = (IQueryable<T>)cached;
            }
            else result = this.dbSet;
            return result;
        }

        public T GetByKey(int id, string cacheKey)
        {
            T result = null;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = this.dbSet.FirstOrDefault(c => c.Id == id);
                    RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                }
                else result = (T)cached;
            }
            else result = this.dbSet.FirstOrDefault(c => c.Id == id);
            return result;
        }

        /// <summary>
        /// Restituisce il risultato filtrato, eventualmente ordinato ed eventualmente con
        /// già incluse le sottocategorie.
        /// </summary>
        /// <param name="filter">es. student => student.LastName == "Smith"</param>
        /// <param name="orderBy">es. q => q.OrderBy(s => s.LastName)</param>
        /// <param name="includeProperties">es. Student,StudentInfo</param>
        /// <returns></returns>
        public IEnumerable<T> Get(
            string cacheKey,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IEnumerable<T> result = null;
            if (!string.IsNullOrEmpty(cacheKey))
            {
                var cached = RepoCache.GetMyCachedItem(cacheKey);
                if (cached == null)
                {
                    result = ProcessGet(filter, orderBy, includeProperties, result);
                    RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                }
                else
                    result = (IEnumerable<T>)cached;
            }
            else
            {
                result = ProcessGet(filter, orderBy, includeProperties, result);
            }
            return result;            
        }

        private IEnumerable<T> ProcessGet(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, string includeProperties, IEnumerable<T> result)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                result = orderBy(query).ToList();
            }
            else
            {
                result = query.ToList();
            }
            return result;
        }

        public IQueryable<T> FindBy(Expression<System.Func<T, bool>> predicate, string cacheKey)
        {
            IQueryable<T> result = null;
            try
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    var cached = RepoCache.GetMyCachedItem(cacheKey);
                    if (cached == null)
                    {
                        result = this.dbSet.Where(predicate);
                        RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                    }
                    else
                        result = (IQueryable<T>)cached;
                }
                else
                    result = this.dbSet.Where(predicate);
            }
            catch (Exception ex)
            {
                 //DAFARE : CREARE UN PROGETTO LOGGER CHE SI OCCUPI DI PRENDERE LE ECCEZIONI E SCRIVERLE IN UN FILE APPOSITO
                var message = ex.Message;
                throw;
            }
            return result;
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void Edit(T entity)
        {
            dbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        public int Count(string cacheKey)
        {
            int result = 0;
            try
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    var cached = RepoCache.GetMyCachedItem(cacheKey);
                    if (cached == null)
                    {
                        result = dbSet.Count();
                        RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                    }
                    else result = (int)cached;
                }
                else
                    result = dbSet.Count();
            }
            catch (Exception ex)
            {
                //DAFARE : CREARE UN PROGETTO LOGGER CHE SI OCCUPI DI PRENDERE LE ECCEZIONI E SCRIVERLE IN UN FILE APPOSITO
                var message = ex.Message;
                throw;
            }
            return result;
        }

        public int Count(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            int result = 0;
            try
            {
                if (!string.IsNullOrEmpty(cacheKey))
                {
                    var cached = RepoCache.GetMyCachedItem(cacheKey);
                    if (cached == null)
                    {
                        result = dbSet.Count(predicate);
                        RepoCache.AddToMyCache(cacheKey, result, Cache.Enum.DalCachePriority.Default);
                    }
                    else
                        result = (int)cached;
                }
                else
                    result = dbSet.Count(predicate);
            }
            catch (Exception ex)
            {
                //DAFARE : CREARE UN PROGETTO LOGGER CHE SI OCCUPI DI PRENDERE LE ECCEZIONI E SCRIVERLE IN UN FILE APPOSITO
                var message = ex.Message;
                throw;
            }
            return result;
        }

        public void CustomDbset(List<T> setter)
        {
            
        }
    }
}
