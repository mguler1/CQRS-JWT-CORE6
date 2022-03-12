using API.Core.Domain;
using API.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;

namespace API.Persistance.Context
{
    public class CoreContext:DbContext
    {
        public CoreContext(DbContextOptions<CoreContext>options):base(options)
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
