using UnitOfWork.Cache;
using UnitOfWork.Implementations.Uows.UowDto;
namespace UnitOfWork.Interfaces.Context
{
    public interface IContextFactory
    {
        IContext Retrieve();
        UowRepositories CreateRepositories();
        DalCache CreateCache();
    }
}
