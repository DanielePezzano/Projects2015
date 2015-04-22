﻿using Models.Base;
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
            this.RepoCache = cache;
            var cached = RepoCache.GetMyCachedItem(repoEntitySign);            
        }

        public void CustomDbset(List<T> setter)
        {
            dbSet = setter;
        }

        public IQueryable<T> GetAll(string cacheKey)
        {
            return (IQueryable<T>)dbSet;
        }

        public T GetByKey(int id, string cacheKey)
        {
            return dbSet.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(
            string cacheKey,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            List<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter.Compile()).ToList();
            }

            //foreach (var includeProperty in includeProperties.Split
            //    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //{
            //    query = query.Include(includeProperty);
            //}

            //if (orderBy != null)
            //{
            //    return order
            //}
            //else
            //{
            //    return query.ToList();
            //}
            return query;
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

        public int Count(string cacheKey)
        {
            return dbSet.Count;
        }

        public int Count(Expression<Func<T, bool>> predicate, string cacheKey)
        {
            return dbSet.Count(predicate.Compile());
        }
    }
}
