using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Base.Interfaces
{
    public interface IBaseCost
    {
        int OreCost { get; set; }
        int MoneyCost { get; set; }
        int OreMaintenanceCost { get; set; }
        int MoneyMaintenanceCost { get; set; }
    }
}
