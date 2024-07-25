namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modified_MaxLength_Company_CompanyName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tbl_company", "CompanyName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tbl_company", "CompanyName", c => c.String(nullable: false, maxLength: 20));
        }
    }
}
