namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveContractFormId_AddContractFormType_BiddingNews : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tbl_bidding_news", "ContractFormId", "dbo.tbl_contract_form");
            DropIndex("dbo.tbl_bidding_news", new[] { "ContractFormId" });
            AddColumn("dbo.tbl_bidding_news", "ContractFormType", c => c.Byte(nullable: false));
            DropColumn("dbo.tbl_bidding_news", "ContractFormId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_bidding_news", "ContractFormId", c => c.Long());
            DropColumn("dbo.tbl_bidding_news", "ContractFormType");
            CreateIndex("dbo.tbl_bidding_news", "ContractFormId");
            AddForeignKey("dbo.tbl_bidding_news", "ContractFormId", "dbo.tbl_contract_form", "Id");
        }
    }
}
