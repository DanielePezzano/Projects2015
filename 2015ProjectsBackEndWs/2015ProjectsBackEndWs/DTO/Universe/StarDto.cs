﻿using Models.Universe;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class StarDto : BaseDto<Star>
    {
        public StarDto(Star model)
            : base(model)
        {
            this.Planets = ConvertModelListToDto(model.Planets);
        }

        private List<PlanetDto> ConvertModelListToDto(ICollection<Planet> collection)
        {
            List<PlanetDto> result = new List<PlanetDto>();
            foreach (Planet planet in collection)
            {
                PlanetDto toAdd = new PlanetDto(planet);
                result.Add(toAdd);
            }
            return result;
        }
        [DataMember]
        public int GalaxyId { get { return Model.Universe.Id; } }
        [DataMember]
        public int PositionX { get { return Model.Coordinate.X; } }
        [DataMember]
        public int PositgionY { get { return Model.Coordinate.Y; } }
        [DataMember]
        public double Mass { get { return Model.Mass; } }
        [DataMember]
        public string Name { get { return Model.Name; } set { Model.Name = value; } }
        [DataMember]
        public int RadiationLevel { get { return Model.RadiationLevel; } }
        [DataMember]
        public double Radius { get { return Model.Radius; } }
        [DataMember]
        public string StarColor { get { return Model.StarColor.ToString(); } }
        [DataMember]
        public string StarType { get { return Model.StarType.ToString(); } }
        [DataMember]
        public int SurfaceTemp { get { return Model.SurfaceTemp; } }
        [DataMember]
        public List<PlanetDto> Planets { get; set; }
    }
}