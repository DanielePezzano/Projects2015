using BaseModels;
using SharedDto.Interfaces;

namespace DAL.Mappers.Interfaces
{
    public interface IMapToDto
    {
        IDto MapToDto(BaseEntity entity); 
    }
}