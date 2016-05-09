using System;
using DAL.Mappers.BaseClasses;
using DAL.Mappers.Fleets;
using DAL.Mappers.Fleets.Enums;
using DAL.Mappers.Fleets.IstanceFactory;
using DAL.Mappers.Interfaces;
using DAL.Mappers.Universe;
using DAL.Mappers.Universe.Enums;
using DAL.Mappers.Universe.IstanceFactory;
using DAL.Mappers.User.Enums;
using DAL.Mappers.User.IstanceFactory;
using DAL.Operations;
using DAL.Operations.BaseClasses;
using Models.Base;
using Models.Users.Enum;
using SharedDto.Interfaces;
using SharedDto.Universe.Race;
using SharedDto.Universe.User;

namespace DAL.Mappers.User
{
   public class UserMapper : BaseMapper,  IMapToDto,IMapToEntity
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
            var planetMapper =
                ((PlanetMapper)
                    MapperFactory.RetrieveMapper(IsTest, ConnectionString, Operations, UniverseMapperTypes.Planets));
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
                Planets = planetMapper.ModelListToEntity(userDto.Planets),
                Satellites = planetMapper.ModelListToSatellites(userDto.Satellites),
                RaceBonuses = ((RaceBonusMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.RaceBonus)).ModelListToEntity(userDto.Race.RaceBonuses),
                Fleets = ((FleetMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Fleet)).ModelListToEntity(userDto.Fleets),
                ShipClasses = ((ShipClassMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Ship)).ModelListToEntity(userDto.ShipClasses),
                Technologies = ((TechMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.Technologies)).ModelListToEntity(userDto.Technologies),
                Researches = ((ResearchQueueMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.ResearchQueue)).ModelListToEntity(userDto.Researches)
            };

            return Entity;
        }

        public IDto MapToDto(BaseEntity entity)
        {
            var userEntity = (Models.Users.User) entity;
            var planetMapper =
                ((PlanetMapper)
                    MapperFactory.RetrieveMapper(IsTest, ConnectionString, Operations, UniverseMapperTypes.Planets));
            return new UserDto()
            {
                Id = userEntity.Id,
                Satellites = planetMapper.EntityListToSatelliteModel(userEntity.Satellites),
                Planets = planetMapper.EntityListToModel(userEntity.Planets),
                Status = userEntity.Status.ToString(),
                Race = new RaceDto()
                {
                    RaceBonuses =  ((RaceBonusMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.RaceBonus)).EntityListToModel(userEntity.RaceBonuses),
                    RaceName = userEntity.RaceName,
                    RacePointsUsed = userEntity.RacePointsUsed,
                    RacePointsLeft = userEntity.RacePointsLeft
                },
                Researches = ((ResearchQueueMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.ResearchQueue)).EntityListToModel(userEntity.Researches),
                Email = userEntity.Email,
                Fleets = ((FleetMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Fleet)).EntityListToModel(userEntity.Fleets),
                Photo = userEntity.Photo,
                ShipClasses = ((ShipClassMapper)FleetMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,FleetMapperTypes.Ship)).EntityListToModel(userEntity.ShipClasses),
                Username = userEntity.UserName,
                Technologies = ((TechMapper)UserMapperFactory.RetrieveMapper(IsTest,ConnectionString,Operations,UserMapperTypes.Technologies)).EntityListToModel(userEntity.Technologies)
            };
        }
    }
}
