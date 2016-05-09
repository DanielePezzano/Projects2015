using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Fleet
{
    [DataContract]
    public class ShieldDto : BaseDto, IDto, ICosts, ISpaces, IMaintenanceCost
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Protection { get; set; }
        [DataMember]
        public int RequiredEnergy { get; set; }
        [DataMember]
        public int SpacesNeeded { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
    }
}
