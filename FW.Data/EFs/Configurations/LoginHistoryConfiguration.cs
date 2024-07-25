using FW.Data.Infrastructure;
using FW.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace FW.Data.EFs.Configurations
{
    public class LoginHistoryConfiguration : CustomEntityTypeConfiguration<LoginHistory>
    {
        public LoginHistoryConfiguration() : base()
        {
            ToTable("tbl_login_history");
            EntityTypeComment("table lịch sử đăng nhập");

            HasKey(p => p.Id);

            Property(p => p.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            PropertyComment(p => p.Id, "user ID");

            PropertyComment(p => p.LoginFailedTimes, "số lần đăng nhập thất bại");

            PropertyComment(p => p.LastLoginTime, "thời gian đăng nhập mới nhất");

            PropertyComment(p => p.FirstLoginFailedTime, "thời gian đầu tiên đăng nhập thất bại");
        }

    }
}
