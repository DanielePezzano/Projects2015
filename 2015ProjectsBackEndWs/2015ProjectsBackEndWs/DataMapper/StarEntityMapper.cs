using _2015ProjectsBackEndWs.DTO;
using _2015ProjectsBackEndWs.DTO.Universe;
using Models.Universe;
using System;
using System.Collections.Generic;

namespace _2015ProjectsBackEndWs.DataMapper
{
    public class StarEntityMapper
    {
        /// <summary>
        /// Map an entity to the correspondent DTO
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public StarDto EntityToModel(Star entity)
        {
            if (entity == null) return null;
            StarDto result = new StarDto();
            PlanetEntityMapper mapper = new PlanetEntityMapper();
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
            return result;
        }
        /// <summary>
        /// Map an entity List to the correspondent DTO List
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<StarDto> EntityListToModel(List<Star> stars)
        {
            List<StarDto> result = new List<StarDto>();
            foreach (Star star in stars)
            {
                result.Add(EntityToModel(star));
            }
            return result;
        }
    }
}