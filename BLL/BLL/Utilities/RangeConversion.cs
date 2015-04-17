using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public sealed class RangeConversion : IDisposable
    {
        private double _MinRangeFrom;
        private double _MaxRangeFrom;
        private double _MinRangeTo;
        private double _MaxRangeTo;
        private ScaleConversion _ScaleConv = null;

        /// <summary>
        /// this class will convert from range (a-b) to range (c-d)
        /// </summary>
        /// <param name="minFrom"></param>
        /// <param name="maxFrom"></param>
        /// <param name="minTo"></param>
        /// <param name="maxTo"></param>
        public RangeConversion(double minFrom, double maxFrom, double minTo, double maxTo, ScaleConversion scaleconverter)
        {
            this._MinRangeFrom = (minFrom < maxFrom) ? minFrom : maxFrom;
            this._MaxRangeFrom = (minFrom < maxFrom) ? maxFrom : minFrom;
            this._MinRangeTo = (minTo < maxTo) ? minTo : maxTo;
            this._MaxRangeTo = (minTo < maxTo) ? maxTo : minTo;
            this._ScaleConv = scaleconverter;
        }

        public double DoConversion(double number)
        {
            double temp = this._ScaleConv.Convert(number);
            return Math.Truncate((temp + this._MinRangeTo) * 100) / 100;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
