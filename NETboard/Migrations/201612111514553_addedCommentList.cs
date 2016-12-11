namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedCommentList : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Announcement_Id", c => c.Int());
            CreateIndex("dbo.Comments", "Announcement_Id");
            AddForeignKey("dbo.Comments", "Announcement_Id", "dbo.Announcements", "Id");
            DropColumn("dbo.Comments", "announcementID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "announcementID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Comments", "Announcement_Id", "dbo.Announcements");
            DropIndex("dbo.Comments", new[] { "Announcement_Id" });
            DropColumn("dbo.Comments", "Announcement_Id");
        }
    }
}
