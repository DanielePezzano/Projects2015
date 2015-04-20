using BLL.Utilities.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace _2015ProjectsBackEndWs.DTO.UtilityDto
{
    [DataContract]
    public class PlanetGenerationDto
    {
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
        public int MinX { get; set; }
        [DataMember]
        public int MaxX { get; set; }
        [DataMember]
        public int MinY { get; set; }
        [DataMember]
        public int MaxY { get; set; }
    }
}