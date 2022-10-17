using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.GenreRepository
{
    [CollectionDefinition(nameof(GenreRepositoryTestFixture))]
    public class GenreRepositoryTestFixtureCollection : ICollectionFixture<GenreRepositoryTestFixture>
    {
    }
    public class GenreRepositoryTestFixture : GenreBaseFixture
    {
        public CommonFixture CommonFixture;
        public CategoryBaseFixture CategoryFixture;
        public GenreRepositoryTestFixture()
        {
            CommonFixture = new CommonFixture();
            CategoryFixture = new CategoryBaseFixture();
        }

        public List<Genre> CloneGenresOrdered(List<Genre> genres, string orderBy, SearchOrder searchOrder)
        {
            var categoriesOrdered = (orderBy.ToLower(), searchOrder) switch
            {
                ("name", SearchOrder.Asc) => genres.OrderBy(x => x.Name).ThenBy(x => x.Id),
                ("name", SearchOrder.Desc) => genres.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => genres.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => genres.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => genres.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => genres.OrderByDescending(x => x.CreatedAt),
                _ => genres.OrderBy(x => x.Name).ThenBy(x => x.Id)
            };

            return categoriesOrdered.ToList();
        }
    }
}

