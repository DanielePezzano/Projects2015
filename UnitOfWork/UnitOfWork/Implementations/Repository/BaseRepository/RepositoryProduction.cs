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
            var cached = RepoCache.GetMyCachedItem(repoEntitySign);
            if (cached == null)
            {
                RepoCache.AddToMyCache(repoEntitySign, this.Context.Set<T>(),Cache.Enum.DalCachePriority.Default);
                cached = RepoCache.GetMyCachedItem(repoEntitySign);
            }
            this.dbSet = (DbSet<T>)cached;
        }

        public IQueryable<T> GetAll()
        {
            
            return this.dbSet;
        }

        public T GetByKey(int id)
        {
            return this.dbSet.FirstOrDefault(c => c.Id == id);
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
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
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
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IQueryable<T> FindBy(Expression<System.Func<T, bool>> predicate)
        {
            return this.dbSet.Where(predicate);
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

        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public void CustomDbset(List<T> setter)
        {
            
        }
    }
}
