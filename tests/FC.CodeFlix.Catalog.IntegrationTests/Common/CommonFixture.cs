using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.IntegrationTests.Common
{
    public class CommonFixture : CategoryBaseFixture
    {
        public CodeFlixCatalogDbContext CreateDbContext()
        {
            return new CodeFlixCatalogDbContext(
                new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
                //.UseInMemoryDatabase($"integration-test-db-{Guid.NewGuid()}")
                .UseInMemoryDatabase($"integration-test-db")
                .Options
                );
        }
        public void CleanInMemoryDatabase()
           => CreateDbContext().Database.EnsureDeleted();
    }
}
