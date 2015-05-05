using System;

namespace BLL.Utilities
{
    public sealed class ScaleConversion : IDisposable
    {
        private readonly double _fromBase;
        private readonly double _toBase;

        /// <summary>
        /// This class will convert a double from a base (eg. 10) 
        /// to another base (eg. 100).
        /// e.g.
        /// number = 3, base from = 10, base to = 100 => result = 30
        /// </summary>
        /// <param name="fromBase"></param>
        /// <param name="toBase"></param>
        public ScaleConversion(double fromBase, double toBase)
        {
            _fromBase = fromBase;
            _toBase = toBase;
        }

        /// <summary>
        /// It convert the given number from a scale to another
        /// </summary>
        /// <param name="numberInFromBase"></param>
        /// <returns></returns>
        public double Convert(double numberInFromBase)
        {
            return (numberInFromBase * _toBase) / _fromBase;
        }

        public void Dispose()
        {
           // GC.SuppressFinalize(this);
        }
    }
}
