using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Models.Queues;
using Models.Queues.Enum;

namespace Models.Base
{
    [DataContract(IsReference = true)]
    [KnownType(typeof (BuildingQueue))]
    [KnownType(typeof (FleetQueue))]
    [KnownType(typeof (ResearchQueue))]
    public class BaseQueueEntity : BaseEntity
    {
        [Display(Name = "Status", ResourceType = typeof (Resources))]
        [DataMember]
        [EnumDataType(typeof (QueueStatus))]
        public QueueStatus Status { get; set; }

        [Required]
        [Display(Name = "FinishAt", ResourceType = typeof (Resources))]
        [DataMember]
        public DateTime FinishAt { get; set; }
    }
}