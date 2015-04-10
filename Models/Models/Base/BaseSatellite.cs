using Models.Universe.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Base
{
    public class BaseSatellite : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }
        [Required]
        [EnumDataType(typeof(SatelliteStatus))]
        [Display(Name = "SatelliteStatus", ResourceType = typeof(Resources))]
        SatelliteStatus SatelliteStatus { get; set; }
        [Range(0, 10)]
        [Display(Name ="RadiationLevel" , ResourceType = typeof(Resources))]
        public int RadiationLevel { get; set; }
        [Required]
        public Spaces Spaces { get; set; }        
        [NotMapped]
        [Display(Name = "MaxPopulation", ResourceType = typeof(Resources))]
        public int MaxPopulation { get { return Spaces.HabitableSpaces * 2; } }
        [Display(Name="SatelliteSocial", ResourceType= typeof(Resources))]
        public SatelliteSocials SatelliteSocial { get; set; }
        [Display(Name="SatelliteProduction", ResourceType= typeof(Resources))]
        public Production SatelliteProduction { get; set; }
        [Display(Name="AtmospherePresent", ResourceType= typeof(Resources))]
        public bool AtmospherePresent { get; set; }
        [Display(Name="RingsPresent", ResourceType=typeof(Resources))]
        public bool RingsPresent { get; set; }
        [Required]
        [Display(Name = "SurfaceTemp", ResourceType = typeof(Resources))]
        public int SurfaceTemp { get; set; }
        [Required]
        [Display(Name = "Mass", ResourceType = typeof(Resources))]
        public float Mass { get; set; }
        [Required]
        [Display(Name = "Radius", ResourceType = typeof(Resources))]
        public float Radius { get; set; }
    }
}
