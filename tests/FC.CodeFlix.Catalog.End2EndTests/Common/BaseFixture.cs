using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.SharedTests;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.End2EndTests.Common
{
    public class BaseFixture : CategoryBaseFixture
    {
        public ApiClient ApiClient { get; set; }
        public HttpClient HttpClient { get; set; }
        public CustomWebApplicationFactory<Program> WebFactory { get; set; }
        public BaseFixture()
        {

            WebFactory = new CustomWebApplicationFactory<Program>();
            HttpClient = WebFactory.CreateClient();
            ApiClient = new ApiClient(HttpClient);
        }

        public CodeFlixCatalogDbContext CreateDbContext()
        {
            return new CodeFlixCatalogDbContext(
                new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
               .UseInMemoryDatabase($"end2end-test-db")
                .Options
                );
        }
        public void CleanInMemoryDatabase()
            => CreateDbContext().Database.EnsureDeleted();
    }
}
