using DAL.Operations.Enums;
using Models.Base;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.BaseClasses
{
    public abstract class BaseOpAbstract
    {
        protected bool IsTest;
        protected string ConnectionString;
        public IUnitOfWork Uow;
        public string CacheKey;
        public int EntityId;
        public OperationResult OperationResult;


        protected BaseOpAbstract(bool isTest, string connectionString)
        {
            IsTest = isTest;
            ConnectionString = connectionString;
        }

        protected void SetUsedUnitOfWork()
        {
            OperationResult.UsedUnitOfWork = Uow;
        }

        protected void SaveUow()
        {
            Uow?.Save();
        }

        protected abstract void GetAll();
        protected abstract void SaveEntity(BaseEntity entity);
        protected abstract void Any();
        protected abstract void GetById();
        protected abstract void Find(dynamic predicate);
        public abstract void Perform(MappedOperations desiredOperation, dynamic predicate=null);
    }
}