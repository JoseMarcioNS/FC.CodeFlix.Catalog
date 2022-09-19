using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [CollectionDefinition(nameof(UpdateGategoryTestFixture))]
    public class UpdateCategoryColletionFixture : ICollectionFixture<UpdateGategoryTestFixture>
    { }
    public class UpdateGategoryTestFixture : CategoryBaseFixture
    {
        public UpdateCategoryInput GetCategory(Guid? id = null)
            => new(
                id ?? Guid.NewGuid(),
                GetValidName(),
                GetValidDescription(),
                GetRandomActive()
                );
    }
}
