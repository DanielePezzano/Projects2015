using System.Collections.Generic;
using System.Linq;
using Models.Users;
using SharedDto.Universe.Race;

namespace SharedDto.DataMapper
{
    public static class RaceEntityMapper
    {
        private static List<RaceBonusDto> EntityToRaceBonusDto(User entity)
        {
            return entity.RaceBonuses.Select(bonus => new RaceBonusDto()
            {
                TraitType = bonus.TraitType, Bonus = bonus.Bonus, Value = bonus.Value
            }).ToList();
        }

        public static RaceDto EntityToModel(User entity)
        {
            return new RaceDto()
            {
                RaceName = entity.RaceName,
                RacePointsLeft = entity.RacePointsLeft,
                RacePointsUsed = entity.RacePointsUsed,
                RaceBonuses = EntityToRaceBonusDto(entity)
            };
        }

    }
}