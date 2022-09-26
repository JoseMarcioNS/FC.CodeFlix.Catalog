using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository
{
    [CollectionDefinition(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTestFixtureCollection : ICollectionFixture<CategoryRepositoryTestFixture> { }
    public class CategoryRepositoryTestFixture : CommonFixture
    {
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
       
    }
}
