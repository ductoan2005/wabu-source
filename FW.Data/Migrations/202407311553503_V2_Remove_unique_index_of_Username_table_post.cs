namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V2_Remove_unique_index_of_Username_table_post : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.tbl_post", "IX_users_1");
        }
        
        public override void Down()
        {
            CreateIndex("dbo.tbl_post", "Username", unique: true, name: "IX_users_1");
        }
    }
}
