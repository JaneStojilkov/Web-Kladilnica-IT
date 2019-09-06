namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Betting_Ticket",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Invested = c.Single(nullable: false),
                        SumCooef = c.Single(nullable: false),
                        SumWin = c.Single(nullable: false),
                        Won = c.Boolean(nullable: false),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Coefficient = c.Single(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Time = c.Single(nullable: false),
                        HalfTime = c.Boolean(nullable: false),
                        Team1 = c.String(),
                        Team2 = c.String(),
                        League = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Games");
            DropTable("dbo.Betting_Ticket");
        }
    }
}
