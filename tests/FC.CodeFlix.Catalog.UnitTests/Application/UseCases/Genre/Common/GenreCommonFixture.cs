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
    }
}
