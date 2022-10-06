using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Genre
{
    [CollectionDefinition(nameof(GenreTestFixture))]
    public class GenreTestFixtureCollection
        : ICollectionFixture<GenreTestFixture>
    { }
    public class GenreTestFixture : GenreBaseFixture
    { }
}
