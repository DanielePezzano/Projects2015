using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Information
{
    public struct DropDownInfo
    {
        public string ItemName;
        public int ItemId;

        public DropDownInfo(int id,string name)
        {
            this.ItemName = name;
            this.ItemId = id;
        }
    }
}
