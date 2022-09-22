using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;
namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Get
{
    [CollectionDefinition(nameof(GetCategoryTestFixture))]
    public class GetCategoryTestFixtureColletion : ICollectionFixture<GetCategoryTestFixture>
    {
    }
    public class GetCategoryTestFixture : CategoryBaseFixture
    {

    }
}
