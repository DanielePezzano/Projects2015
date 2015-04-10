using Models.Base;
using Models.Tech.Enum;
using System.ComponentModel.DataAnnotations;

namespace Models.Buildings
{
    public class BuildingSpec : BaseEntity
    {
        [Required()]
        [Display(Name = "BonusType", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(BonusType))]
        public BonusType Bonus { get; set; }
        [Required()]
        [Display(Name = "Value", ResourceType = typeof(Resources))]
        public int Value { get; set; }

        public virtual Building Building { get; set; }
    }
}
