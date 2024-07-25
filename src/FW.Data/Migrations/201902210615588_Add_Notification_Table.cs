namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Notification_Table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_notification",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 200, unicode: false),
                        Message = c.String(),
                        Type = c.String(maxLength: 200, unicode: false),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.tbl_company", "StringHtml", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_company", "StringHtml");
            DropTable("dbo.tbl_notification");
        }
    }
}
