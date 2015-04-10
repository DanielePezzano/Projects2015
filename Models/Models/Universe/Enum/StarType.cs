using System.ComponentModel.DataAnnotations;
namespace Models.Universe.Enum
{
    public enum StarType
    {
        [Display(Name = "Dwarf", ResourceType=typeof(Resources))]
        Dwarf,
        [Display(Name = "Giant", ResourceType = typeof(Resources))]
        Giant,
        [Display(Name = "Hyper", ResourceType = typeof(Resources))]
        HyperGiant
    }
}
