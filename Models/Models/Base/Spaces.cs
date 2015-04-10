using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Base
{
    [ComplexType]
    public class Spaces
    {
        [Required]
        [Display(Name = "RadiatedGround", Description = "RadiatedHint", ResourceType = typeof(Resources))]
        public int GroundRadiatedSpaces { get; set; }
        [Required]
        [Display(Name = "RadiatedWater", Description = "RadiatedHint", ResourceType = typeof(Resources))]
        public int WaterRadiatedSpaces { get; set; }
        [Required]
        [Display(Name = "GroundSpaces", Description = "GroundSpacesHint", ResourceType = typeof(Resources))]
        public int GroundSpaces { get; set; }
        [Required]
        [Display(Name = "WaterSpaces", Description = "WaterSpacesHint", ResourceType = typeof(Resources))]
        public int WaterSpaces { get; set; }
        [Display(Name = "TotalSpaces", Description = "TotalSpacesHint", ResourceType = typeof(Resources))]
        [NotMapped]
        public int Totalspaces { get { return GroundRadiatedSpaces + WaterRadiatedSpaces + GroundSpaces + WaterSpaces; } }
        [NotMapped]
        [Display(Name = "GravityCompared", Description = "GravityComparedHint", ResourceType = typeof(Resources))]
        public double GravityEarthCompared { get { return (Totalspaces == 0) ? 1 : (100 / Totalspaces); } } // gravità rispetto alla terra (che si decide abbia 100 spazi come paragone)
        [NotMapped]
        [Display(Name = "HabitableSpaces", Description = "HabitableSpacesHint", ResourceType = typeof(Resources))]
        public int HabitableSpaces { get { return WaterSpaces + GroundSpaces; } }
        [Required]
        [Display(Name="WaterUsedSpaces", ResourceType=typeof(Resources))]
        public int WaterUsedSpaces { get; set; }
        [NotMapped]
        [Display(Name = "UsableSpacesLeft", ResourceType = typeof(Resources))]
        public int WaterSpacesLeft { get { return (WaterSpaces - WaterUsedSpaces > 0) ? WaterSpaces - WaterUsedSpaces : 0; } }
        [Required]
        [Display(Name="GroundUsedSpaces", ResourceType= typeof(Resources))]
        public int GroudUsedSpaces { get; set; }
        [NotMapped]
        [Display(Name = "GroudSpacesLeft", ResourceType = typeof(Resources))]
        public int GroudSpacesLeft { get { return (GroundSpaces - GroudUsedSpaces > 0) ? GroundSpaces - GroudUsedSpaces : 0; } }
    }
}
