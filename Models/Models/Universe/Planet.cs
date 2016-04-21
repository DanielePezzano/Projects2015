using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Base;
using Models.Users;

namespace Models.Universe
{
    [DataContract(IsReference = true)]
    public class Planet : BaseSatellite
    {
        [Display(Name = "Satellites", ResourceType = typeof (Resources))]
        [DataMember]
        public virtual ICollection<Satellite> Satellites { get; set; }

        [DataMember]
        public bool IsHomePlanet { get; set; }

        [DataMember]
        public virtual User User { get; set; }

        [DataMember]
        public virtual Star Star { get; set; }
    }
}