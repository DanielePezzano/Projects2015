using Models.Base;
using Models.Tech;
using Models.Universe;
using System.ComponentModel.DataAnnotations;

namespace Models.Queues
{
    public class BuildingQueue : BaseQueueEntity
    {
        [Required()]
        [Display(Name="Number", ResourceType = typeof(Resources))]
        public int Number { get; set; }

        public virtual Technology Technology { get; set; }
        public int? PlanetId { get; set; }
        public int? SatelliteId { get; set; }
    }
}
