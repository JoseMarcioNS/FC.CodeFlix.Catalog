using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;
using FC.CodeFlix.Catalog.End2EndTests.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Create
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryFixtureCollection
        : ICollectionFixture<CreateCategoryTestFixture>
    {
    }
    public class CreateCategoryTestFixture : CategoryBaseFixture
    {
    }
}
