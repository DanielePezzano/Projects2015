using System.Collections.Generic;
using System.Linq;
using Models.Tech;
using SharedDto.Universe.Technology;

namespace SharedDto.DataMapper
{
    public static class TechnologyBonusEntityMapper
    {
        public static TechnologyBonusDto EntityToModel(TechBonus entity)
        {
            return new TechnologyBonusDto()
            {
                Value = entity.Value,
                Bonus = entity.Bonus
            };
        }

        public static List<TechnologyBonusDto> EntityListToModel(List<TechBonus> bonuses)
        {
            return bonuses.Select(EntityToModel).ToList();
        }
    }
}