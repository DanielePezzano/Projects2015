using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Universe.Enum
{
    [DataContract]
    public enum StarColor
    {
        [EnumMember]
        [Display(Name="Red", ResourceType= typeof(Resources))]
        Red,
        [EnumMember]
        [Display(Name = "Yellow", ResourceType = typeof(Resources))]
        Yellow,
        [EnumMember]
        [Display(Name = "Orange", ResourceType = typeof(Resources))]
        Orange,
        [EnumMember]
        [Display(Name = "Blue", ResourceType = typeof(Resources))]
        Blue,
        [EnumMember]
        [Display(Name = "White", ResourceType = typeof(Resources))]
        White
    }
}
