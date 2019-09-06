namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class db1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        Time = c.Single(nullable: false),
                        HalfTime = c.Boolean(nullable: false),
                        Sport = c.String(),
                        Coefficient1 = c.Single(nullable: false),
                        Coefficient2 = c.Single(nullable: false),
                        Coefficient3 = c.Single(nullable: false),
                        League = c.String(),
                        selectedCoefficient = c.Int(nullable: false),
                        Team1_Name = c.String(),
                        Team1_score = c.Int(nullable: false),
                        Team1_BadgeUrl = c.String(),
                        Team2_Name = c.String(),
                        Team2_score = c.Int(nullable: false),
                        Team2_BadgeUrl = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        moneyInvested = c.Single(nullable: false),
                        totalCoefficient = c.Single(nullable: false),
                        WinMoney = c.Single(nullable: false),
                        win = c.Boolean(nullable: false),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tickets");
            DropTable("dbo.Games");
        }
    }
}
