namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserAvatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_users", "AvatarPath", c => c.String(maxLength: 250));
            AddColumn("dbo.tbl_users", "AvatarName", c => c.String(maxLength: 250));
            DropColumn("dbo.tbl_users", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_users", "Image", c => c.String(maxLength: 250));
            DropColumn("dbo.tbl_users", "AvatarName");
            DropColumn("dbo.tbl_users", "AvatarPath");
        }
    }
}
