using System.Threading;
using System.Threading.Tasks;

namespace UnitOfWork.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkAsync
    {
        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}