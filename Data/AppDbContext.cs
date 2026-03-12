using OakDevelopments.Models;
using Microsoft.EntityFrameworkCore;


namespace OakDevelopments.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public AppDbContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=OakPropertiesDb;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

    }
}
