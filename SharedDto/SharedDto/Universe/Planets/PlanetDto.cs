﻿using Models.Base.Enum;
using Models.Universe.Enum;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SharedDto
{
    [DataContract]
    public class PlanetDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public SatelliteStatus Status { get; set; }
        [DataMember]
        public int RadiationLevel { get; set; }
        [DataMember]
        public int GroundRadiatedSpaces { get; set; }
        [DataMember]
        public int WaterRadiatedSpaces { get; set; }
        [DataMember]
        public int GroundSpaces { get; set; }
        [DataMember]
        public int WaterSpaces { get; set; }
        [DataMember]
        public int Totalspaces { get; set; }
        [DataMember]
        public int HabitableSpaces { get; set; }
        [DataMember]
        public int WaterUsedSpaces { get; set; }
        [DataMember]
        public int WaterSpacesLeft { get; set; }
        [DataMember]
        public int GroundUsedSpaces { get; set; }
        [DataMember]
        public int GroundSpacesLeft { get; set; }
        [DataMember]
        public int MaxPopulation { get; set; }
        [DataMember]
        public int Population { get; set; }
        [DataMember]
        public TaxLevel TaxLevel { get; set; }
        [DataMember]
        public double ActivePopOnFoodProduction { get; set; }
        [DataMember]
        public double ActivePopOnOreProduction { get; set; }
        [DataMember]
        public double ActivePopOnResProduction { get; set; }
        [DataMember]
        public int FoodProduction { get; set; }
        [DataMember]
        public int OreProduction { get; set; }
        [DataMember]
        public int ResearchPointProduction { get; set; }
        [DataMember]
        public bool AtmospherePresent { get; set; }
        [DataMember]
        public bool RingsPresent { get; set; }
        [DataMember]
        public int SurfaceTemp { get; set; }
        [DataMember]
        public double Mass { get; set; }
        [DataMember]
        public double Radius { get; set; }
        [DataMember]
        public double DistanceR { get; set; }
        [DataMember]
        public double Eccentricity { get; set; }
        [DataMember]
        public double PeriodOfRevolution { get; set; }
        [DataMember]
        public double PeriodOfRotation { get; set; }
        [DataMember]
        public double TetaZero { get; set; }
        [DataMember]
        public double GravityEarthCompared { get; set; } // gravità rispetto alla terra (che si decide abbia 100 spazi come paragone)
        [DataMember]
        public int? UserId { get; set; }
        [DataMember]
        public List<BuildingDto> Buildings { get; set; }
    }
}