namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingCommentsFeature : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "announcementID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "announcementID");
        }
    }
}
