namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ContructionForm_CollumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_construction", "ConstructionForm", c => c.Byte(nullable: false));
            DropColumn("dbo.tbl_construction", "ContructionForm");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_construction", "ContructionForm", c => c.Byte(nullable: false));
            DropColumn("dbo.tbl_construction", "ConstructionForm");
        }
    }
}
