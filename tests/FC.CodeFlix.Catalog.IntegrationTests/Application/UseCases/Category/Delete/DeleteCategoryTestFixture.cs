using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Delete
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    {
    }
    public class DeleteCategoryTestFixture : CategoryBaseFixture
    {
        public CommonFixture CommonFixture;
        public DeleteCategoryTestFixture()
        => CommonFixture = new CommonFixture();
    }
}
