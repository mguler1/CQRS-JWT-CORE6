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
        public DbSet<AppUser> AppUsers => this.Set<AppUser>();
        public DbSet<AppRole> AppRoles =>this.Set<AppRole>();
        public DbSet<Category> Categories => this.Set<Category>();
        public DbSet<Product> Products =>this.Set<Product>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
