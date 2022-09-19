using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Create
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
    public class CreateCategoryTestFixture : CategoryBaseFixture
    {
        public CreateCategoryInput GetCreateCategoryInput() => new(
                          GetValidName(),
                          GetValidDescription(),
                          GetRandomActive());
        public CreateCategoryInput GetInputInvalidNameNull()
        {
            var input = GetCreateCategoryInput();
            input.Name = null!;
            return input;
        }
        public CreateCategoryInput GetInputInvalidNameMinLenght()
        {
            var input = GetCreateCategoryInput();
            input.Name = input.Name[..2];
            return input;
        }
        public CreateCategoryInput GetInputInvalidNameMaxLeght()
        {
            var input = GetCreateCategoryInput();
            while (input.Name.Length <= 255)
                input.Name += Faker.Commerce.ProductName();
            return input;
        }
        public CreateCategoryInput GetInputInvalidDescriptionNull()
        {
            var input = GetCreateCategoryInput();
            input.Description = null!;
            return input;
        }
        public CreateCategoryInput GetInputInvalidDescriptionMaxLeght()
        {
            var input = GetCreateCategoryInput();

            while (input.Description.Length <= 10_000)
                input.Description += Faker.Commerce.ProductDescription();
            return input;
        }
    }

}
