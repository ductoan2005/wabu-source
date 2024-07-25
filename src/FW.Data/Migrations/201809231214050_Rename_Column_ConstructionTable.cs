namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Column_ConstructionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_contruction", "ConstructionName", c => c.String());
            AddColumn("dbo.tbl_contruction", "ConstructionDescription", c => c.String());
            DropColumn("dbo.tbl_contruction", "ContructionName");
            DropColumn("dbo.tbl_contruction", "ContructionDescription");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_contruction", "ContructionDescription", c => c.String());
            AddColumn("dbo.tbl_contruction", "ContructionName", c => c.String());
            DropColumn("dbo.tbl_contruction", "ConstructionDescription");
            DropColumn("dbo.tbl_contruction", "ConstructionName");
        }
    }
}
