namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class staffNameToString : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Announcements", "staffName_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Announcements", new[] { "staffName_Id" });
            AddColumn("dbo.Announcements", "staffName", c => c.String());
            DropColumn("dbo.Announcements", "staffName_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Announcements", "staffName_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.Announcements", "staffName");
            CreateIndex("dbo.Announcements", "staffName_Id");
            AddForeignKey("dbo.Announcements", "staffName_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
