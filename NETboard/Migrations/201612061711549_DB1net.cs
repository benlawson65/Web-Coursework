namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DB1net : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcements", "announcementTitle", c => c.String());
            AlterColumn("dbo.Announcements", "announcementContent", c => c.String());
            AlterColumn("dbo.Announcements", "annoucmementTimeStamp", c => c.String());
            AlterColumn("dbo.Announcements", "staffName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcements", "staffName", c => c.Int(nullable: false));
            AlterColumn("dbo.Announcements", "annoucmementTimeStamp", c => c.Int(nullable: false));
            AlterColumn("dbo.Announcements", "announcementContent", c => c.Int(nullable: false));
            AlterColumn("dbo.Announcements", "announcementTitle", c => c.Int(nullable: false));
        }
    }
}
