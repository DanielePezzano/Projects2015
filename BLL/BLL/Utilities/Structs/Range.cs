
namespace BLL.Utilities.Structs
{
    public struct IntRange
    {
        public int Min;
        public int Max;

        public IntRange(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }
    }

    public struct DoubleRange
    {
        public double Min;
        public double Max;

        public DoubleRange(double min, double max)
        {
            this.Min = min;
            this.Max = max;
        }
    }
}
