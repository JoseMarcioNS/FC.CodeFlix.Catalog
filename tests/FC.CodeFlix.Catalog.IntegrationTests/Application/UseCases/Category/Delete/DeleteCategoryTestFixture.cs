using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Delete
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    {
    }
    public class DeleteCategoryTestFixture : CategoryBaseFixture
    {
    }
}
