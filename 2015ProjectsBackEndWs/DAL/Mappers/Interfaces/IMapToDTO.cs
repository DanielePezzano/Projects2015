using Models.Base;
using SharedDto.Interfaces;

namespace DAL.Mappers.Interfaces
{
    public interface IMapToDto
    {
        IDto MapToDto(BaseEntity entity); 
    }
}