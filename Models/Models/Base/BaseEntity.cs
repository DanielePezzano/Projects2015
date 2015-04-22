using Models.Buildings;
using Models.Fleets.ShipClasses.Base;
using Models.Races;
using Models.Tech;
using Models.Universe;
using Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Models.Base
{
    [DataContract(IsReference=true)]
    [KnownType(typeof(BaseBuildingEntity))]
    [KnownType(typeof(BaseLogEntity))]
    [KnownType(typeof(BaseQueueEntity))]
    [KnownType(typeof(BaseSatellite))]
    [KnownType(typeof(BuildingSpec))]
    [KnownType(typeof(BaseShipEntity))]
    [KnownType(typeof(RaceBonus))]
    [KnownType(typeof(TechBonus))]
    [KnownType(typeof(Technology))]
    [KnownType(typeof(TechRequisiteNode))]
    [KnownType(typeof(Star))]
    [KnownType(typeof(Galaxy))]
    [KnownType(typeof(InternalMail))]
    [KnownType(typeof(User))]
    public class BaseEntity
    {
        [Required()]
        [DataMember]
        public int Id { get; set; }
        [Required()]
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [Required()]
        [DataMember]
        public DateTime UpdatedAt { get; set; }
    }
}
