namespace FW.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V1_Add_table_post : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 20),
                        Username = c.String(nullable: false, maxLength: 20),
                        Content = c.String(),
                        IsDeleted = c.Boolean(),
                        DateInserted = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Username, unique: true, name: "IX_users_1");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.tbl_post", "IX_users_1");
            DropTable("dbo.tbl_post");
        }
    }
}
