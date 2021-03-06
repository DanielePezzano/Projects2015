﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    [ComplexType]
    [DataContract]
    public class Production
    {
        [Display(Name = "FoodProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int FoodProduction { get; set; }
        [Display(Name = "OreProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreProduction { get; set; }
        [Display(Name = "ResearchProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int ResearchPointProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnFoodProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnFoodProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnOreProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnOreProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnResProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnResProduction { get; set; }
        [DataMember]
        [Display(Name = "Food", ResourceType = typeof(Resources))]
        public int StoredFood { get; set; }
        [DataMember]
        [Display(Name = "Ore", ResourceType = typeof(Resources))]
        public int StoredOre { get; set; }
        [Display(Name = "ResearchPoints", ResourceType = typeof(Resources))]
        [DataMember]
        public int ResearchPoints { get; set; }

        [Display(Name = "TotalIncome", ResourceType = typeof(Resources))]
        [DataMember]
        public int TotalIncome { get; set; }

        [DataMember]
        public DateTime LastFoodUpDateTime { get; set; }
        [DataMember]
        public DateTime LastOreUpdateTime { get; set; }
        [DataMember]
        public DateTime LastResearchUpdateTime { get; set; }
        [DataMember]
        public DateTime LastMaintenanceUpdateTime { get; set; }
        [DataMember]
        public DateTime LastIncomeRevenueTime { get; set; }
    }
}
