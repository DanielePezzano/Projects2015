﻿using Models.Base;
using Models.Tech.Enum;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Buildings
{
    [DataContract]
    public class BuildingSpec : BaseEntity
    {
        [Required()]
        [Display(Name = "BonusType", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(BonusType))]
        [DataMember]
        public BonusType Bonus { get; set; }
        [Required()]
        [Display(Name = "Value", ResourceType = typeof(Resources))]
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public virtual Building Building { get; set; }
    }
}