using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Interfaces.Repository;

namespace UnitOfWork.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        bool Save();
    }
}
