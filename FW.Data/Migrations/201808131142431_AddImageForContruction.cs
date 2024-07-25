namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageForContruction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_contruction", "Image1", c => c.Binary());
            AddColumn("dbo.tbl_contruction", "Image2", c => c.Binary());
            AddColumn("dbo.tbl_contruction", "Image3", c => c.Binary());
            AddColumn("dbo.tbl_contruction", "ImageFileName1", c => c.String());
            AddColumn("dbo.tbl_contruction", "ImageFileName2", c => c.String());
            AddColumn("dbo.tbl_contruction", "ImageFileName3", c => c.String());
            DropColumn("dbo.tbl_contruction", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_contruction", "Image", c => c.Binary());
            DropColumn("dbo.tbl_contruction", "ImageFileName3");
            DropColumn("dbo.tbl_contruction", "ImageFileName2");
            DropColumn("dbo.tbl_contruction", "ImageFileName1");
            DropColumn("dbo.tbl_contruction", "Image3");
            DropColumn("dbo.tbl_contruction", "Image2");
            DropColumn("dbo.tbl_contruction", "Image1");
        }
    }
}
