using Models.Base;
using Models.Tech;
using Models.Universe;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Queues
{
    [DataContract(IsReference=true)]
    public class BuildingQueue : BaseQueueEntity
    {
        [Required()]
        [Display(Name = "Number", ResourceType = typeof(Resources))]
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public virtual Technology Technology { get; set; }
        [DataMember]
        public int? PlanetId { get; set; }
        [DataMember]
        public int? SatelliteId { get; set; }
    }
}