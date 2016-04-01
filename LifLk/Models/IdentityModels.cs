using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using AspNet.Identity.MySQL;

namespace LifLk.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : MySQLDatabase
    {
        public ApplicationDbContext()
            : base("LifDBIdentity")
        {
        }
    }
}