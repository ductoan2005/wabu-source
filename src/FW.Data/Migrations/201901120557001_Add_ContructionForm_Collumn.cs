namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_ContructionForm_Collumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_construction", "ContructionForm", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_construction", "ContructionForm");
        }
    }
}
