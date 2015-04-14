using Models.Base;
using Models.Universe.Enum;
using Models.Universe.Strcut;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Universe
{
    public class Star : BaseEntity, IStar
    {
        [Required]
        [EnumDataType(typeof(StarType))]
        [Display(Name = "Type", ResourceType = typeof(Resources))]
        public StarType StarType { get; set; }
        [Required]
        [Display(Name ="Color", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(StarColor))]
        public StarColor StarColor { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string Name { get; set; }
        [Required]
        [Display(Name = "SurfaceTemp", ResourceType = typeof(Resources))]
        public int SurfaceTemp { get; set; }
        [Required]
        [Display(Name = "Mass", ResourceType = typeof(Resources))]
        public double Mass { get; set; }
        [Required]
        [Display(Name = "Radius", ResourceType = typeof(Resources))]
        public double Radius { get; set; }
        [Required]
        [Range(1,10)]
        [Display(Name = "RadiationLevel", ResourceType = typeof(Resources))]
        public int RadiationLevel { get; set; }
        [Required]
        public Coordinates Coordinate { get; set; }

        public List<Satellite> Satellites { get; set; }
        public virtual Galaxy Universe { get; set; }
    }
}
