using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Infra.Data.EF.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF
{
    public class CodeFlixCatalogDbContext : DbContext
    {
        public CodeFlixCatalogDbContext(DbContextOptions options)
            : base(options) {}

        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
