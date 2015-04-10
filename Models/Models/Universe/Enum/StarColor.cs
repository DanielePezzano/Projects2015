
using System.ComponentModel.DataAnnotations;
namespace Models.Universe.Enum
{
    public enum StarColor
    {
        [Display(Name="Red", ResourceType= typeof(Resources))]
        Red,
        [Display(Name = "Yellow", ResourceType = typeof(Resources))]
        Yellow,
        [Display(Name = "Orange", ResourceType = typeof(Resources))]
        Orange,
        [Display(Name = "Blue", ResourceType = typeof(Resources))]
        Blue,
        [Display(Name = "White", ResourceType = typeof(Resources))]
        White
    }
}
