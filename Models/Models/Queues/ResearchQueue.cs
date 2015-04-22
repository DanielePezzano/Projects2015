using Models.Base;
using Models.Tech;
using Models.Universe;
using Models.Users;
using System.Runtime.Serialization;

namespace Models.Queues
{
    [DataContract]
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