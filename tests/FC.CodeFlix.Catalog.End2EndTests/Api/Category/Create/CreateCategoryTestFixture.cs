using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common;
using FC.CodeFlix.Catalog.End2EndTests.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Create
{
    [CollectionDefinition(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryFixtureCollection
        : ICollectionFixture<CreateCategoryTestFixture>
    {
    }
    public class CreateCategoryTestFixture : CategoryBaseFixture
    {
        public CreateCategoryInput CreateCategoryInput()
        => new(GetValidName()
                , GetValidDescription()
                , GetRandomActive());

        public CreateCategoryInput GetCategoryInputInvalidNameMinLenght()
        => new(GetInvalidNameMinLenght()
                , GetValidDescription()
                , GetRandomActive());
        public CreateCategoryInput GetCategoryInputInvalidDescriptionMaxLeght()
        => new(GetValidName()
                , GetInvalidDescriptionMaxLeght()
                , GetRandomActive());

        public CreateCategoryInput GetCategoryInvalidNameMaxLeght()
         => new(GetInvalidNameMaxLeght()
                , GetInvalidDescriptionMaxLeght()
                , GetRandomActive());
       
    }
}
