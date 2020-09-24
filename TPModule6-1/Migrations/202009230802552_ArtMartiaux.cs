namespace TPModule6_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtMartiaux : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArtMartials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nom = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Samourais", "ArtMartial_Id", c => c.Int());
            CreateIndex("dbo.Samourais", "ArtMartial_Id");
            AddForeignKey("dbo.Samourais", "ArtMartial_Id", "dbo.ArtMartials", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Samourais", "ArtMartial_Id", "dbo.ArtMartials");
            DropIndex("dbo.Samourais", new[] { "ArtMartial_Id" });
            DropColumn("dbo.Samourais", "ArtMartial_Id");
            DropTable("dbo.ArtMartials");
        }
    }
}
