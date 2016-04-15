namespace BLL.Utilities.Structs
{
    public struct IntRange
    {
        public int Max;
        public int Min;

        public IntRange(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }

    public struct DoubleRange
    {
        public double Max;
        public double Min;

        public DoubleRange(double min, double max)
        {
            Min = min;
            Max = max;
        }
    }

    public static class UtilitiesFactory
    {
        public static IntRange RetrieveRange(int min, int max)
        {
            return new IntRange(min, max);
        }

        public static DoubleRange RetrieveDoubleRange(double min, double max)
        {
            return new DoubleRange(min,max);
        }
    }
}