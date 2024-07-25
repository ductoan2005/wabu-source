using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class NotificationConfiguration : CustomEntityTypeConfiguration<Notification>
    {
        public NotificationConfiguration() : base()
        {
            ToTable("tbl_notification");
            EntityTypeComment("table notification");

            //Id
            HasKey(c => c.Id);
            PropertyComment(c => c.Id, "ID company de quan ly DB");

            //UserId
            Property(s => s.UserId);

            //Title
            Property(s => s.Title).HasMaxLength(200).HasColumnType("varchar");

            //Type
            Property(s => s.Type).HasMaxLength(200).HasColumnType("varchar");

            //Message
            Property(s => s.Message);
        }
    }
}
