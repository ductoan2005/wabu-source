namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify__Columnname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_users", "EmailConfirmToken", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.tbl_users", "EmailConfirmedToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_users", "EmailConfirmedToken", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.tbl_users", "EmailConfirmToken");
        }
    }
}
