using System;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe.Technology
{
    [DataContract]
    public class TechnologyRequisiteDto : BaseDto, IDto
    {
        [DataMember]
        public string RequisiteType { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}