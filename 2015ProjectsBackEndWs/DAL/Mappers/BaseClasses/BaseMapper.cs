using System;
using DAL.Operations.IstanceFactory;
using Models.Base;

namespace DAL.Mappers.BaseClasses
{
    public abstract class BaseMapper
    {
        protected OpFactory Operations { get; set; }
        public bool ExsistEntity;
        protected BaseEntity Entity;
        protected string ConnectionString;

        protected BaseMapper(string connectionString, OpFactory operations)
        {
            if (operations == null) throw new ArgumentNullException(nameof(operations));
            Operations = operations;
            ConnectionString = connectionString;
        }

        public abstract bool ExistsEntity();
    }
}