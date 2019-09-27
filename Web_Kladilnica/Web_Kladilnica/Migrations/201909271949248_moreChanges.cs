namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreChanges : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Team1ID", "dbo.Teams");
            DropForeignKey("dbo.Games", "Team2ID", "dbo.Teams");
            DropForeignKey("dbo.Games", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.Games", new[] { "Team1ID" });
            DropIndex("dbo.Games", new[] { "Team2ID" });
            DropIndex("dbo.Games", new[] { "Ticket_ID" });
            AddColumn("dbo.Tickets", "win", c => c.Boolean(nullable: false));
            DropColumn("dbo.Games", "Ticket_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Ticket_ID", c => c.Int());
            DropColumn("dbo.Tickets", "win");
            CreateIndex("dbo.Games", "Ticket_ID");
            CreateIndex("dbo.Games", "Team2ID");
            CreateIndex("dbo.Games", "Team1ID");
            AddForeignKey("dbo.Games", "Ticket_ID", "dbo.Tickets", "ID");
            AddForeignKey("dbo.Games", "Team2ID", "dbo.Teams", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Games", "Team1ID", "dbo.Teams", "ID", cascadeDelete: true);
        }
    }
}
