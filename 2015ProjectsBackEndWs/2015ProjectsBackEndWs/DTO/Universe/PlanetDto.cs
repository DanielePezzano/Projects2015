using _2015ProjectsBackEndWs.DataMapper;
using Models.Base.Enum;
using Models.Buildings;
using Models.Universe;
using Models.Universe.Enum;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace _2015ProjectsBackEndWs.DTO.Universe
{
    [DataContract]
    public class PlanetDto : BaseDto<Planet>
    {
        [DataMember]
        public int Id { get { return Model.Id; } }
        [DataMember]
        public string Name { get { return Model.Name; } set { Model.Name = value; } }
        [DataMember]
        public SatelliteStatus Status { get { return Model.SatelliteStatus; } set { Model.SatelliteStatus = value; } }
        [DataMember]
        public int RadiationLevel { get { return Model.RadiationLevel; } set { Model.RadiationLevel = value; } }
        [DataMember]
        public int GroundRadiatedSpaces { get { return Model.Spaces.GroundRadiatedSpaces; } set { Model.Spaces.GroundRadiatedSpaces = value; } }
        [DataMember]
        public int WaterRadiatedSpaces { get { return Model.Spaces.WaterRadiatedSpaces; } set { Model.Spaces.WaterRadiatedSpaces = value; } }
        [DataMember]
        public int GroundSpaces { get { return Model.Spaces.GroundSpaces; } set { Model.Spaces.GroundSpaces = value; } }
        [DataMember]
        public int WaterSpaces { get { return Model.Spaces.WaterSpaces; } set { Model.Spaces.WaterSpaces = value; } }
        [DataMember]
        public int Totalspaces { get { return Model.Spaces.Totalspaces; } }
        [DataMember]
        public int HabitableSpaces { get { return Model.Spaces.HabitableSpaces; } }
        [DataMember]
        public int WaterUsedSpaces { get { return Model.Spaces.WaterUsedSpaces; } set { Model.Spaces.WaterUsedSpaces = value; } }
        [DataMember]
        public int WaterSpacesLeft { get { return Model.Spaces.WaterSpacesLeft; } }
        [DataMember]
        public int GroundUsedSpaces { get { return Model.Spaces.GroudUsedSpaces; } set { Model.Spaces.GroudUsedSpaces = value; } }
        [DataMember]
        public int GroundSpacesLeft { get { return Model.Spaces.GroudSpacesLeft; } }
        [DataMember]
        public int MaxPopulation { get { return Model.MaxPopulation; } }
        [DataMember]
        public int Population { get { return Model.SatelliteSocial.Population; } set { Model.SatelliteSocial.Population = value; } }
        [DataMember]
        public TaxLevel TaxLevel { get { return Model.SatelliteSocial.TaxLevel; } set { Model.SatelliteSocial.TaxLevel = value; } }
        [DataMember]
        public double ActivePopOnFoodProduction { get { return Model.SatelliteProduction.ActivePopOnFoodProduction; } set { Model.SatelliteProduction.ActivePopOnFoodProduction = value; } }
        [DataMember]
        public double ActivePopOnOreProduction { get { return Model.SatelliteProduction.ActivePopOnOreProduction; } set { Model.SatelliteProduction.ActivePopOnOreProduction = value; } }
        [DataMember]
        public double ActivePopOnResProduction { get { return Model.SatelliteProduction.ActivePopOnResProduction; } set { Model.SatelliteProduction.ActivePopOnResProduction = value; } }
        [DataMember]
        public int FoodProduction { get { return Model.SatelliteProduction.FoodProduction; } set { Model.SatelliteProduction.FoodProduction = value; } }
        [DataMember]
        public int OreProduction { get { return Model.SatelliteProduction.OreProduction; } set { Model.SatelliteProduction.OreProduction = value; } }
        [DataMember]
        public int ResearchPointProduction { get { return Model.SatelliteProduction.ResearchPointProduction; } set { Model.SatelliteProduction.ResearchPointProduction = value; } }
        [DataMember]
        public bool AtmospherePresent { get { return Model.AtmospherePresent; } set { Model.AtmospherePresent = value; } }
        [DataMember]
        public bool RingsPresent { get { return Model.RingsPresent; } }
        [DataMember]
        public int SurfaceTemp { get { return Model.SurfaceTemp; } set { Model.SurfaceTemp = value; } }
        [DataMember]
        public double Mass { get { return Model.Mass; } }
        [DataMember]
        public double Radius { get { return Model.Radius; } }
        [DataMember]
        public double DistanceR { get { return Model.Orbit.DistanceR; } }
        [DataMember]
        public double Eccentricity { get { return Model.Orbit.Eccentricity; } }
        [DataMember]
        public double PeriodOfRevolution { get { return Model.Orbit.PeriodOfRevolution; } }
        [DataMember]
        public double PeriodOfRotation { get { return Model.Orbit.PeriodOfRotation; } }
        [DataMember]
        public double TetaZero { get { return Model.Orbit.TetaZero; } }
        [DataMember]
        public double GravityEarthCompared { get { return Mass; } } // gravità rispetto alla terra (che si decide abbia 100 spazi come paragone)
        [DataMember]
        public List<BuildingDto> Buildings { get; set; }

        public PlanetDto(Planet model)
            : base(model)
        {
            BuildingEntityMapper mapper = new BuildingEntityMapper();
            this.Buildings = mapper.EntityListToModel((List<Building>)model.Buildings);
        }
    }
}