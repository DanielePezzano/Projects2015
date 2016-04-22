using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.IstanceFactory;
using Models.Base;
using Models.Universe;
using Models.Universe.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Stars;
using UnitOfWork.Implementations.Uows;
using UnitOfWork.Interfaces.UnitOfWork;

namespace DAL.Mappers.User
{
    class UserMapper : BaseMapper, IMapper
    {
        public UserMapper(IUnitOfWork uow, bool isTest=false) : base(uow, isTest)
        {
        }

        public override bool ExistsEntity()
        {
            return (IsTest)
                ? ((TestUow) UnitOfWork)?.UserRepository.Count(c => c.Id == Entity.Id,"USERCOUNT"+Entity.Id) > 0
                : ((ProductionUow) UnitOfWork)?.UserRepository.Count(c => c.Id == Entity.Id,"USERCOUNT"+Entity.Id) > 0;
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            throw new NotImplementedException();
        }

        public IDto MapToDto(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
