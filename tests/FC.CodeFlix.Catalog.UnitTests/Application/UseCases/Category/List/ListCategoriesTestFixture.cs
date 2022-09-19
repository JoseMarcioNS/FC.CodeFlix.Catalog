using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.List
{
    [CollectionDefinition(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTestFixtureCollection : ICollectionFixture<ListCategoriesTestFixture> { }
    public class ListCategoriesTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();

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
        public DomainEntity.Category GetValidCategory() => new(GetValidName(), GetValidDescription());
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
                 dir: radom.Next(1,5) > 5 ?  SearchOrder.Asc : SearchOrder.Desc
                 );
        }
    }
}
