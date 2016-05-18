using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;
using SharedDto.Universe.Planets;

namespace SharedDto.Universe.Stars
{
    [DataContract]
    public class StarDto : BaseDto,IDto
    {
        [DataMember]
        public int GalaxyId { get; set; }

        [DataMember]
        public int PositionX { get; set; }

        [DataMember]
        public int PositionY { get; set; }

        [DataMember]
        public double Mass { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int RadiationLevel { get; set; }

        [DataMember]
        public double Radius { get; set; }

        [DataMember]
        public string StarColor { get; set; }

        [DataMember]
        public string StarType { get; set; }

        [DataMember]
        public int SurfaceTemp { get; set; }

        [DataMember]
        public List<PlanetDto> Planets { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }  

    }
}