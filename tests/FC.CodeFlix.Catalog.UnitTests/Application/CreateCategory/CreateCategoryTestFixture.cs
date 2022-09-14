using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory
{
    public class CreateCategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
        public string GetValidName()
        {
            var name = "";
            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }
        public string GetValidDescription()
        {
            var description = Faker.Commerce.ProductDescription();
            if (description.Length > 10_000)
                description = description[..10_000];

            return description;
        }
        public bool GetRandomActive() => new Random().NextDouble() > 0.5;
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
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTestFixtureCollection : ICollectionFixture<CreateCategoryTestFixture> { }
}
