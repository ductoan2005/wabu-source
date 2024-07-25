using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class WorkContentOtherConfiguration : CustomEntityTypeConfiguration<WorkContentOther>
    {
        public WorkContentOtherConfiguration():base()
        {
            ToTable("tbl_work_content_other");
            EntityTypeComment("table noi dung cong viec khac");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "noi dung cong viec ID");

            //BiddingPackageId
            Property(p => p.BiddingPackageId);
            PropertyComment(p => p.BiddingPackageId, "id goi thau");

            //WorkContentOtherName
            Property(p => p.WorkContentOtherName);
            PropertyComment(p => p.WorkContentOtherName, "ten noi dung congviec");
        }
    }
}
