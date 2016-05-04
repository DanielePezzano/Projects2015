using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Mappers.User.IstanceFactory;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Users.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.User;

namespace DAL.Mappers.User
{
    class UserMapper : BaseMapper, IMapper
    {
        public UserMapper(string connectionString, BaseOperations operations, bool isTest = false)
            : base(isTest, connectionString, operations)
        {
        }

        public override bool ExistsEntity()
        {
             var cacheKey = $"USERCOUNT{Entity.Id}";
            return Operations.Any(MappedRepositories.PlanetRepository, Entity.Id, cacheKey);
        }

        public BaseEntity MapToEntity(IDto dto)
        {
            var userDto = (UserDto) dto;
            Entity = new Models.Users.User()
            {
                Id = userDto.Id,
                Email = userDto.Email,
                Photo = userDto.Photo,
                RaceName = userDto.Race.RaceName,
                RacePointsUsed = userDto.Race.RacePointsUsed,
                Status = (UserStatus)Enum.Parse(typeof(UserStatus),userDto.Status),
                UpdatedAt = DateTime.Now,
                UserName = userDto.Username,
                Planets = MapperFactory.RetrievePlanetMapper(ConnectionString,Operations,IsTest).ModelListToEntity(userDto.Planets),
                Satellites = MapperFactory.RetrievePlanetMapper(ConnectionString,Operations,IsTest).ModelListToSatellites(userDto.Satellites),
                RaceBonuses = UserMapperFactory.RetrieveBonusMapper(ConnectionString,Operations,IsTest).ModelListToEntity(userDto.Race.RaceBonuses),
                //Fleets = 
            };

            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
