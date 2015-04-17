using Models.Base;
using Models.Tech;
using Models.Universe;
using Models.Users;

namespace Models.Queues
{
    public class ResearchQueue : BaseQueueEntity
    {
        public virtual User User { get; set; }
        public virtual Technology Technology { get; set; }
        public int? PlanetId { get; set; }
        public int? SatelliteId { get; set; }
    }
}
