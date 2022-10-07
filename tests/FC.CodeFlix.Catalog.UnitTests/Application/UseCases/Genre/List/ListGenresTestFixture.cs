using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.List
{
    [CollectionDefinition(nameof(ListGenresTestFixture))]
    public class ListGenreTestFixtureCollection : ICollectionFixture<ListGenresTestFixture>
    {
    }
    public class ListGenresTestFixture : GenreCommonFixture
    {
        public ListGenresInput GetListGenresInput()
        {
            var radom = new Random();
            return new ListGenresInput(
                 page: radom.Next(1, 10),
                 perPage: radom.Next(15, 50),
                 search: Faker.Commerce.ProductName(),
                 sort: Faker.Commerce.ProductName(),
                 dir: radom.Next(1, 5) > 5 ? SearchOrder.Asc : SearchOrder.Desc
                 );
        }
    }
}
