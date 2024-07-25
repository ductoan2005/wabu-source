namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn_Construction_Basement_Byte_to_Int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_construction", "Basement", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_construction", "Basement", c => c.Byte(nullable: false));
        }
    }
}
