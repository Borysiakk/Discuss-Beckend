using Discuss.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Discuss.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        
        public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(DependencyInjection.DbConnection);
                return new ApplicationDbContext(builder.Options);
            }
        }
    }
}