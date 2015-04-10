using Models.Base.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Base
{
    [ComplexType]
    public class SatelliteSocials
    {
        [Required]
        [Display(Name = "Population", ResourceType = typeof(Resources))]
        public int Population { get; set; }
        [Required]
        [Display(Name = "TaxLevel", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TaxLevel))]
        public TaxLevel TaxLevel { get; set; }
        
    }
}
