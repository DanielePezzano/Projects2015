using System;
using Models.Base;
using SharedDto.Interfaces;

namespace DAL.Mappers.Interfaces
{
    public interface IMapper
    {
        BaseEntity MapToEntity(IDto dto);
        IDto MapToDto(BaseEntity entity);
    }
}