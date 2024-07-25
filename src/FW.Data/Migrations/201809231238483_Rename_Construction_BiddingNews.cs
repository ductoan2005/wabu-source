namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename_Construction_BiddingNews : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_contruction", newName: "tbl_construction");
            RenameColumn(table: "dbo.tbl_bidding_news", name: "ContructionId", newName: "ConstructionId");
            RenameIndex(table: "dbo.tbl_bidding_news", name: "IX_ContructionId", newName: "IX_ConstructionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.tbl_bidding_news", name: "IX_ConstructionId", newName: "IX_ContructionId");
            RenameColumn(table: "dbo.tbl_bidding_news", name: "ConstructionId", newName: "ContructionId");
            RenameTable(name: "dbo.tbl_construction", newName: "tbl_contruction");
        }
    }
}
