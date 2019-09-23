namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doubleStuff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "Coefficient1", c => c.Double(nullable: false));
            AlterColumn("dbo.Games", "Coefficient2", c => c.Double(nullable: false));
            AlterColumn("dbo.Games", "Coefficient3", c => c.Double(nullable: false));
            AlterColumn("dbo.Tickets", "moneyInvested", c => c.Int(nullable: false));
            AlterColumn("dbo.Tickets", "totalCoefficient", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "totalCoefficient", c => c.Single(nullable: false));
            AlterColumn("dbo.Tickets", "moneyInvested", c => c.Single(nullable: false));
            AlterColumn("dbo.Games", "Coefficient3", c => c.Single(nullable: false));
            AlterColumn("dbo.Games", "Coefficient2", c => c.Single(nullable: false));
            AlterColumn("dbo.Games", "Coefficient1", c => c.Single(nullable: false));
        }
    }
}
