using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Universe
{
    [ComplexType]
    public sealed class OrbitDetail
    {
        [Range(0,1)]
        [Required]
        [Display(Name = "Eccentricity", Description = "EccentricityHint", ResourceType = typeof(Resources))]
        public double Eccentricity { get; set; }
        public double TetaZero { get; set; }
        [Required]
        [Display(Name = "PeriodOfRevolution", ResourceType = typeof(Resources))]
        public int PeriodOfRevolution { get; set; }
        [Required]
        [Display(Name = "PeriodOfRotation", ResourceType = typeof(Resources))]
        public int PeriodOfRotation { get; set; }

        public int DistanceR { get; set; }
    }
}
