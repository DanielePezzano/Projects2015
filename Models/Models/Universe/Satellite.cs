using Models.Base;
using Models.Universe.Enum;
using System.ComponentModel.DataAnnotations;
using Models.Users;
using System.Collections.Generic;
using Models.Buildings;

namespace Models.Universe
{
    public class Satellite : BaseSatellite
    {
        [Required]
        [Display(Name = "OrbitDetails", ResourceType = typeof(Resources))]
        public OrbitDetail Orbit { get; set; }

        [Display(Name = "Buildings", ResourceType = typeof(Resources))]
        public virtual ICollection<Building> Buildings { get; set; }

        public virtual Planet Planet { get; set; }
        public virtual User User { get; set; }
    }
}
