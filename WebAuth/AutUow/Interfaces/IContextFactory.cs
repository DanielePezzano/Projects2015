using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutUow.Cache;
using AutUow.Implementations.UnitOfWork.Dto;

namespace AutUow.Interfaces
{
    public interface IContextFactory
    {
        IContext Retrieve();
        UowRepositories CreateRepositories();
        DalCache CreateCache();
    }
}
