using System;
using System.Linq.Expressions;
using DAL.Operations.BaseClasses;
using DAL.Operations.Enums;
using Models.Base;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.IstanceFactory
{
    public class OpFactory
    {
        private readonly string _connectionString;
        public bool IsTest { get; set; }
        

        public OpFactory(string connectionString, bool isTest = false)
        {
            _connectionString = connectionString;
            IsTest = isTest;
        }

        public OperationResult SetOperation(MappedRepositories selectedRepositories, MappedOperations desiredOperation, string cacheKey, int entityId)
        {
            var operation = IstancesCreator.SelectOperator(selectedRepositories, IsTest, _connectionString);
            operation.EntityId = entityId;
            operation.CacheKey = cacheKey;
            operation.Perform(desiredOperation);
            return operation.OperationResult;
        }

        public OperationResult SetOperation<T>(MappedRepositories selectedRepositories, MappedOperations desiredOperation, string cacheKey, Expression<Func<T, bool>> predicate,IUnitOfWork uow=null) where T : BaseEntity
        {
            var operation = IstancesCreator.SelectOperator(selectedRepositories, IsTest, _connectionString, uow);
            if (!IsTest && uow != null) operation.Uow = uow;
            operation.CacheKey = cacheKey;
            operation.Perform(desiredOperation,predicate);
            return operation.OperationResult;
        }
    }
}