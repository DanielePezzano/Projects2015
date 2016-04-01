using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto.Universe.Stars
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
