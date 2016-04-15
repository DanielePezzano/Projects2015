using System;

namespace BLL.Utilities.Structs
{
    public struct TimeDiff
    {
        public int Hours;
        public int Minutes;

        public TimeDiff(DateTime timeA, DateTime timeB)
        {
            var diff = timeB - timeA;
            Hours = diff.Hours;
            Minutes = diff.Minutes;
        }
    }
}