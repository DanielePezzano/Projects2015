using System.Collections.Generic;
using System.Linq;
using Models.Tech;
using SharedDto.Universe.Technology;

namespace SharedDto.DataMapper
{
    public static class TechnologyEntityMapper
    {
        public static TechnologyDto EntityToModel(Technology entity)
        {
            return new TechnologyDto()
            {
                Id = entity.Id,
                Description = entity.Description,
                Field = entity.Field.ToString(),
                MoneyCost = entity.MoneyCost,
                Name = entity.Name,
                OreCost = entity.OreCost,
                ResearchPoints = entity.ResearchPoints,
                SubField = entity.SubField.ToString(),
                NeededTechnologies = entity.TechRequisites.Select(c=>c.Requisite.Id).ToList(),
                TechnologyBonuses = TechnologyBonusEntityMapper.EntityListToModel(entity.TechBonuses.ToList())
            };
        }

        public static List<TechnologyDto> EntityListToModel(List<Technology> technologies)
        {
            return technologies.Select(EntityToModel).ToList();
        }
    }
}