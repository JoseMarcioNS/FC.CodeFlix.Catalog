using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.List
{
    [CollectionDefinition(nameof(ListCategoryTestFixture))]
    public class ListCategoryTestFixtureCollection 
        : ICollectionFixture<ListCategoryTestFixture> 
    {
    }
    public class ListCategoryTestFixture : CategoryCommonFixture
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
                ("name", SearchOrder.Asc) => categories.OrderBy(x => x.Name).ThenBy(x=> x.Id),
                ("name", SearchOrder.Desc) => categories.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => categories.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => categories.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => categories.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => categories.OrderByDescending(x => x.CreatedAt),
                _ => categories.OrderBy(x => x.Name).ThenBy(x => x.Id),
            };

            return categoriesOrdered.ToList();
        }
    }
}
