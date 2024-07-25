using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using FW.Data.Infrastructure.Interfaces;

namespace FW.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbFactory dbFactory;
        private FWDbContext dbContext;

        public UnitOfWork(IDbFactory dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public FWDbContext DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public DbExecutionResult Commit()
        {
            return DbContext.Commit();
        }

        public async Task<DbExecutionResult> CommitAsync() => await DbContext.CommitAsync();

        public void RollBack()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:

                        entry.State = EntityState.Detached;

                        break;

                    case EntityState.Modified:

                    case EntityState.Deleted:

                        entry.Reload();

                        break;

                    case EntityState.Detached:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
