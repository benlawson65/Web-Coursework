namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pendingChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "Title", c => c.String());
            AddColumn("dbo.Announcements", "Content", c => c.String());
            AddColumn("dbo.Announcements", "TimeStamp", c => c.String());
            DropColumn("dbo.Announcements", "announcementTitle");
            DropColumn("dbo.Announcements", "announcementContent");
            DropColumn("dbo.Announcements", "announcementTimeStamp");
            DropColumn("dbo.Announcements", "staffID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcements", "staffID", c => c.Int(nullable: false));
            AddColumn("dbo.Announcements", "announcementTimeStamp", c => c.String());
            AddColumn("dbo.Announcements", "announcementContent", c => c.String());
            AddColumn("dbo.Announcements", "announcementTitle", c => c.String());
            DropColumn("dbo.Announcements", "TimeStamp");
            DropColumn("dbo.Announcements", "Content");
            DropColumn("dbo.Announcements", "Title");
        }
    }
}
