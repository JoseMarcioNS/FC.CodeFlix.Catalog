using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture> { }
    public class CategoryRepositoryTestFixture : BaseFixture
    {
        public CodeFlixCatalogDbContext CreateDbContext()
        {
            return new CodeFlixCatalogDbContext(
                new DbContextOptionsBuilder<CodeFlixCatalogDbContext>()
                .UseInMemoryDatabase("integration-test-db")
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

        public List<Category> CreateCategoriesWithNames(string[] names)
            => names.Select(n =>
            {
                var category = GetCategory();
                category.Update(n);
                return category;
            }).ToList();
        public List<Category> CloneCategoriesOrdered(List<Category> categories, string orderBy, SearchOrder searchOrder)
        {
            var categoriesOrdered = (orderBy.ToLower(), searchOrder) switch
            {
                ("name", SearchOrder.Asc) => categories.OrderBy(x => x.Name).ToList(),
                ("name", SearchOrder.Desc) => categories.OrderByDescending(x => x.Name).ToList(),
                ("id", SearchOrder.Asc) => categories.OrderBy(x => x.Id).ToList(),
                ("id", SearchOrder.Desc) => categories.OrderByDescending(x => x.Id).ToList(),
                ("createdat", SearchOrder.Asc) => categories.OrderBy(x => x.CreatedAt).ToList(),
                ("createdat", SearchOrder.Desc) => categories.OrderByDescending(x => x.CreatedAt).ToList(),
                _ => categories.OrderBy(x => x.Name).ToList()
            };

            return categoriesOrdered;
        }
        public void CleanInMemoryDatabase()
            => CreateDbContext().Database.EnsureDeleted();
    }
}
