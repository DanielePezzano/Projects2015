﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using BaseModels;
using Models.Fleets;
using Models.Fleets.ShipClasses;
using Models.Queues;
using Models.Races;
using Models.Tech;
using Models.Universe;
using Models.Users.Enum;

namespace Models.Users
{
    [DataContract(IsReference = true)]
    public class User : BaseEntity
    {
        private const int TotalPoints = 10;

        [Required]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Username", ResourceType = typeof (Resources))]
        [DataMember]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        [Required]
        [Display(Name = "Email", ResourceType = typeof (Resources))]
        [DataMember]
        public string Email { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "UserImg", ResourceType = typeof (Resources))]
        [DataMember]
        public string Photo { get; set; }
        
        [Display(Name = "UserStatus", ResourceType = typeof (Resources))]
        [DataMember]
        [EnumDataType(typeof (UserStatus))]
        public UserStatus Status { get; set; }

        [Required]
        [DataMember]
        [Display(Name = "Name", ResourceType = typeof (Resources))]
        public string RaceName { get; set; }

        [Range(0, TotalPoints)]
        [Display(Name = "PointsUsed", ResourceType = typeof (Resources))]
        [DataMember]
        public int RacePointsUsed { get; set; }

        [NotMapped]
        [Display(Name = "PointsLeft", ResourceType = typeof (Resources))]
        [DataMember]
        public int RacePointsLeft => TotalPoints - RacePointsUsed;

        [Display(Name = "Planets", ResourceType = typeof (Resources))]
        [DataMember]
        public virtual ICollection<Planet> Planets { get; set; }

        [Display(Name = "Satellites", ResourceType = typeof (Resources))]
        [DataMember]
        public virtual ICollection<Satellite> Satellites { get; set; }

        [Display(Name = "Technologies", ResourceType = typeof (Resources))]
        [DataMember]
        public virtual ICollection<Technology> Technologies { get; set; }

        [Display(Name = "Researches", ResourceType = typeof (Resources))]
        [DataMember]
        public virtual ICollection<ResearchQueue> Researches { get; set; }

        [DataMember]
        public virtual ICollection<ShipClass> ShipClasses { get; set; }

        [DataMember]
        public virtual ICollection<Fleet> Fleets { get; set; }

        [DataMember]
        public virtual ICollection<RaceBonus> RaceBonuses { get; set; }
        
    }
}