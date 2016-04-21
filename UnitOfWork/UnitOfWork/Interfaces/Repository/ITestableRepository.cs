using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Models.Base;

namespace UnitOfWork.Interfaces.Repository
{
    public interface ITestableRepository<T> where T:BaseEntity
    {
        IEnumerable<T> GetAll(string cacheKey);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate, string cacheKey);
    }
}