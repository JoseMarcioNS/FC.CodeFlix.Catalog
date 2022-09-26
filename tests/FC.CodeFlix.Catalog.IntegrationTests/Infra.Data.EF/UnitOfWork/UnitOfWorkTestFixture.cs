namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.UnitOfWork
{
    [CollectionDefinition(nameof(UnitOfWorkTestFixture))]
    public class UnitOfWorkTestFixtureCollection : ICollectionFixture<UnitOfWorkTestFixture>
    {
    }
    public class UnitOfWorkTestFixture : CommonFixture
    {
    }
}
