namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idfk : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tickets", "totalCoefficient");
            DropColumn("dbo.Tickets", "WinMoney");
            DropColumn("dbo.Tickets", "win");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tickets", "win", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tickets", "WinMoney", c => c.Single(nullable: false));
            AddColumn("dbo.Tickets", "totalCoefficient", c => c.Single(nullable: false));
        }
    }
}
