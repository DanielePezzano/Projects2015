using Models.Base;
using Models.Queues.Enum;
using Models.Universe;
using Models.Universe.Strcut;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Queues
{
    [DataContract]
    public class FleetQueue : BaseQueueEntity
    {
        [Required()]
        [DataMember]
        public int FleetId { get; set; }
        [Required()]
        [Display(Name = "Position", ResourceType = typeof(Resources))]
        [DataMember]
        public Coordinates Position { get; set; }
        [Required()]
        [Display(Name = "Transport", ResourceType = typeof(Resources))]
        [DataMember]
        public Transport Transport { get; set; }
        [Required()]
        [Display(Name = "MissionType", ResourceType = typeof(Resources))]
        [DataMember]
        [EnumDataType(typeof(MissionType))]
        public MissionType MissionType { get; set; }
        [Required()]
        [Display(Name = "Destination", ResourceType = typeof(Resources))]
        [DataMember]
        public int DestinationId { get; set; }
        [DataMember]
        public bool IsDestinationSatellite { get; set; }
        [Required()]
        [Display(Name = "StartPoint", ResourceType = typeof(Resources))]
        [DataMember]
        public int StartPointId { get; set; }
        [DataMember]
        public bool IsStartPointSatellite { get; set; }
    }
}