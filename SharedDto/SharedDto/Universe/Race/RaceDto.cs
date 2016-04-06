using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto.Universe.Race
{
    [DataContract]
    public class RaceDto
    {
        [DataMember]
        public string RaceName { get; set; }
        [DataMember]
        public int RacePointsUsed { get; set; }
        [DataMember]
        public int RacePointsLeft { get; set; }
        [DataMember]
        public List<RaceBonusDto> RaceBonuses { get; set; }
    }
}