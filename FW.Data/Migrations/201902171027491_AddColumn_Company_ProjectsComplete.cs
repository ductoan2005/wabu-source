namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Company_ProjectsComplete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_company", "ProjectsComplete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_company", "ProjectsComplete");
        }
    }
}
