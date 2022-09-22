using Bogus;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [CollectionDefinition(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryColletionFixture : ICollectionFixture<UpdateCategoryTestFixture>
    { }
    public class UpdateCategoryTestFixture : CategoryBaseFixture
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
         => new(input.Id, input.Name[..2], input.Description, input.IsActive);

        public UpdateCategoryInput GetInputInvalidNameMaxLeght(DomainEntity.Category input)
        {
            var name = Faker.Commerce.ProductName();
            while (name.Length <= 255)
                name += Faker.Commerce.ProductName();

            return new(input.Id, name, input.Description, input.IsActive);
        }
        public UpdateCategoryInput GetInputInvalidDescriptionMaxLeght(DomainEntity.Category input)
        {
            var description = Faker.Commerce.ProductDescription();
            while (description.Length <= 10_000)
                description += Faker.Commerce.ProductDescription();

            return new(input.Id, input.Name, description, input.IsActive);
        }
    }
}
