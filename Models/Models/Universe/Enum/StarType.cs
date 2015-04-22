using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Universe.Enum
{
    [DataContract]
    public enum StarType
    {
        [EnumMember]
        [Display(Name = "Dwarf", ResourceType=typeof(Resources))]
        Dwarf,
        [EnumMember]
        [Display(Name = "Giant", ResourceType = typeof(Resources))]
        Giant,
        [EnumMember]
        [Display(Name = "Hyper", ResourceType = typeof(Resources))]
        HyperGiant
    }
}
