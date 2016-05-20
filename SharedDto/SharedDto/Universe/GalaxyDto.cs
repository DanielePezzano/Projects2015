using System;
using System.Runtime.Serialization;
using SharedDto.BaseClasses;
using SharedDto.Interfaces;

namespace SharedDto.Universe
{
    [DataContract]
    public class GalaxyDto : BaseDto, IDto
    {

        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}