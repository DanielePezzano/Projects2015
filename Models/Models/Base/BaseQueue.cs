using Models.Base.Enum;
using Models.Queues.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Base
{
    [DataContract]
    public class BaseQueueEntity : BaseEntity
    {
        [Display(Name = "Status", ResourceType = typeof(Resources))]
        [DataMember]
        [EnumDataType(typeof(QueueStatus))]
        public QueueStatus Status { get; set; }
        [Required()]
        [Display(Name = "FinishAt", ResourceType = typeof(Resources))]
        [DataMember]
        public DateTime FinishAt { get; set; }
    }
}
