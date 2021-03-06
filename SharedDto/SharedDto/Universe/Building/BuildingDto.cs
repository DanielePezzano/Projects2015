﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Models.Buildings.Enums;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Building
{
    [DataContract]
    public class BuildingDto:IDto
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int PlanetId { get; set; }
        [DataMember]
        public BuildingType BuildingType { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int MoneyCost { get; set; }
        [DataMember]
        public int MoneyMaintenanceCost { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int OreCost { get; set; }
        [DataMember]
        public int OreMaintenanceCost { get; set; }
        [DataMember]
        public int SpaceNeeded { get; set; }
        [DataMember]
        public int UsedSpaces { get; set; }
        [DataMember]
        public List<BuildingSpecsDto> Details { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}
