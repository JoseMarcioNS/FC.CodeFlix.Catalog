using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Get
{
    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureColletion : ICollectionFixture<GetCategoryTestFixture>
    {
    }
    public class GetCategoryTestFixture : CategoryBaseFixture
    {
        public CommonFixture CommonFixture;
        public GetCategoryTestFixture()
        => CommonFixture = new CommonFixture();
    }
}
