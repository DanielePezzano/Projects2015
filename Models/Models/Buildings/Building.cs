using Models.Base;
using Models.Tech;
using Models.Universe;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Models.Buildings
{
    [DataContract]
    public class Building : BaseBuildingEntity
    {
        [DataMember]
        public virtual Technology Technology { get; set; }
        [DataMember]
        public virtual Planet Planet { get; set; }
        [DataMember]
        public virtual Satellite Satellite { get; set; }
        [DataMember]
        public virtual ICollection<BuildingSpec> BuildingSpecs { get; set; }
    }
}
