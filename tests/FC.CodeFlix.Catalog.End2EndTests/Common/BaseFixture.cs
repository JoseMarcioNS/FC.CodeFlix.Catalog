using FC.CodeFlix.Catalog.Infra.Data.EF;
using FC.CodeFlix.Catalog.SharedTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FC.CodeFlix.Catalog.End2EndTests.Common
{
    public class BaseFixture : CategoryBaseFixture
    {
        public ApiClient ApiClient { get; set; }
        public HttpClient HttpClient { get; set; }
        public CustomWebApplicationFactory<Program> WebFactory { get; set; }
        private readonly string _dbConnectionString;
        public BaseFixture()
        {

            WebFactory = new CustomWebApplicationFactory<Program>();
            HttpClient = WebFactory.CreateClient();
            ApiClient = new ApiClient(HttpClient);
            var configuration = WebFactory.Services.GetService(typeof(IConfiguration));
            ArgumentNullException.ThrowIfNull(configuration);
            _dbConnectionString = ((IConfiguration)configuration).GetConnectionString("CatalogDb");
        }

        public CodeFlixCatalogDbContext CreateDbContext()
        {
            return new CodeFlixCatalogDbContext(
                new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
               .UseMySql(
                    _dbConnectionString,
                    ServerVersion.AutoDetect(_dbConnectionString)
                 )
                .Options
                ); ;
        }
        public void CleanDatabase()
        {
             CreateDbContext().Database.EnsureDeleted();
             CreateDbContext().Database.EnsureCreated();
        }
        
    }
}
