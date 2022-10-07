using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Delete
{
    [CollectionDefinition(nameof(DeleteGenreTestFixture))]
    public class GetGenreTestFixtureCollection : ICollectionFixture<DeleteGenreTestFixture>
    {
    }
    public class DeleteGenreTestFixture : GenreCommonFixture
    {
    }
}
