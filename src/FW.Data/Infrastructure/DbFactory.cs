using FW.Data.Infrastructure.Interfaces;

namespace FW.Data.Infrastructure
{
    public class DbFactory: Disposable, IDbFactory
    {
        FWDbContext dbContext;

        public DbFactory(FWDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public FWDbContext Init()
        {
            return dbContext;
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
