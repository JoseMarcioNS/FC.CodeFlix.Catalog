using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.SharedTests.Common;

namespace FC.CodeFlix.Catalog.SharedTests
{
    public class GenreBaseFixture : BaseFixture
    {
        public string GetValidName()
        => Faker.Commerce.ProductName();

        public Genre GetValidGenre(List<Guid>? categoriesIds = null)
        {
            var genre = new Genre(GetValidName());
            if (categoriesIds is not null)
                foreach (var categoryId in categoriesIds)
                    genre.AddCategory(categoryId);

            return genre;
        }
     }
}
