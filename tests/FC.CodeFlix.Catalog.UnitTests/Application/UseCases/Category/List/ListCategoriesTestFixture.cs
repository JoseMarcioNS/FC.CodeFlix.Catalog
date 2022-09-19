using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.List
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
    public class ListCategoriesTestFixture : CategoryBaseFixture
    {
        public List<DomainEntity.Category> ListCategories(int length = 10)
        {
            List<DomainEntity.Category> categories = new List<DomainEntity.Category>();
            for (int i = 0; i < length; i++)
                categories.Add(GetValidCategory());

            return categories;
        }
        public ListCategoriesInput GetListCategoriesInput()
        {
            var radom = new Random();
            return new ListCategoriesInput(
                 page: radom.Next(1, 10),
                 perPage: radom.Next(15, 50),
                 search: Faker.Commerce.ProductName(),
                 sort: Faker.Commerce.ProductName(),
                 dir: radom.Next(1, 5) > 5 ? SearchOrder.Asc : SearchOrder.Desc
                 );
        }
    }
}
