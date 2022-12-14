using Bogus;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryColletionFixture : ICollectionFixture<UpdateCategoryTestFixture>
    { }
    public class UpdateCategoryTestFixture : CategoryCommonFixture
    {
        public UpdateCategoryInput GetCategory(Guid? id = null)
            => new(
                id ?? Guid.NewGuid(),
                GetValidName(),
                GetValidDescription(),
                GetRandomActive()
                );

        public UpdateCategoryInput GetInputInvalidNameNull(DomainEntity.Category input)
        => new(input.Id, null!, input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidNameMinLenght(DomainEntity.Category input)
         => new(input.Id, GetInvalidNameMinLenght(), input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidNameMaxLeght(DomainEntity.Category input)
        => new(input.Id, GetInvalidNameMaxLeght(), input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidDescriptionMaxLeght(DomainEntity.Category input)
       => new(input.Id, input.Name, GetInvalidDescriptionMaxLeght(), input.IsActive);

    }
}
