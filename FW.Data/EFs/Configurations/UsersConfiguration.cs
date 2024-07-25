using FW.Data.Infrastructure;
using FW.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;

namespace FW.Data.EFs.Configurations
{
    public class UsersConfiguration : CustomEntityTypeConfiguration<Users>
    {
        public UsersConfiguration() : base()
        {
            ToTable("tbl_users");
            EntityTypeComment("table user");

            //Id
            HasKey(u => u.Id);
            PropertyComment(u => u.Id, "user ID");

            //UserName
            Property(u => u.UserName).IsRequired().HasMaxLength(20)
                .HasColumnAnnotation(
                    "Index",
                    new IndexAnnotation(
                            new IndexAttribute("IX_users_1", 4) { IsUnique = true }
                        )
                );

            //Password
            Property(u => u.Password).IsRequired().HasMaxLength(20);

            //PasswordChangedDate
            Property(u => u.PasswordChangedDate);

            // DateOfBirth
            Property(u => u.DateOfBirth);

            //Authority
            Property(u => u.Authority).IsRequired();
            PropertyComment(u => u.Authority, "0:root; 1:Administrator; 2: chủ đầu tư, 3: Nhà thầu");

            //FullName
            Property(u => u.FullName).HasMaxLength(50);

            // Giới tính
            Property(u => u.Gender);

            //CMND
            Property(u => u.CMND).HasMaxLength(9);

            //PhoneNumber
            Property(u => u.PhoneNumber).HasMaxLength(11);

            //Address
            Property(u => u.Address).HasMaxLength(250);

            //Email
            Property(u => u.Email).HasMaxLength(50);

            //AvartaName
            Property(u => u.AvatarName).HasMaxLength(250);

            //AvatarPath
            Property(u => u.AvatarPath).HasMaxLength(250);

            //IsActive
            Property(u => u.IsActive);

            //IsAgreeTerm
            Property(u => u.IsAgreeTerm);

            //IsPerson
            Property(u => u.IsPerson);

            //EmailConfirmed
            Property(u => u.EmailConfirmed);

            //EmailConfirmedToken
            Property(u => u.EmailConfirmToken)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50);

            //Collection BiddingDetails
            HasMany(fr => fr.Constructions)
               .WithRequired(u => u.User)
               .HasForeignKey(u => u.UserId)
               .WillCascadeOnDelete(false);
        }
    }
}
