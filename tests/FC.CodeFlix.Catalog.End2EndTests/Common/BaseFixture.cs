using Bogus;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.End2EndTests.Common
{
    public class BaseFixture
    {
        public Faker Faker { get; set; }
        public ApiClient ApiClient { get; set; }
        public HttpClient HttpClient { get; set; }
        public CustomWebApplicationFactory<Program> WebFactory { get; set; }
        public BaseFixture()
        {
            Faker = new Faker("pt_BR");
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
        public string GetValidName()
        {
            var name = "";
            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }
        public string GetValidDescription()
        {
            var description = Faker.Commerce.ProductDescription();
            if (description.Length > 10_000)
                description = description[..10_000];

            return description;
        }
        public bool GetRandomActive() => new Random().NextDouble() > 0.5;

        public Category GetCategory()
            => new(GetValidName(),
                   GetValidDescription(),
                   GetRandomActive());

        public List<Category> GetListCategories(int length = 10)
          => Enumerable.Range(1, length)
                .Select(_ => GetCategory()).ToList();
        public void CleanInMemoryDatabase()
           => CreateDbContext().Database.EnsureDeleted();
    }
}
