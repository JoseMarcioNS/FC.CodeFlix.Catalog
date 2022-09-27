using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Get
{
    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureCollection : ICollectionFixture<GetCategoryTestFixture>
    {
    }
    public class GetCategoryTestFixture : CategoryCommonFixture
    {
    }
}
