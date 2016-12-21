using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NETboard.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
      //  public DbSet<Announcement> Announcements { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public AnnouncementWithItsComment AnnouncementWithItsComment { get; set; }

        public System.Data.Entity.DbSet<NETboard.Models.LinkAnnouncementAndStudent> LinkAnnouncementAndStudents { get; set; }

        public System.Data.Entity.DbSet<NETboard.Models.StudentNotViewed> StudentNotVieweds { get; set; }

        public System.Data.Entity.DbSet<NETboard.Models.StudentViewed> StudentVieweds { get; set; }
    }
}