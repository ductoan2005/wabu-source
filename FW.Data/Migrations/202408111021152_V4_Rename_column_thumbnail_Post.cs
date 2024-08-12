namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V4_Rename_column_thumbnail_Post : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_post", "ThumbnailImageFilePath", c => c.String());
            DropColumn("dbo.tbl_post", "ThumbnailImage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_post", "ThumbnailImage", c => c.String());
            DropColumn("dbo.tbl_post", "ThumbnailImageFilePath");
        }
    }
}
