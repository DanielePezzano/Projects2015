
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace Models.Queues
{
    [DataContract]
    public struct Transport
    {
        [Display(Name = "Ore", ResourceType = typeof(Resources))]
        [DataMember]
        public int Ore { get; set; }
        [Display(Name = "Food", ResourceType = typeof(Resources))]
        [DataMember]
        public int Food { get; set; }
        [Display(Name = "Civils", ResourceType = typeof(Resources))]
        [DataMember]
        public int Civils { get; set; }
        [Display(Name = "Armies", ResourceType = typeof(Resources))]
        [DataMember]
        public int Armies { get; set; }
    }
}