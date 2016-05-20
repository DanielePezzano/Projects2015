using System.Runtime.Serialization;

namespace SharedDto.UtilityDto
{
    [DataContract]
    public class SystemGenerationDto
    {
        
        [DataMember]
        public int MinX { get; set; }
        [DataMember]
        public int MaxX { get; set; }
        [DataMember]
        public int MinY { get; set; }
        [DataMember]
        public int MaxY { get; set; }
        [DataMember]
        public int GalaxyId { get; set; }

        #region Planet Info

        [DataMember]
        public bool ForceLiving { get; set; }

        [DataMember]
        public bool MineralRich { get; set; }

        [DataMember]
        public bool MineralPoor { get; set; }

        [DataMember]
        public bool FoodRich { get; set; }

        [DataMember]
        public bool FoodPoor { get; set; }

        [DataMember]
        public bool ForceWater { get; set; }

        [DataMember]
        public bool MostlyWater { get; set; }
        [DataMember]
        public bool IsHomePlanet { get; set; }
        #endregion

    }
}
