using OakDevelopments.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OakDevelopments.Data
{
    public class AppDbContext : IdentityDbContext
    {
        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<PropertyStatus> PropertyStatuses { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }

        public DbSet<Booking> Bookings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Server=localhost;Database=OakPropertiesDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
    }
}