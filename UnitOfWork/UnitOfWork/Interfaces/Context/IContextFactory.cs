

namespace UnitOfWork.Interfaces.Context
{
    public interface IContextFactory
    {
        IContext Retrieve();
    }
}
