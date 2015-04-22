using Models.Base;
using Models.Universe.Enum;
using Models.Universe.Strcut;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Universe
{
    [DataContract(IsReference=true)]
    public class Star : BaseEntity
    {
        [Required]
        [EnumDataType(typeof(StarType))]
        [Display(Name = "Type", ResourceType = typeof(Resources))]
        [DataMember]
        public StarType StarType { get; set; }
        [Required]
        [Display(Name = "Color", ResourceType = typeof(Resources))]
        [EnumDataType(typeof(StarColor))]
        [DataMember]
        public StarColor StarColor { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [DataMember]
        public string Name { get; set; }
        [Required]
        [Display(Name = "SurfaceTemp", ResourceType = typeof(Resources))]
        [DataMember]
        public int SurfaceTemp { get; set; }
        [Required]
        [Display(Name = "Mass", ResourceType = typeof(Resources))]
        [DataMember]
        public double Mass { get; set; }
        [Required]
        [Display(Name = "Radius", ResourceType = typeof(Resources))]
        [DataMember]
        public double Radius { get; set; }
        [Required]
        [Range(1, 10)]
        [Display(Name = "RadiationLevel", ResourceType = typeof(Resources))]
        [DataMember]
        public int RadiationLevel { get; set; }
        [Required()]
        [DataMember]
        public int CoordinateX { get; set; }
        [Required()]
        [DataMember]
        public int CoordinateY { get; set; }
        [DataMember]
        public virtual ICollection<Planet> Planets { get; set; }
        [DataMember]
        public virtual Galaxy Galaxy { get; set; }
    }
}