using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.Universe.Planets;

namespace SharedDto.Universe.Stars
{
    [DataContract]
    public class StarDto
    {
        [DataMember]
        public int GalaxyId { get; set; }

        [DataMember]
        public int PositionX { get; set; }

        [DataMember]
        public int PositgionY { get; set; }

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
    }
}