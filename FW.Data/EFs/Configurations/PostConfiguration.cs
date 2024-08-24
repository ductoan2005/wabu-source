using FW.Data.Infrastructure;
using FW.Models;

namespace FW.Data.EFs.Configurations
{
    public class PostConfiguration : CustomEntityTypeConfiguration<Post>
    {
        public PostConfiguration() : base()
        {
            ToTable("tbl_post");
            EntityTypeComment("table post");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "post ID");

            //UserName
            Property(u => u.Username).IsRequired();

            //Title
            Property(u => u.Title).IsRequired();

            //Content
            Property(u => u.Content).HasColumnType("ntext");

            //Thumbnail
            Property(u => u.ThumbnailImageFilePath);
        }
    }
}
