namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "team1Score", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "team2Score", c => c.Int(nullable: false));
            DropColumn("dbo.Teams", "score");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Teams", "score", c => c.Int(nullable: false));
            DropColumn("dbo.Games", "team2Score");
            DropColumn("dbo.Games", "team1Score");
        }
    }
}
