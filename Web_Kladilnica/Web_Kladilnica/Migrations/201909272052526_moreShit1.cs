namespace Web_Kladilnica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreShit1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tickets", "InternalGuesses", c => c.String());
            AddColumn("dbo.Tickets", "InternalgameIDs", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tickets", "InternalgameIDs");
            DropColumn("dbo.Tickets", "InternalGuesses");
        }
    }
}
