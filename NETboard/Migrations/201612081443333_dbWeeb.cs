namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbWeeb : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "announcementTimeStamp", c => c.String());
            DropColumn("dbo.Announcements", "annoucmementTimeStamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcements", "annoucmementTimeStamp", c => c.String());
            DropColumn("dbo.Announcements", "announcementTimeStamp");
        }
    }
}
