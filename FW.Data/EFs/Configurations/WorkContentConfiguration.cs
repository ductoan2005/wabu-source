using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public  class WorkContentConfiguration : CustomEntityTypeConfiguration<WorkContent>
    {
        public WorkContentConfiguration():base()
        {
            ToTable("tbl_work_content");
            EntityTypeComment("table noi dung cong viec");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "noi dung cong viec ID");

            //BiddingPackageId
            Property(p => p.BiddingPackageId);
            PropertyComment(p => p.BiddingPackageId, "id goi thau");

            //WorkContentName
            Property(p => p.WorkContentName);
            PropertyComment(p => p.WorkContentName, "ten noi dung congviec");
        }
    }
}
