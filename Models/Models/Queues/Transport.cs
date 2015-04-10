
using System.ComponentModel.DataAnnotations;
namespace Models.Queues
{
    public struct Transport
    {
        [Display(Name = "Ore", ResourceType = typeof(Resources))]
        public int Ore { get; set; }
        [Display(Name = "Food", ResourceType = typeof(Resources))]
        public int Food { get; set; }
        [Display(Name = "Civils", ResourceType = typeof(Resources))]
        public int Civils { get; set; }
        [Display(Name = "Armies", ResourceType = typeof(Resources))]
        public int Armies { get; set; }
    }
}
