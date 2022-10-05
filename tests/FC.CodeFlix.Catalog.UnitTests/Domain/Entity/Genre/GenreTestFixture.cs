using FC.CodeFlix.Catalog.SharedTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Genre
{
    [CollectionDefinition(nameof(GenreTestFixture))]
    public class GenreTestFixtureCollection
        : ICollectionFixture<GenreTestFixture>
    { }
    public class GenreTestFixture : BaseFixture
    {
        public string GetValidName()
         => Faker.Commerce.ProductName();

        public DomainEntity.Genre GetValidGenre(List<Guid>? categoriesIds = null)
        {
            var genre = new DomainEntity.Genre(GetValidName());
            if (categoriesIds is not null)
                foreach (var categoryId in categoriesIds)
                   genre.AddCategory(categoryId);
        
            return genre;
        }

    }
}
