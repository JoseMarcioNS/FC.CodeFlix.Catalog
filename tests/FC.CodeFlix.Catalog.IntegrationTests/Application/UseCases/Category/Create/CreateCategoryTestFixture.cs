using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Common;
namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Create
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class GetCategoryTestFixtureColletion : ICollectionFixture<CreateCategoryTestFixture>
    {
    }
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
            input.Name = GetInvalidNameMinLenght();
            return input;
        }
        public CreateCategoryInput GetInputInvalidNameMaxLeght()
        {
            var input = GetCreateCategoryInput();
            input.Name = GetInvalidNameMaxLeght();
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
           input.Description = GetInvalidDescriptionMaxLeght();
           return input;

        }
    }
}

