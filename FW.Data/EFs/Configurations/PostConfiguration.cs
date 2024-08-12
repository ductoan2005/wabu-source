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
            Property(u => u.Username).IsRequired().HasMaxLength(20);

            //Title
            Property(u => u.Title).IsRequired().HasMaxLength(20);

            //Content
            Property(u => u.Content);

            //Thumbnail
            Property(u => u.ThumbnailImageFilePath);
        }
    }
}
