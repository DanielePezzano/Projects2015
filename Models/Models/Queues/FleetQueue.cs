using Models.Base;
using Models.Queues.Enum;
using Models.Universe;
using Models.Universe.Strcut;
using System.ComponentModel.DataAnnotations;

namespace Models.Queues
{
    public class FleetQueue : BaseQueueEntity
    {
        [Required()]
        public int FleetId { get; set; }
        [Required()]
        [Display(Name = "Position", ResourceType = typeof(Resources))]
        public Coordinates Position { get; set; }
        [Required()]
        [Display(Name = "Transport", ResourceType = typeof(Resources))] 
        public Transport Transport { get; set; }
        [Required()]
        [Display(Name = "MissionType", ResourceType = typeof(Resources))]
        public MissionType MissionType { get; set; }
        [Required()]
        [Display(Name = "Destination", ResourceType = typeof(Resources))]
        public int DestinationId { get; set; }
        public bool IsDestinationSatellite { get; set; }
        [Required()]
        [Display(Name = "StartPoint", ResourceType = typeof(Resources))]
        public int StartPointId { get; set; }
        public bool IsStartPointSatellite { get; set; }
    }
}
