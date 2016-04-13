using System.Collections.Generic;
using System.Runtime.Serialization;
using Models.Universe.Strcut;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class FleetDto
    {
        [DataMember]
        public int FleetId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int TravelSpeed { get; set; }
        [DataMember]
        public int Range { get; set; }
        [DataMember]
        public bool AtBay { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public Coordinates Position { get; set; }
        [DataMember]
        public List<ShipClassDto> ShipClassDtos { get; set; }
    }
}