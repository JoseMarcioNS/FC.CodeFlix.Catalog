using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture> { }
    public class CategoryRepositoryTestFixture : CategoryBaseFixture
    {
        public CommonFixture CommonFixture;
        public CategoryRepositoryTestFixture()
        => CommonFixture = new CommonFixture();
        

        public List<Category> CreateCategoriesWithNames(string[] names)
          => names.Select(n =>
          {
              var category = GetValidCategory();
              category.Update(n);
              return category;
          }).ToList();
        public List<Category> CloneCategoriesOrdered(List<Category> categories, string orderBy, SearchOrder searchOrder)
        {
            var categoriesOrdered = (orderBy.ToLower(), searchOrder) switch
            {
                ("name", SearchOrder.Asc) => categories.OrderBy(x => x.Name).ThenBy(x => x.Id),
                ("name", SearchOrder.Desc) => categories.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => categories.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => categories.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => categories.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => categories.OrderByDescending(x => x.CreatedAt),
                _ => categories.OrderBy(x => x.Name).ThenBy(x => x.Id)
            };

            return categoriesOrdered.ToList();
        }
       
    }
}
