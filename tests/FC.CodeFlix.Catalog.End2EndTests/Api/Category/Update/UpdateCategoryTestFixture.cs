using FC.CodeFlix.Catalog.Api.Models;
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
        public UpdateCategoryApiInput GetUpdateCategoryApiInput()
         => new(
                GetValidName(),
                GetValidDescription(),
                GetRandomActive());

        public UpdateCategoryApiInput GetInputInvalidNameMinLenght(DomainEntity.Category input)
         => new(GetInvalidNameMinLenght(), input.Description, input.IsActive);

        public UpdateCategoryApiInput GetInputInvalidNameMaxLeght(DomainEntity.Category input)
        => new(GetInvalidNameMaxLeght(), input.Description, input.IsActive);

        public UpdateCategoryApiInput GetInputInvalidDescriptionMaxLeght(DomainEntity.Category input)
        => new(input.Name, GetInvalidDescriptionMaxLeght(), input.IsActive);
        
    }
}
