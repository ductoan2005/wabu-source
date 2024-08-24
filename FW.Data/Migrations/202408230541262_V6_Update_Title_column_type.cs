namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V6_Update_Title_column_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_post", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.tbl_post", "Username", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_post", "Username", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("dbo.tbl_post", "Title", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
