using FW.Data.Infrastructure;
using FW.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

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
            PropertyComment(u => u.Id, "user ID");

            //UserName
            Property(u => u.Username).IsRequired().HasMaxLength(20)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                            new IndexAttribute("IX_users_1", 4) { IsUnique = true }
                        )
                );

            //Title
            Property(u => u.Title).IsRequired().HasMaxLength(20);

            //Content
            Property(u => u.Content);
        }
    }
}
