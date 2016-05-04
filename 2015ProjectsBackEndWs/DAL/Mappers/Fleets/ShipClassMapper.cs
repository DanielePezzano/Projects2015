using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using SharedDto.Interfaces;

namespace DAL.Mappers.Fleets
{
    public class ShipClassMapper : BaseMapper, IMapper
    {
        public ShipClassMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"SHIPCLASS{Entity.Id}";
            return Operations.Any(MappedRepositories.ShipClassRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            throw new System.NotImplementedException();
        }

        public IDto MapToDto(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}