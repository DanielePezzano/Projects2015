using Models.Base;
using Models.Queues.Enum;
using Models.Universe;
using System.ComponentModel.DataAnnotations;

namespace Models.Queues
{
    public class FleetQueue : BaseQueueEntity
    {
        public int FleetId { get; set; }
        [Display(Name = "Position", ResourceType = typeof(Resources))]
        public Position Position { get; set; }
        [Display(Name = "Transport", ResourceType = typeof(Resources))] 
        public Transport Transport { get; set; }
        [Display(Name = "MissionType", ResourceType = typeof(Resources))]
        public MissionType MissionType { get; set; }

        public virtual Planet Destination { get; set; }
        public virtual Planet StartPoint { get; set; }
    }
}
