using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Delete
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    { }

    public class DeleteCategoryTestFixture : CategoryCommonFixture
    { }
}
