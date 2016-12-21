namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcements", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Announcements", "TimeStamp", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "CommentContent", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Comments", "CommentContent", c => c.String(nullable: false));
            AlterColumn("dbo.Announcements", "TimeStamp", c => c.String());
            AlterColumn("dbo.Announcements", "Content", c => c.String());
        }
    }
}
