using System.Collections.Generic;
using System.Linq;
using Models.Universe;
using SharedDto.Universe.Stars;

namespace SharedDto.DataMapper
{
    public class StarEntityMapper
    {
        /// <summary>
        ///     Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public StarDto EntityToModel(Star entity)
        {
            if (entity == null) return null;
            var result = new StarDto();
            var mapper = new PlanetEntityMapper();
            result.GalaxyId = entity.Galaxy.Id;
            result.Mass = entity.Mass;
            result.Name = entity.Name;
            result.Planets = mapper.EntityListToModel(entity.Planets);
            result.PositgionY = entity.CoordinateY;
            result.PositionX = entity.CoordinateX;
            result.RadiationLevel = entity.RadiationLevel;
            result.Radius = entity.Radius;
            result.StarColor = entity.StarColor.ToString();
            result.StarType = entity.StarType.ToString();
            result.SurfaceTemp = entity.SurfaceTemp;
            result.GalaxyId = entity.Id;
            result.StarId = entity.Id;
            return result;
        }

        /// <summary>
        ///     Map an entity List to the correspondent DTO List
        /// </summary>
        /// <returns></returns>
        public List<StarDto> EntityListToModel(List<Star> stars)
        {
            return stars.Select(EntityToModel).ToList();
        }
    }
}