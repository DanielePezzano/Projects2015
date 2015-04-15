using Models.Base.Enum;
using Models.Queues.Enum;
using System;
using System.ComponentModel.DataAnnotations;
namespace Models.Base
{
    public class BaseQueueEntity : BaseEntity
    {
        [Display(Name = "Status", ResourceType = typeof(Resources))]
        public QueueStatus Status { get; set; }
        [Required()]
        [Display(Name = "FinishAt", ResourceType = typeof(Resources))]
        public DateTime FinishAt { get; set; }
    }
}
