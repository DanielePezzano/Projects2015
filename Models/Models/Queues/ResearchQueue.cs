using System.Runtime.Serialization;
using Models.Base;
using Models.Tech;
using Models.Users;

namespace Models.Queues
{
    [DataContract(IsReference = true)]
    public class ResearchQueue : BaseQueueEntity
    {
        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public virtual Technology Technology { get; set; }

        [DataMember]
        public int? PlanetId { get; set; }

        [DataMember]
        public int? SatelliteId { get; set; }
    }
}