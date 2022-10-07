using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Delete;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Get;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Delete
{
    [Collection(nameof(DeleteGenreTestFixture))]
    public class DeleteGenreTest
    {
        private readonly DeleteGenreTestFixture _fixture;

        public DeleteGenreTest(DeleteGenreTestFixture fixture) => _fixture = fixture;

        [Fact(DisplayName = nameof(Delete))]
        [Trait("Applcation", "DeleteGenre - Usecases")]
        public async Task Delete()
        {
            var genreRepisitoryMock = _fixture.GetGenreRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenreWithCategories();
            genreRepisitoryMock.Setup(x => x.Get(
                It.Is<Guid>(x => x == genre.Id),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(genre);
            var input = new DeleteGenreInput(genre.Id);
            var useCase = new DeleteGenre(genreRepisitoryMock.Object, unitOfWorkMock.Object);

            await useCase.Handle(input, CancellationToken.None);

           
            genreRepisitoryMock.Verify(x => x.Get(It.Is<Guid>(x => x == genre.Id),
                It.IsAny<CancellationToken>()), Times.Once);
            genreRepisitoryMock.Verify(x => x.Delete(It.Is<DomainEntity.Genre>(x => x == genre),
                It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x=> x.Commit(It.IsAny<CancellationToken>()));
        }
        [Fact(DisplayName = nameof(ThrowExceptionWhenNotFound))]
        [Trait("Applcation", "DeleteGenre - Usecases")]
        public async Task ThrowExceptionWhenNotFound()
        {
            var genreRepisitoryMock = _fixture.GetGenreRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genreId = Guid.NewGuid();
            genreRepisitoryMock.Setup(x => x.Get(
                It.Is<Guid>(x => x == genreId),
                It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Genre '{genreId}' not found"));
            var input = new DeleteGenreInput(genreId);
            var useCase = new DeleteGenre(genreRepisitoryMock.Object, unitOfWorkMock.Object);

            var action = async () => await useCase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<NotFoundException>()
                  .WithMessage($"Genre '{genreId}' not found");
            genreRepisitoryMock.Verify(x => x.Get(It.Is<Guid>(x => x == genreId),
               It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
