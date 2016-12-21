namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingMoreValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Announcements", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Announcements", "TimeStamp", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Announcements", "TimeStamp", c => c.String(nullable: false));
            AlterColumn("dbo.Announcements", "Title", c => c.String());
        }
    }
}
