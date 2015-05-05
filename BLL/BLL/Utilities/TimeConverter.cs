using System;

namespace BLL.Utilities
{
    public static class TimeConverter
    {
        public static string FromDoubleToTime(double yearComparedToHearth)
        {
            var totalDays = yearComparedToHearth*365; //es 0.05 * 365 = 18.25
            return (Convert.ToInt32(totalDays)).ToString();
        }
    }
}