using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    [ComplexType]
    [DataContract]
    public class Spaces
    {
        [Required]
        [Display(Name = "RadiatedGround", Description = "RadiatedHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int GroundRadiatedSpaces { get; set; }
        [Required]
        [Display(Name = "RadiatedWater", Description = "RadiatedHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int WaterRadiatedSpaces { get; set; }
        [Required]
        [Display(Name = "GroundSpaces", Description = "GroundSpacesHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int GroundSpaces { get; set; }
        [Required]
        [Display(Name = "WaterSpaces", Description = "WaterSpacesHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int WaterSpaces { get; set; }
        [Display(Name = "TotalSpaces", Description = "TotalSpacesHint", ResourceType = typeof(Resources))]
        [NotMapped]
        [DataMember]
        public int Totalspaces { get { return GroundRadiatedSpaces + WaterRadiatedSpaces + GroundSpaces + WaterSpaces; } }
        [NotMapped]
        [Display(Name = "HabitableSpaces", Description = "HabitableSpacesHint", ResourceType = typeof(Resources))]
        [DataMember]
        public int HabitableSpaces { get { return WaterSpaces + GroundSpaces; } }
        [Required]
        [Display(Name = "WaterUsedSpaces", ResourceType = typeof(Resources))]
        [DataMember]
        public int WaterUsedSpaces { get; set; }
        [NotMapped]
        [Display(Name = "UsableSpacesLeft", ResourceType = typeof(Resources))]
        [DataMember]
        public int WaterSpacesLeft { get { return (WaterSpaces - WaterUsedSpaces > 0) ? WaterSpaces - WaterUsedSpaces : 0; } }
        [Required]
        [Display(Name = "GroundUsedSpaces", ResourceType = typeof(Resources))]
        [DataMember]
        public int GroudUsedSpaces { get; set; }
        [NotMapped]
        [Display(Name = "GroudSpacesLeft", ResourceType = typeof(Resources))]
        [DataMember]
        public int GroudSpacesLeft { get { return (GroundSpaces - GroudUsedSpaces > 0) ? GroundSpaces - GroudUsedSpaces : 0; } }
    }
}
