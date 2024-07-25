namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modify_Construction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_construction", "Image1FileName", c => c.String());
            AddColumn("dbo.tbl_construction", "Image2FileName", c => c.String());
            AddColumn("dbo.tbl_construction", "Image3FileName", c => c.String());
            AddColumn("dbo.tbl_construction", "Image1FilePath", c => c.String());
            AddColumn("dbo.tbl_construction", "Image2FilePath", c => c.String());
            AddColumn("dbo.tbl_construction", "Image3FilePath", c => c.String());
            DropColumn("dbo.tbl_construction", "Image1");
            DropColumn("dbo.tbl_construction", "Image2");
            DropColumn("dbo.tbl_construction", "Image3");
            DropColumn("dbo.tbl_construction", "ImageFileName1");
            DropColumn("dbo.tbl_construction", "ImageFileName2");
            DropColumn("dbo.tbl_construction", "ImageFileName3");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_construction", "ImageFileName3", c => c.String());
            AddColumn("dbo.tbl_construction", "ImageFileName2", c => c.String());
            AddColumn("dbo.tbl_construction", "ImageFileName1", c => c.String());
            AddColumn("dbo.tbl_construction", "Image3", c => c.Binary());
            AddColumn("dbo.tbl_construction", "Image2", c => c.Binary());
            AddColumn("dbo.tbl_construction", "Image1", c => c.Binary());
            DropColumn("dbo.tbl_construction", "Image3FilePath");
            DropColumn("dbo.tbl_construction", "Image2FilePath");
            DropColumn("dbo.tbl_construction", "Image1FilePath");
            DropColumn("dbo.tbl_construction", "Image3FileName");
            DropColumn("dbo.tbl_construction", "Image2FileName");
            DropColumn("dbo.tbl_construction", "Image1FileName");
        }
    }
}
