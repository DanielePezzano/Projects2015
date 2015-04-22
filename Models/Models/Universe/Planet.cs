using Models.Users;
using Models.Base;
using Models.Universe.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Queues;
using Models.Buildings;
using System.Runtime.Serialization;
namespace Models.Universe
{
    [DataContract]
    public class Planet : BaseSatellite
    {
        [Display(Name = "Satellites", ResourceType = typeof(Resources))]
        [DataMember]
        public virtual ICollection<Satellite> Satellites { get; set; }
        [DataMember]
        public virtual User User { get; set; }
        [DataMember]
        public virtual Star Star { get; set; }
    }
}