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

        protected BaseMapper(OpFactory operations)
        {
            if (operations == null) throw new ArgumentNullException(nameof(operations));
            Operations = operations;
        }

        public abstract bool ExistsEntity();
    }
}