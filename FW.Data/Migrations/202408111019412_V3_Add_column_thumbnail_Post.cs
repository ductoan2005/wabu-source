namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V3_Add_column_thumbnail_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_post", "ThumbnailImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_post", "ThumbnailImage");
        }
    }
}
