namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColumn_Construction_Basement_Int_to_IntNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_construction", "Basement", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_construction", "Basement", c => c.Int(nullable: false));
        }
    }
}
