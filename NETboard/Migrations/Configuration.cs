namespace NETboard.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;

    internal sealed class Configuration : DbMigrationsConfiguration<NETboard.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "NETboard.Models.ApplicationDbContext";
        }

        protected override void Seed(NETboard.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            //Seeding users
            AddUserAndRole(context);
            
        }

        bool AddUserAndRole(NETboard.Models.ApplicationDbContext context) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            IdentityResult identityRoles = roleManager.Create(new IdentityRole("canEdit"));

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            //seed students and lecutrer, giving passwords and roles
            var student1 = new ApplicationUser { UserName = "Student1@email.com" };
            var student2 = new ApplicationUser { UserName = "Student2@email.com" };
            var student3 = new ApplicationUser { UserName = "Student3@email.com" };
            var student4 = new ApplicationUser { UserName = "Student4@email.com" };
            var student5 = new ApplicationUser { UserName = "Student5@email.com" };
            var lecturer = new ApplicationUser { UserName = "Lecturer1@email.com" };

            identityRoles = userManager.Create(lecturer, "password");
            userManager.Create(student1, "password");
            userManager.Create(student2, "password");
            userManager.Create(student3, "password");
            userManager.Create(student4, "password");
            userManager.Create(student5, "password");

            if (identityRoles.Succeeded == false)
                return identityRoles.Succeeded;
            identityRoles = userManager.AddToRole(lecturer.Id, "canEdit");
            return identityRoles.Succeeded;
        }
    }
}
