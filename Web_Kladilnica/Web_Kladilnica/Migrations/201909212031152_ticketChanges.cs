namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticketChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Ticket_ID", c => c.Int());
            AddColumn("dbo.Tickets", "totalCoefficient", c => c.Single(nullable: false));
            CreateIndex("dbo.Games", "Ticket_ID");
            AddForeignKey("dbo.Games", "Ticket_ID", "dbo.Tickets", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Ticket_ID", "dbo.Tickets");
            DropIndex("dbo.Games", new[] { "Ticket_ID" });
            DropColumn("dbo.Tickets", "totalCoefficient");
            DropColumn("dbo.Games", "Ticket_ID");
        }
    }
}
