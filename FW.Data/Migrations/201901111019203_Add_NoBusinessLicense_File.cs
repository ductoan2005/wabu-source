namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_NoBusinessLicense_File : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_company", "NoBusinessLicensePath", c => c.String());
            AddColumn("dbo.tbl_company", "NoBusinessLicenseName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_company", "NoBusinessLicenseName");
            DropColumn("dbo.tbl_company", "NoBusinessLicensePath");
        }
    }
}
