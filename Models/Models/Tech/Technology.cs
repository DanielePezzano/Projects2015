using Models.Base;
using Models.Buildings;
using Models.Fleets.ShipClasses.Armors;
using Models.Fleets.ShipClasses.Engines;
using Models.Fleets.ShipClasses.Shields;
using Models.Fleets.ShipClasses.System;
using Models.Fleets.ShipClasses.Weapons;
using Models.Queues;
using Models.Tech.Enum;
using Models.Users;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Tech
{
    public class Technology : BaseEntity
    {
        private const int _OreWeigt = 23;
        private const int _MoneyWeight = 15;
        private const int _ResearchWeight = 10;

        [Required()]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }
        [Required()]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        public string  Description { get; set; }
        [Required()]
        [Display(Name = "Field", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TechnologyField))]
        public TechnologyField Field { get; set; }
        [Required()]
        [Display(Name = "SubField", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TechnologySubField))]
        public TechnologySubField SubField { get; set; }
        [Required()]
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        public int OreCost { get; set; }
        [Required()]
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        public int MoneyCost { get; set; }
        [Required()]
        [Display(Name = "ResearchPoints", ResourceType = typeof(Resources))]
        public int ResearchPoints { get; set; } //punti ricerca da investire
        [Display(Name = "TimeToComplete", ResourceType = typeof(Resources))]
        [NotMapped]
        public int TimeToComplete { get { return _OreWeigt * OreCost + _MoneyWeight * MoneyCost + _ResearchWeight * ResearchPoints; } } //tempo in secondi per terminarla

        public virtual ICollection<TechRequisiteNode> TechRequisites { get; set; }
        public virtual ICollection<TechBonus> TechBonuses { get; set; }
        public virtual ICollection<BuildingQueue> BuildingQueues { get; set; }
        public virtual ICollection<ResearchQueue> ResearchQueues { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Building> Buildings { get; set; }

        public virtual ICollection<Armor> Armors{get;set;}
        public virtual ICollection<Engine> Engines { get; set; }
        public virtual ICollection<Shield> Shields { get; set; }
        public virtual ICollection<ShipSystem> ShipSystems { get; set; }
        public virtual ICollection<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }
        public virtual ICollection<AntiShipWeapon> AntiShipWeapon { get; set; }

        public delegate int CalculateCostOre(ICollection<TechBonus> bonuses);
        public delegate int CalculateCostMoney(ICollection<TechBonus> bonuses);
        public delegate int CalculateResearchPoints(ICollection<TechBonus> bonuses, int costOre, int costMoney);
    }
}
