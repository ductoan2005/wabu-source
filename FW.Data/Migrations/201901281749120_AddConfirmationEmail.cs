namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConfirmationEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_users", "EmailConfirmed", c => c.Boolean(nullable: false, defaultValue: true));
            AddColumn("dbo.tbl_users", "EmailConfirmedToken", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_users", "EmailConfirmedToken");
            DropColumn("dbo.tbl_users", "EmailConfirmed");
        }
    }
}
