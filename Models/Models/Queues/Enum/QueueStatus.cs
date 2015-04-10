
using System.ComponentModel.DataAnnotations;
namespace Models.Queues.Enum
{
    public enum QueueStatus
    {
        [Display(Name = "InQueue", ResourceType = typeof(Resources))]
        InQueue,
        [Display(Name = "Done", ResourceType = typeof(Resources))]
        Done,
    }
}
