using FC.CodeFlix.Catalog.Application.UseCases.Genre.Create;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Create
{
    [CollectionDefinition(nameof(CreateGenreTestFixture))]
    public class CreateGenreTestFixtureCollection : ICollectionFixture<CreateGenreTestFixture>
    {
    }
    public class CreateGenreTestFixture : GenreCommonFixture
    {
        public CreateGenreInput GetCreateGenreInput()
           => new(GetValidName(), GetRandomActive());
        public CreateGenreInput GetCreateGenreInput(string? name = null)
           => new(name!, GetRandomActive());

        public CreateGenreInput GetCreateGenreInputWithCategories()
        {
            var numberOfCategoriesIds = new Random().Next(1,10);
            var categoriesIds = Enumerable.Range(1, numberOfCategoriesIds)
                .Select(_=> Guid.NewGuid()).ToList();
            return new CreateGenreInput(GetValidName(), GetRandomActive(), categoriesIds);
        }
    }
}
