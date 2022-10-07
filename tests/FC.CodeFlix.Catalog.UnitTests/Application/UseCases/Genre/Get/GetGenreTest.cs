using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Get;
using FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Get
{
    [Collection(nameof(GetGenreTestFixture))]
    public class GetGenreTest
    {
        private readonly GetGenreTestFixture _fixture;

        public GetGenreTest(GetGenreTestFixture fixture) => _fixture = fixture;

        [Fact(DisplayName = nameof(Get))]
        [Trait("Applcation", "GetGenre - Usecases")]
        public async Task Get()
        {
            var genreRepisitoryMock = _fixture.GetGenreRepositoryMock();
            var genre = _fixture.GetGenreWithCategories();
            genreRepisitoryMock.Setup(x => x.Get(
                It.Is<Guid>(x => x == genre.Id),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(genre);
            var input = new GetGenreInput(genre.Id);
            var useCase = new GetGenre(genreRepisitoryMock.Object);

            GenreModelOutput output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(genre.Id);
            output.Name.Should().Be(genre.Name);
            output.IsActive.Should().Be(genre.IsActive);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            output.CategoriesIds.Should().HaveCount(genre.Categories.Count);
            genreRepisitoryMock.Verify(x => x.Get(It.Is<Guid>(x => x == genre.Id),
                It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact(DisplayName = nameof(ThrowExceptionWhenNotFound))]
        [Trait("Applcation", "GetGenre - Usecases")]
        public async Task ThrowExceptionWhenNotFound()
        {
            var genreRepisitoryMock = _fixture.GetGenreRepositoryMock();
            var genreId = Guid.NewGuid();
            genreRepisitoryMock.Setup(x => x.Get(
                It.Is<Guid>(x => x == genreId),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Genre '{genreId}' not found"));
            var input = new GetGenreInput(genreId);
            var useCase = new GetGenre(genreRepisitoryMock.Object);

            var action = async () => await useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<NotFoundException>()
                  .WithMessage($"Genre '{genreId}' not found");
            genreRepisitoryMock.Verify(x => x.Get(It.Is<Guid>(x => x == genreId),
               It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
