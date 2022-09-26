using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Get
{
    [CollectionDefinition(nameof(GetCategoryTestFixure))]
    public class GetCategoryTestFixureCollection : ICollectionFixture<GetCategoryTestFixure>
    { }
    public class GetCategoryTestFixure : CategoryCommonFixture
    { }
}
