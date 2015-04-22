using Models.Base.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    [ComplexType]
    [DataContract]
    public class SatelliteSocials
    {
        [Required]
        [Display(Name = "Population", ResourceType = typeof(Resources))]
        [DataMember]
        public int Population { get; set; }
        [Required]
        [Display(Name = "TaxLevel", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(TaxLevel))]
        [DataMember]
        public TaxLevel TaxLevel { get; set; }
        
    }
}
