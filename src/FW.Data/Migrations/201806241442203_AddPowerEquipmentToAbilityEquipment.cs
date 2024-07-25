namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPowerEquipmentToAbilityEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_bidding_news_ability_equipment", "PowerEquipment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_bidding_news_ability_equipment", "PowerEquipment");
        }
    }
}
