using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Base
{
    [ComplexType]
    public class Production
    {
        [Display(Name="FoodProduction", ResourceType=typeof(Resources))]
        public int FoodProduction { get; set; }
        [Display(Name="OreProduction",ResourceType=typeof(Resources))]
        public int OreProduction { get; set; }
        [Display(Name="ResearchProduction", ResourceType= typeof(Resources))]
        public int ResearchPointProduction { get; set; }

        [Display(Name="ActivePopOnFoodProduction",ResourceType= typeof(Resources))]
        public double ActivePopOnFoodProduction { get; set; }
        [Display(Name="ActivePopOnOreProduction", ResourceType= typeof(Resources))]
        public double ActivePopOnOreProduction { get; set; }
        [Display(Name="ActivePopOnResProduction", ResourceType=typeof(Resources))]
        public double ActivePopOnResProduction { get; set; }
    }
}
