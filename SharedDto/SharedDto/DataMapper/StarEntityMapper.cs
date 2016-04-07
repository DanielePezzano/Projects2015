using System.Collections.Generic;
using System.Linq;
using Models.Universe;
using SharedDto.Universe.Stars;

namespace SharedDto.DataMapper
{
    public static class StarEntityMapper
    {
        /// <summary>
        ///     Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static StarDto EntityToModel(Star entity)
        {
            return new StarDto()
            {
                GalaxyId = entity.Galaxy.Id,
                Mass = entity.Mass,
                Name = entity.Name,
                Planets = PlanetEntityMapper.EntityListToModel(entity.Planets),
                PositgionY = entity.CoordinateY,
                PositionX = entity.CoordinateX,
                RadiationLevel = entity.RadiationLevel,
                Radius = entity.Radius,
                StarColor = entity.StarColor.ToString(),
                StarType = entity.StarType.ToString(),
                SurfaceTemp = entity.SurfaceTemp,
                StarId = entity.Id
            };
            
        }

        /// <summary>
        ///     Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public static List<StarDto> EntityListToModel(List<Star> stars)
        {
            return stars.Select(EntityToModel).ToList();
        }
    }
}