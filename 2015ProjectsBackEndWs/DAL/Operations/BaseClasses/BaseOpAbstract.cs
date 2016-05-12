using DAL.Operations.Enums;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.BaseClasses
{
    public abstract class BaseOpAbstract
    {
        protected bool IsTest;
        protected string ConnectionString;
        protected IUnitOfWork Uow;
        public string CacheKey;
        public int EntityId;
        public OperationResult OperationResult;
        

        protected BaseOpAbstract(bool isTest, string connectionString)
        {
            IsTest = isTest;
            ConnectionString = connectionString;
        }

        protected abstract void Any();
        protected abstract void GetById();
        protected abstract void Find(dynamic predicate);
        public abstract void Perform(MappedOperations desiredOperation, dynamic predicate=null);

    }
}