

using ClaimsRUs.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimsRUs.Entity
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        public DbSet<ApplicationUser> applicationUser { get; set; }
        public DbSet<Claim> claim { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Role> role{ get; set; }
        public DbSet<UserAuditTrail> userAudiotTrail { get; set; }
        public DbSet<Vehicle> vehicle { get; set; }
    }
}
