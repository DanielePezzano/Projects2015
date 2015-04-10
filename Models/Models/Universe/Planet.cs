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
        [Required]
        [Display(Name = "OrbitDetails", ResourceType = typeof(Resources))]
        public OrbitDetail Orbit { get; set; }

        [Display(Name = "Satellites", ResourceType = typeof(Resources))]
        public virtual ICollection<Satellite> Satellites { get; set; }
        [Display(Name = "BuildingQueues", ResourceType = typeof(Resources))]
        public virtual ICollection<BuildingQueue> BuildingQueues { get; set; }
        [Display(Name = "FleetQueues", ResourceType = typeof(Resources))]
        public virtual ICollection<FleetQueue> FleetQueues { get; set; }
        [Display(Name = "Researches", ResourceType = typeof(Resources))]
        public virtual ICollection<ResearchQueue> Researches { get; set; }
        [Display(Name = "Buildings", ResourceType = typeof(Resources))]
        public virtual ICollection<Building> Buildings { get; set; }
        public virtual User User { get; set; }
        public virtual Star Star { get; set; }
    }
}
