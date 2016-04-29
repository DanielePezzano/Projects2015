using System;
using DAL.Operations.BaseClasses;
using Models.Base;

namespace DAL.Mappers.BaseClasses
{
    public abstract class BaseMapper
    {
        protected BaseOperations Operations { get; set; }
        public bool ExsistEntity;
        protected bool IsTest;
        protected BaseEntity Entity;
        protected string ConnectionString;

        protected BaseMapper(bool isTest, string connectionString, BaseOperations operations)
        {
            if (operations == null) throw new ArgumentNullException(nameof(operations));
            Operations = operations;
            IsTest = isTest;
            ConnectionString = connectionString;
        }

        public abstract bool ExistsEntity();
    }
}