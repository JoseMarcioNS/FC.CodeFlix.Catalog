using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.SharedTests.Common;
using System.Collections.Generic;

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

        public List<Genre> GetValidGenres(int count = 10)
           => Enumerable.Range(1, count).Select(_ => GetValidGenre()).ToList();
        public List<Genre> GetValidGenresWithNames(List<string> names)
        => names.Select(n => new Genre(n)).ToList();
    }
}
