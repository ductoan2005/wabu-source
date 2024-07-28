using FW.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace FW.Data.Infrastructure
{
    [DbConfigurationType(typeof(DbConfiguration))]
    public class FWDbContext : DbContext
    {
        public FWDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            DbExecutionResult = new DbExecutionResult();
            this.Database.CommandTimeout = 3600;//Thoi gian ket noi DB
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<LoginHistory> LoginHistories { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<BiddingNews> BiddingNews { get; set; }
        public DbSet<BiddingDetail> BiddingDetail { get; set; }
        public DbSet<BiddingNewsAbilityEquipment> BiddingNewsAbilityEquipments { get; set; }
        public DbSet<BiddingNewsAbilityHR> BiddingNewsAbilityHRs { get; set; }
        public DbSet<BiddingPackage> BiddingPackages { get; set; }
        public DbSet<ContractForm> ContractForms { get; set; }
        public DbSet<Construction> Contructions { get; set; }
        //public DbSet<FileRequest> FileRequests { get; set; }
        //public DbSet<FileRequestOther> FileRequestOthers { get; set; }
        public DbSet<WorkContent> WorkContents { get; set; }
        public DbSet<WorkContentOther> WorkContentOthers { get; set; }
        public DbSet<CompanyProfile> CompanyProfile { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Notification> Notification { get; set; }

        public DbSet<Post> Post { get; set; }

        public DbExecutionResult DbExecutionResult { get; set; }

        public virtual DbExecutionResult Commit()
        {
            DbExecutionResult.ResultType = EDbExecutionResult.None;
            DbExecutionResult.AffectedRows = -1;
            DbExecutionResult.AffectedRows = base.SaveChanges();
            return DbExecutionResult;
        }

        public virtual async Task<DbExecutionResult> CommitAsync()
        {
            DbExecutionResult.ResultType = EDbExecutionResult.None;
            DbExecutionResult.AffectedRows = -1;
            DbExecutionResult.AffectedRows = await SaveChangesAsync();
            return DbExecutionResult;
        }
    }
}
