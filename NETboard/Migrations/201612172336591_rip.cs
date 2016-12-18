namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rip : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StudentNotVieweds",
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
            
            CreateTable(
                "dbo.StudentVieweds",
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
            DropForeignKey("dbo.StudentVieweds", "SpecificStudent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentVieweds", "SpecificAnnouncementId", "dbo.Announcements");
            DropForeignKey("dbo.StudentNotVieweds", "SpecificStudent_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.StudentNotVieweds", "SpecificAnnouncementId", "dbo.Announcements");
            DropIndex("dbo.StudentVieweds", new[] { "SpecificStudent_Id" });
            DropIndex("dbo.StudentVieweds", new[] { "SpecificAnnouncementId" });
            DropIndex("dbo.StudentNotVieweds", new[] { "SpecificStudent_Id" });
            DropIndex("dbo.StudentNotVieweds", new[] { "SpecificAnnouncementId" });
            DropTable("dbo.StudentVieweds");
            DropTable("dbo.StudentNotVieweds");
        }
    }
}
