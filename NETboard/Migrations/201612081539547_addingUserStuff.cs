namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingUserStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Announcements", "staffName_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Announcements", "staffName_Id");
            AddForeignKey("dbo.Announcements", "staffName_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Announcements", "staffName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcements", "staffName", c => c.String());
            DropForeignKey("dbo.Announcements", "staffName_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Announcements", new[] { "staffName_Id" });
            DropColumn("dbo.Announcements", "staffName_Id");
        }
    }
}
