using System;

namespace BLL.Utilities
{
    public sealed class RangeConversion : IDisposable
    {
        private readonly double _minRangeTo;
        private readonly ScaleConversion _scaleConv;

        /// <summary>
        ///     this class will convert from range (a-b) to range (c-d)
        /// </summary>
        /// <param name="minTo"></param>
        /// <param name="maxTo"></param>
        /// <param name="scaleconverter"></param>
        public RangeConversion(double minTo, double maxTo,
            ScaleConversion scaleconverter)
        {
            _minRangeTo = minTo < maxTo ? minTo : maxTo;
            _scaleConv = scaleconverter;
        }

        public void Dispose()
        {
            //GC.SuppressFinalize(this);
        }

        public double DoConversion(double number)
        {
            var temp = _scaleConv.Convert(number);
            return Math.Truncate((temp + _minRangeTo)*100)/100;
        }
    }
}