namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.GenreRepository
{
    [CollectionDefinition(nameof(GenreRepositoryTestFixture))]
    public class GenreRepositoryTestFixtureCollect : ICollectionFixture<GenreRepositoryTestFixture>
    {
    }
    public class GenreRepositoryTestFixture : CommonFixture
    {
    }
}
