using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Models.Buildings;
using Models.Universe;
using Models.Universe.Enum;

namespace Models.Base
{
    [DataContract(IsReference = true)]
    [KnownType(typeof (Planet))]
    [KnownType(typeof (Satellite))]
    public class BaseSatellite : BaseEntity
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Name", ResourceType = typeof (Resources))]
        [DataMember]
        public string Name { get; set; }

        [Required]
        [EnumDataType(typeof (SatelliteStatus))]
        [Display(Name = "SatelliteStatus", ResourceType = typeof (Resources))]
        [DataMember]
        public SatelliteStatus SatelliteStatus { get; set; }

        [Range(0, 10)]
        [Display(Name = "RadiationLevel", ResourceType = typeof (Resources))]
        [DataMember]
        public int RadiationLevel { get; set; }

        [Required]
        [DataMember]
        public Spaces Spaces { get; set; }

        [NotMapped]
        [Display(Name = "MaxPopulation", ResourceType = typeof (Resources))]
        [DataMember]
        public int MaxPopulation
        {
            get { return (Spaces != null) ? Spaces.HabitableSpaces*2 : 0; }
        }

        [Display(Name = "SatelliteSocial", ResourceType = typeof (Resources))]
        [DataMember]
        public SatelliteSocials SatelliteSocial { get; set; }

        [Display(Name = "SatelliteProduction", ResourceType = typeof (Resources))]
        [DataMember]
        public Production SatelliteProduction { get; set; }

        [Display(Name = "AtmospherePresent", ResourceType = typeof (Resources))]
        [DataMember]
        public bool AtmospherePresent { get; set; }

        [Display(Name = "RingsPresent", ResourceType = typeof (Resources))]
        [DataMember]
        public bool RingsPresent { get; set; }

        [Required]
        [Display(Name = "SurfaceTemp", ResourceType = typeof (Resources))]
        [DataMember]
        public int SurfaceTemp { get; set; }

        [Required]
        [Display(Name = "Mass", ResourceType = typeof (Resources))]
        [DataMember]
        public double Mass { get; set; }

        [Required]
        [Display(Name = "Radius", ResourceType = typeof (Resources))]
        [DataMember]
        public double Radius { get; set; }

        [Required]
        [Display(Name = "OrbitDetails", ResourceType = typeof (Resources))]
        [DataMember]
        public OrbitDetail Orbit { get; set; }

        [NotMapped]
        [Display(Name = "GravityCompared", Description = "GravityComparedHint", ResourceType = typeof (Resources))]
        [DataMember]
        public double GravityEarthCompared
        {
            get { return Mass; }
        } // gravità rispetto alla terra (che si decide abbia 100 spazi come paragone)

        [DataMember]
        [Display(Name = "Buildings", ResourceType = typeof (Resources))]
        public virtual ICollection<Building> Buildings { get; set; }
    }
}