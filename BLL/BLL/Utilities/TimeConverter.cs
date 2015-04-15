using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Utilities
{
    public static class TimeConverter
    {
        public static string FromDoubleToTime(double yearComparedToHearth)
        {
            double totalDays = yearComparedToHearth * 365; //es 0.05 * 365 = 18.25
            return (Convert.ToInt32(totalDays)).ToString();
        }
    }
}
