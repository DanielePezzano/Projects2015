using Models.Users;
using Models.Base;
using Models.Universe.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Models.Queues;
using Models.Buildings;
namespace Models.Universe
{
    public class Planet : BaseSatellite
    {
        [Display(Name = "Satellites", ResourceType = typeof(Resources))]
        public virtual ICollection<Satellite> Satellites { get; set; }
       
        public virtual User User { get; set; }
        public virtual Star Star { get; set; }
    }
}
