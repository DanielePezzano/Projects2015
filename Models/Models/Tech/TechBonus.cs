using Models.Base;
using Models.Tech.Enum;
using System.ComponentModel.DataAnnotations;

namespace Models.Tech
{
    public class TechBonus:BaseEntity
    {
        [Required()]
        [Display(Name="BonusType", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(BonusType))]
        public BonusType  Bonus { get; set; }
        [Required()]
        [Display(Name = "Value", ResourceType = typeof(Resources))]
        public int Value { get; set; }

        public virtual Technology Technology { get; set; }
    }
}
