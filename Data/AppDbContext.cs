using OakDevelopments.Models;
using Microsoft.EntityFrameworkCore;

namespace OakDevelopments.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor used when dependency injection provides options
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets for your tables
        public DbSet<Property> Properties { get; set; }
        public DbSet<Agent> Agents { get; set; }
        public DbSet<PropertyStatus> PropertyStatuses { get; set; }
        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<CompanyInfo> CompanyInfos { get; set; }

        // Optional: configure connection string if not using Program.cs
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