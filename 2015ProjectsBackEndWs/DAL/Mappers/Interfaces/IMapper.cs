using BaseModels;
using SharedDto.Interfaces;

namespace DAL.Mappers.Interfaces
{
    public interface IMapToEntity
    {
        BaseEntity MapToEntity(IDto dto);
    }
}