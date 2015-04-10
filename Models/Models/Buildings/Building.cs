using Models.Base;
using Models.Tech;
using Models.Universe;
using System.Collections.Generic;

namespace Models.Buildings
{
    public class Building : BaseBuildingEntity
    {
        public virtual Technology Technology { get; set; }
        public virtual Planet Planet { get; set; }
        public virtual Satellite Satellite { get; set; }
        public virtual ICollection<BuildingSpec> BuildingSpecs { get; set; }
    }
}
