namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Notification_UserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_notification", "UserId", c => c.Long());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_notification", "UserId");
        }
    }
}
