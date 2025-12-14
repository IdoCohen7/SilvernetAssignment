using Microsoft.EntityFrameworkCore;
using Silvernet.Data.Configurations;
using Silvernet.Models;

namespace Silvernet.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions DbContextOptions) : base(DbContextOptions)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TenantConfiguration());


        }
    }
}
