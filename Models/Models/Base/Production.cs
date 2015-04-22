using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Models.Base
{
    [ComplexType]
    [DataContract]
    public class Production
    {
        [Display(Name = "FoodProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int FoodProduction { get; set; }
        [Display(Name = "OreProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int OreProduction { get; set; }
        [Display(Name = "ResearchProduction", ResourceType = typeof(Resources))]
        [DataMember]
        public int ResearchPointProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnFoodProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnFoodProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnOreProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnOreProduction { get; set; }
        [DataMember]
        [Display(Name = "ActivePopOnResProduction", ResourceType = typeof(Resources))]
        public double ActivePopOnResProduction { get; set; }
    }
}
