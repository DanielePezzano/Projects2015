using Models.Base;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Mappers.BaseClasses
{
    public abstract class BaseMapper
    {
        public bool ExsistEntity { get; set; }
        protected bool IsTest { get; set; }
        protected IUnitOfWork UnitOfWork { get; set; }
        protected BaseEntity Entity { get; set; }

        protected BaseMapper(IUnitOfWork uow, bool isTest)
        {
            IsTest = isTest;
            UnitOfWork = uow;
        }

        public abstract bool ExistsEntity();
    }
}