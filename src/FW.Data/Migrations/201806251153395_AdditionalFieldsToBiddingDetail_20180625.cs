namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalFieldsToBiddingDetail_20180625 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_detail", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.tbl_bidding_detail", "DateInserted", c => c.DateTime());
            AddColumn("dbo.tbl_bidding_detail", "DateUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_detail", "DateUpdated");
            DropColumn("dbo.tbl_bidding_detail", "DateInserted");
            DropColumn("dbo.tbl_bidding_detail", "IsDeleted");
        }
    }
}
