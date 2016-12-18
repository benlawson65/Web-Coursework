namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addlinkmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LinkAnnouncementAndStudents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SpecificStudentId = c.Int(nullable: false),
                        SpecificAnnouncementId = c.Int(nullable: false),
                        SpecificStudent_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Announcements", t => t.SpecificAnnouncementId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.SpecificStudent_Id)
                .Index(t => t.SpecificAnnouncementId)
                .Index(t => t.SpecificStudent_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LinkAnnouncementAndStudents", "SpecificStudent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LinkAnnouncementAndStudents", "SpecificAnnouncementId", "dbo.Announcements");
            DropIndex("dbo.LinkAnnouncementAndStudents", new[] { "SpecificStudent_Id" });
            DropIndex("dbo.LinkAnnouncementAndStudents", new[] { "SpecificAnnouncementId" });
            DropTable("dbo.LinkAnnouncementAndStudents");
        }
    }
}
