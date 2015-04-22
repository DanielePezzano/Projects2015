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
using System.Runtime.Serialization;

namespace Models.Tech
{
    [DataContract(IsReference=true)]
    public class Technology : BaseEntity
    {
        private const int _OreWeigt = 23;
        private const int _MoneyWeight = 15;
        private const int _ResearchWeight = 10;

        [Required()]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [DataMember]
        public string Name { get; set; }
        [Required()]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [DataMember]
        public string Description { get; set; }
        [Required()]
        [Display(Name = "Field", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TechnologyField))]
        [DataMember]
        public TechnologyField Field { get; set; }
        [Required()]
        [Display(Name = "SubField", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TechnologySubField))]
        [DataMember]
        public TechnologySubField SubField { get; set; }
        [Required()]
        [Display(Name = "OreCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreCost { get; set; }
        [Required()]
        [Display(Name = "MoneyCost", ResourceType = typeof(Resources))]
        [DataMember]
        public int MoneyCost { get; set; }
        [Required()]
        [Display(Name = "ResearchPoints", ResourceType = typeof(Resources))]
        [DataMember]
        public int ResearchPoints { get; set; } //punti ricerca da investire
        [Display(Name = "TimeToComplete", ResourceType = typeof(Resources))]
        [NotMapped]
        [DataMember]
        public int TimeToComplete { get { return _OreWeigt * OreCost + _MoneyWeight * MoneyCost + _ResearchWeight * ResearchPoints; } } //tempo in secondi per terminarla
        [DataMember]
        public virtual ICollection<TechRequisiteNode> TechRequisites { get; set; }
        [DataMember]
        public virtual ICollection<TechBonus> TechBonuses { get; set; }
        [DataMember]
        public virtual ICollection<BuildingQueue> BuildingQueues { get; set; }
        [DataMember]
        public virtual ICollection<ResearchQueue> ResearchQueues { get; set; }
        [DataMember]
        public virtual ICollection<User> Users { get; set; }
        [DataMember]
        public virtual ICollection<Building> Buildings { get; set; }
        [DataMember]
        public virtual ICollection<Armor> Armors { get; set; }
        [DataMember]
        public virtual ICollection<Engine> Engines { get; set; }
        [DataMember]
        public virtual ICollection<Shield> Shields { get; set; }
        [DataMember]
        public virtual ICollection<ShipSystem> ShipSystems { get; set; }
        [DataMember]
        public virtual ICollection<AntiPlanetWeapon> AntiPlanetWeapons { get; set; }
        [DataMember]
        public virtual ICollection<AntiShipWeapon> AntiShipWeapon { get; set; }

        public delegate int CalculateCostOre(ICollection<TechBonus> bonuses);
        public delegate int CalculateCostMoney(ICollection<TechBonus> bonuses);
        public delegate int CalculateResearchPoints(ICollection<TechBonus> bonuses, int costOre, int costMoney);
    }
}