using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Universe
{
    [ComplexType]
    [DataContract]
    public class OrbitDetail
    {
        [Range(0, 1)]
        [Required]
        [Display(Name = "Eccentricity", Description = "EccentricityHint", ResourceType = typeof(Resources))]
        [DataMember]
        public double Eccentricity { get; set; }
        [DataMember]
        public double TetaZero { get; set; }
        [Required]
        [Display(Name = "PeriodOfRevolution", ResourceType = typeof(Resources))]
        [DataMember]
        public double PeriodOfRevolution { get; set; }
        [Required]
        [Display(Name = "PeriodOfRotation", ResourceType = typeof(Resources))]
        [DataMember]
        public double PeriodOfRotation { get; set; }
        [Required]
        [DataMember]
        public double DistanceR { get; set; }
    }
}