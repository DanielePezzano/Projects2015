using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Queues.Enum
{
    [DataContract]
    public enum QueueStatus
    {
        [Display(Name = "InQueue", ResourceType = typeof(Resources))]
        [EnumMember]
        InQueue,
        [Display(Name = "Done", ResourceType = typeof(Resources))]
        [EnumMember]
        Done
    }
}
