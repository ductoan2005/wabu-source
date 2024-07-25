namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_BiddingDetail_NumberOfDaysImplement : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_detail", "NumberOfDaysImplement", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_detail", "NumberOfDaysImplement");
        }
    }
}
