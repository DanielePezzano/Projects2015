using Models.Base;
using SharedDto.Interfaces;

namespace DAL.Operations.BaseClasses
{
    public struct OperationResult
    {
        public bool CheckResult;
        public BaseEntity Entity;
        public IDto ResultDto;
        public object ListResult;
    }
}