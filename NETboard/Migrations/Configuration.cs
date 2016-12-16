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


            var userBen = new ApplicationUser { UserName = "Ben@hotmail.co.uk" };
            var userBobLect = new ApplicationUser { UserName = "BobLecturer@hotmail.co.uk" };
            var userBob = new ApplicationUser { UserName = "Bob@hotmail.co.uk" };

            identityRoles = userManager.Create(userBobLect, "password");
            userManager.Create(userBen, "password");
            userManager.Create(userBob, "password");
            userManager.Create(userBob, "password");

            if (identityRoles.Succeeded == false)
                return identityRoles.Succeeded;
            identityRoles = userManager.AddToRole(userBobLect.Id, "canEdit");
            return identityRoles.Succeeded;
        }
    }
}
