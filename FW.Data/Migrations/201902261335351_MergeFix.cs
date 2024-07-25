namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MergeFix : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.tbl_company", "TotalBiddedNews", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_company", "ProjectImplemented", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_company", "ProjectsComplete", c => c.Int(nullable: false));
            //DropColumn("dbo.tbl_company", "TotalNewsBidded");
            //DropColumn("dbo.tbl_company", "NumberNewsBidded");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.tbl_company", "NumberNewsBidded", c => c.Int(nullable: false));
            //AddColumn("dbo.tbl_company", "TotalNewsBidded", c => c.Int(nullable: false));
            //DropColumn("dbo.tbl_company", "ProjectsComplete");
            //DropColumn("dbo.tbl_company", "ProjectImplemented");
            //DropColumn("dbo.tbl_company", "TotalBiddedNews");
        }
    }
}
