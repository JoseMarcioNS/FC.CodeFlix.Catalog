using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.List
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoryTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture>
    {
    }
    public class ListCategoriesTestFixture : CategoryBaseFixture
    {
        public List<DomainEntity.Category> CreateCategoriesWithNames(string[] names)
          => names.Select(n =>
          {
              var category = GetValidCategory();
              category.Update(n);
              return category;
          }).ToList();
        public List<DomainEntity.Category> CloneCategoriesOrdered(List<DomainEntity.Category> categories, string orderBy, SearchOrder searchOrder)
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
