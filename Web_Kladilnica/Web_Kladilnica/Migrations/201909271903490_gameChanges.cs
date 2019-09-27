namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gameChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Team1_ID", "dbo.Teams");
            DropForeignKey("dbo.Games", "Team2_ID", "dbo.Teams");
            DropIndex("dbo.Games", new[] { "Team1_ID" });
            DropIndex("dbo.Games", new[] { "Team2_ID" });
            RenameColumn(table: "dbo.Games", name: "Team1_ID", newName: "Team1ID");
            RenameColumn(table: "dbo.Games", name: "Team2_ID", newName: "Team2ID");
            AlterColumn("dbo.Games", "Team1ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Games", "Team2ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Games", "Team1ID");
            CreateIndex("dbo.Games", "Team2ID");
            AddForeignKey("dbo.Games", "Team1ID", "dbo.Teams", "ID", cascadeDelete: false);
            AddForeignKey("dbo.Games", "Team2ID", "dbo.Teams", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Team2ID", "dbo.Teams");
            DropForeignKey("dbo.Games", "Team1ID", "dbo.Teams");
            DropIndex("dbo.Games", new[] { "Team2ID" });
            DropIndex("dbo.Games", new[] { "Team1ID" });
            AlterColumn("dbo.Games", "Team2ID", c => c.Int());
            AlterColumn("dbo.Games", "Team1ID", c => c.Int());
            RenameColumn(table: "dbo.Games", name: "Team2ID", newName: "Team2_ID");
            RenameColumn(table: "dbo.Games", name: "Team1ID", newName: "Team1_ID");
            CreateIndex("dbo.Games", "Team2_ID");
            CreateIndex("dbo.Games", "Team1_ID");
            AddForeignKey("dbo.Games", "Team2_ID", "dbo.Teams", "ID");
            AddForeignKey("dbo.Games", "Team1_ID", "dbo.Teams", "ID");
        }
    }
}
