namespace TPModule6_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ArtMartiaux1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Samourais", "ArtMartial_Id", "dbo.ArtMartials");
            DropIndex("dbo.Samourais", new[] { "ArtMartial_Id" });
            AddColumn("dbo.ArtMartials", "Samourai_Id", c => c.Int());
            CreateIndex("dbo.ArtMartials", "Samourai_Id");
            AddForeignKey("dbo.ArtMartials", "Samourai_Id", "dbo.Samourais", "Id");
            DropColumn("dbo.Samourais", "ArtMartial_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Samourais", "ArtMartial_Id", c => c.Int());
            DropForeignKey("dbo.ArtMartials", "Samourai_Id", "dbo.Samourais");
            DropIndex("dbo.ArtMartials", new[] { "Samourai_Id" });
            DropColumn("dbo.ArtMartials", "Samourai_Id");
            CreateIndex("dbo.Samourais", "ArtMartial_Id");
            AddForeignKey("dbo.Samourais", "ArtMartial_Id", "dbo.ArtMartials", "Id");
        }
    }
}
