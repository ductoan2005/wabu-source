namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V5_Update_content_column_type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_post", "Content", c => c.String(storeType: "ntext"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_post", "Content", c => c.String());
        }
    }
}
