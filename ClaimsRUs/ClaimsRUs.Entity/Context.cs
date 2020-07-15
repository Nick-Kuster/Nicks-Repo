

using ClaimsRUs.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace ClaimsRUs.Entity
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ClaimContactVehicle>()
                .HasKey(ccv => new { ccv.ClaimId, ccv.ContactId, ccv.VehicleId });
            builder.Entity<ClaimContactVehicle>()
                .HasOne(ccv => ccv.Claim)
                .WithMany(c => c.ClaimContactVehicles)
                .HasForeignKey(ccv => ccv.ClaimId);
            builder.Entity<ClaimContactVehicle>()
                .HasOne(ccv => ccv.Vehicle)
                .WithMany(v => v.ClaimContactVehicles)
                .HasForeignKey(ccv => ccv.VehicleId);
            builder.Entity<ClaimContactVehicle>()
                .HasOne(ccv => ccv.Contact)
                .WithMany(c => c.ClaimContactVehicles)
                .HasForeignKey(ccv => ccv.ContactId);
            builder.Entity<Vehicle>()
                .HasOne(v => v.Contact)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.ContactId);
        }

        public DbSet<ApplicationUser> applicationUser { get; set; }
        public DbSet<Claim> claim { get; set; }
        public DbSet<Contact> contact { get; set; }
        public DbSet<Role> role{ get; set; }
        public DbSet<UserAuditTrail> userAuditTrail { get; set; }
        public DbSet<Vehicle> vehicle { get; set; }
        public DbSet<ClaimContactVehicle> claimContactVehicle { get; set; }

    }
}
