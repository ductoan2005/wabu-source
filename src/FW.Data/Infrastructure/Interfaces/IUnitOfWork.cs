using System.Threading.Tasks;

namespace FW.Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        DbExecutionResult Commit();

        Task<DbExecutionResult> CommitAsync();

        void RollBack();
    }
}
