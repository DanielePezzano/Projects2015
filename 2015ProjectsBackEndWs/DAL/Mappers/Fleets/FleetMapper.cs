using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using SharedDto.Interfaces;

namespace DAL.Mappers.Fleets
{
    class FleetMapper : BaseMapper, IMapper
    {
        public FleetMapper(bool isTest, string connectionString, BaseOperations operations) : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
            var cacheKey = $"Fleet{Entity.Id}";
            return Operations.Any(MappedRepositories.FleetRepository, Entity.Id, cacheKey);
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
