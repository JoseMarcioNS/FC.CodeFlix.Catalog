using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Delete
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixtureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    {
    }
    public class DeleteCategoryTestFixture : CategoryCommonFixture
    {
    }
}
