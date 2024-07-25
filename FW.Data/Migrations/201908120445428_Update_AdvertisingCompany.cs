namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_AdvertisingCompany : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_company", "AdvertisingIsOn", c => c.Boolean(nullable: false));
            AddColumn("dbo.tbl_company", "AdvertisingBackgroundImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_company", "AdvertisingBackgroundImage");
            DropColumn("dbo.tbl_company", "AdvertisingIsOn");
        }
    }
}
