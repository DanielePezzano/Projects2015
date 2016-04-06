using System.Collections.Generic;
using System.Linq;
using Models.Users;
using SharedDto.Universe.Race;

namespace SharedDto.DataMapper
{
    public sealed class RaceEntityMapper
    {
        private List<RaceBonusDto> EntityToRaceBonusDto(User entity)
        {
            return entity.RaceBonuses.Select(bonus => new RaceBonusDto()
            {
                TraitType = bonus.TraitType, Bonus = bonus.Bonus, Value = bonus.Value
            }).ToList();
        }

        public RaceDto EntityToModel(User entity)
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