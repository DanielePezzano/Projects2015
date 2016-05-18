using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SharedDto.Interfaces;
using SharedDto.Universe.Fleet;
using SharedDto.Universe.Mails;
using SharedDto.Universe.Planets;
using SharedDto.Universe.Queues;
using SharedDto.Universe.Race;
using SharedDto.Universe.Technology;

namespace SharedDto.Universe.User
{
    [DataContract]
    public class UserDto : IDto
    {
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int GalaxyId { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Photo { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public RaceDto Race { get; set; }
        [DataMember]
        public List<PlanetDto> Planets { get; set; }
        [DataMember]
        public List<PlanetDto> Satellites { get; set; }
        [DataMember]
        public List<TechnologyDto> Technologies { get; set; }
        [DataMember]
        public List<ResearchDto> Researches { get; set; }
        [DataMember]
        public List<FleetDto> Fleets { get; set; }
        [DataMember]
        public List<ShipClassDto> ShipClasses { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
    }
}