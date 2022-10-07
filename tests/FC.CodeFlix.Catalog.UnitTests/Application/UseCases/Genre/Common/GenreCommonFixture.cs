using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.SharedTests;


namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Common
{
    public class GenreCommonFixture : GenreBaseFixture
    {
        public Mock<IGenreRepository> GetGenreRepositoryMock() => new();
        public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();
        public DomainEntity.Genre GetGenre()
              => new(GetValidName(), GetRandomActive());
        public List<DomainEntity.Genre> GetGenres(int length = 10)
        {
            List<DomainEntity.Genre> genres = new();
            for (int i = 0; i < length; i++)
                genres.Add(GetGenreWithCategories());

            return genres;
        }
            
        public DomainEntity.Genre GetGenreWithCategories()
        {
            var genre = new DomainEntity.Genre(GetValidName(), GetRandomActive());
            var categoriesIds = GetRandomCategoriesIs();
            categoriesIds.ForEach(genre.AddCategory);
            return genre;
        }
             
        public List<Guid> GetRandomCategoriesIs(int length = 10)
              => Enumerable.Range(1
                 , new Random().Next(1, length))
                .Select(_ => Guid.NewGuid()).ToList();
    }
}
