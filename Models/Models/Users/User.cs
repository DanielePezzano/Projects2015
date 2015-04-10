using Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Models.Universe;
using Models.Tech;
using Models.Queues;
using Models.Logs;
using Models.Universe;
using Models.Races;
using Models.Users.Enum;
using Models.Fleets.ShipClasses;
using Models.Fleets;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Users
{
    public class User : BaseEntity
    {
        private const int _TotalPoints = 10;

        [Required()]
        [StringLength(255, MinimumLength = 5)]
        [Display(Name = "Username", ResourceType = typeof(Resources))]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        [Required()]
        [Display(Name = "Email", ResourceType = typeof(Resources))]
        public string Email { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "UserImg", ResourceType = typeof(Resources))]
        public string Photo { get; set; }

        [Display(Name = "ScoreConstruction", ResourceType = typeof(Resources))]
        public int ScoreConstruction { get; set; }
        [Display(Name = "ScoreResearch", ResourceType = typeof(Resources))]
        public int ScoreResearch { get; set; }
        [Display(Name = "ScoreMilitary", ResourceType = typeof(Resources))]
        public int ScoreMilitary { get; set; }
        [Display(Name = "ScoreCultural", ResourceType = typeof(Resources))]
        public int ScoreCultural { get; set; }
        [Display(Name="UserStatus",ResourceType = typeof(Resources))]
        public UserStatus Status { get; set; }

        [Required()]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        public string RaceName { get; set; }
        [Range(0, _TotalPoints)]
        [Display(Name = "PointsUsed", ResourceType = typeof(Resources))]
        public int RacePointsUsed { get; set; }
        [NotMapped()]
        [Display(Name = "PointsLeft", ResourceType = typeof(Resources))]
        public int RacePointsLeft { get { return _TotalPoints - RacePointsUsed; } }

        [Display(Name = "Mails", ResourceType = typeof(Resources))]
        public virtual ICollection<InternalMail> Mails { get; set; }
        [Display(Name = "Planets", ResourceType = typeof(Resources))]
        public virtual ICollection<Planet> Planets { get; set; }
        [Display(Name = "Satellites", ResourceType = typeof(Resources))]
        public virtual ICollection<Satellite> Satellites { get; set; }
        [Display(Name = "Technologies", ResourceType = typeof(Resources))]
        public virtual ICollection<Technology> Technologies { get; set; }
        [Display(Name = "Researches", ResourceType = typeof(Resources))]
        public virtual ICollection<ResearchQueue> Researches { get; set; }
        public virtual ICollection<UserLog> Logs { get; set; }
        public virtual ICollection<ShipClass> SchipClasses { get; set; }
        public virtual ICollection<Fleet> Fleets { get; set; }
        public virtual ICollection<RaceBonus> RaceBonuses { get; set; }

        public virtual Galaxy Universe { get; set; }
    }
}
