using BaseModels;
using SharedDto.Interfaces;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Operations.BaseClasses
{
    public struct OperationResult
    {
        public bool CheckResult;
        public BaseEntity Entity;
        public IDto ResultDto;
        public object RawResult;
        public IUnitOfWork UsedUnitOfWork;
    }
}