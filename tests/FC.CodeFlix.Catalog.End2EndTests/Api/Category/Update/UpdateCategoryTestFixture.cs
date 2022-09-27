using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Update
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTestFixtureCollection
        : ICollectionFixture<UpdateCategoryTestFixture>
    {
    }
    public class UpdateCategoryTestFixture : CategoryCommonFixture
    {
        public UpdateCategoryInput GetUpdateCategoryInput(Guid id)
         => new(
                id,
                GetValidName(),
                GetValidDescription(),
                GetRandomActive());

        public UpdateCategoryInput GetInputInvalidNameMinLenght(DomainEntity.Category input)
         => new(input.Id, GetInvalidNameMinLenght(), input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidNameMaxLeght(DomainEntity.Category input)
        => new(input.Id, GetInvalidNameMaxLeght(), input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidDescriptionMaxLeght(DomainEntity.Category input)
        => new(input.Id, input.Name, GetInvalidDescriptionMaxLeght(), input.IsActive);
        
    }
}
