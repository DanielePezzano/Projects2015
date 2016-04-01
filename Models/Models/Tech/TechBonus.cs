using Models.Base;
using Models.Tech.Enum;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Tech
{
    [DataContract(IsReference=true)]
    public class TechBonus : BaseEntity
    {
        [Required]
        [Display(Name = "BonusType", ResourceType = typeof(Resources))]
        [DataMember]
        [EnumDataType(typeof(BonusType))]
        public BonusType Bonus { get; set; }
        [Required]
        [Display(Name = "Value", ResourceType = typeof(Resources))]
        [DataMember]
        public int Value { get; set; }
        [DataMember]
        public virtual Technology Technology { get; set; }
    }
}