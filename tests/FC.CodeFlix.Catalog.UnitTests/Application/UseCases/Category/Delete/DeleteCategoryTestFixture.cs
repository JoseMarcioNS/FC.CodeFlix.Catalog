using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Delete
{
    [CollectionDefinition(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTestFixureCollection : ICollectionFixture<DeleteCategoryTestFixture>
    {}

    public class DeleteCategoryTestFixture : CategoryBaseFixture
    {}
}
