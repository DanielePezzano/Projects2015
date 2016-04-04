using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.Universe.Stars;

namespace SharedDto.Universe.Sector
{
    [DataContract]
    public class SectorDto
    {
        [DataMember]
        public List<StarDto> Stars { get; set; }
        [DataMember]
        public int MaxNumberOfStars { get; set; }
        [DataMember]
        public int MinX { get; set; }
        [DataMember]
        public int MaxX { get; set; }
        [DataMember]
        public int MinY { get; set; }
    }
}
