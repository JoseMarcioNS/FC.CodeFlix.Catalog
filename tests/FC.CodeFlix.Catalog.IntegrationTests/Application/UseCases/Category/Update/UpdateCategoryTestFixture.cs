using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Update
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoyTestFixtureCollection : ICollectionFixture<UpdateCategoryTestFixture>
    {
    }
    public class UpdateCategoryTestFixture : CategoryBaseFixture
    {
        public UpdateCategoryInput GetUpdateCategoryInput(Guid? id = null)
           => new(
               id ?? Guid.NewGuid(),
               GetValidName(),
               GetValidDescription(),
               GetRandomActive()
               );
    }
}
