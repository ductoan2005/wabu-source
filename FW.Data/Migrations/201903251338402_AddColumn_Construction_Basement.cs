namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Construction_Basement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_construction", "Basement", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_construction", "Basement");
        }
    }
}
