using System.Runtime.Serialization;

namespace SharedDto.UtilityDto
{
    [DataContract]
    public class UniverseRangeDto
    {
        [DataMember]
        public int MinX { get; set; }
        [DataMember]
        public int MaxX { get; set; }
        [DataMember]
        public int MinY { get; set; }
        [DataMember]
        public int MaxY { get; set; }
        [DataMember]
        public BaseAuthDto Auth { get; set; }
    }
}
