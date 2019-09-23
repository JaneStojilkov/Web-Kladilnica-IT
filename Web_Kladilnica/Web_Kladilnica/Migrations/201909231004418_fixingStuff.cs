namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingStuff : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "Completed");
            DropColumn("dbo.Games", "Time");
            DropColumn("dbo.Games", "HalfTime");
            DropColumn("dbo.Games", "selectedCoefficient");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "selectedCoefficient", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "HalfTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.Games", "Time", c => c.Single(nullable: false));
            AddColumn("dbo.Games", "Completed", c => c.Boolean(nullable: false));
        }
    }
}
