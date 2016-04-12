namespace BLL.Utilities.Structs
{
    public struct Costs
    {
        public double OreCost { get; set; }
        public double MoneyCost { get; set; }
        public double FoodCost { get; set; }
        public double ResearchCost { get; set; }

        public Costs(double oreCost,double moneycost,double foodCost,double researchCost)
        {
            OreCost = oreCost;
            MoneyCost = moneycost;
            FoodCost = foodCost;
            ResearchCost = researchCost;
        }
    }
}