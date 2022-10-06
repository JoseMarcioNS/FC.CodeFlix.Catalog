using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Update;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Update
{
    [Collection(nameof(UpdateGenreTestFixture))]
    public class UpdateGenreTest
    {
        private readonly UpdateGenreTestFixture _fixture;

        public UpdateGenreTest(UpdateGenreTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Update))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        public async Task Update()
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenre();
            genreRepositoryMock.Setup(x => x.Get(
               It.Is<Guid>(x => x == genre.Id),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(genre);
            var input = new UpdateGenreInput(
                genre.Id,
                _fixture.GetValidName(),
                _fixture.GetRandomActive()
                );
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            GenreModelOutput output = await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(input.IsActive!.Value);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            output.CategoriesIds.Should().HaveCount(0);
            genreRepositoryMock.Verify(x => x.Update(
                It.Is<DomainEntity.Genre>(x => x == genre),
                It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(x => x.Commit(
               It.IsAny<CancellationToken>()));
        }
        [Fact(DisplayName = nameof(ThrowExceptionWhenNotFound))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        public async Task ThrowExceptionWhenNotFound()
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var categoryId = Guid.NewGuid();
            genreRepositoryMock.Setup(x =>
            x.Get(It.IsAny<Guid>(),
            It.IsAny<CancellationToken>())
            ).ThrowsAsync(new NotFoundException($"Genre '{categoryId}' not found'"));

            var input = new UpdateGenreInput(
                 categoryId,
                _fixture.GetValidName(),
                _fixture.GetRandomActive()
                );
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            var action = async () => await usecase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<NotFoundException>()
                 .WithMessage($"Genre '{categoryId}' not found'");
        }
        [Theory(DisplayName = nameof(ThrowExceptionWhenInvalidName))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task ThrowExceptionWhenInvalidName(string? name)
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenre();
            genreRepositoryMock.Setup(x =>
            x.Get(It.Is<Guid>(x => x == genre.Id),
            It.IsAny<CancellationToken>())
            ).ReturnsAsync(genre);

            var input = new UpdateGenreInput(
                 genre.Id,
                 name!,
                _fixture.GetRandomActive()
                );
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            var action = async () => await usecase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<EntityValidationException>()
                 .WithMessage($"Name should not be null or empty");
        }
        [Theory(DisplayName = nameof(UpdateOptionalActive))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        [InlineData(true)]
        [InlineData(false)]
        [InlineData(null)]
        public async Task UpdateOptionalActive(bool? isActive)
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenre();
            genreRepositoryMock.Setup(x =>
            x.Get(It.Is<Guid>(x => x == genre.Id),
            It.IsAny<CancellationToken>())
            ).ReturnsAsync(genre);

            var input = new UpdateGenreInput(
                 genre.Id,
                 _fixture.GetValidName(),
                 isActive
                );
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            GenreModelOutput output = await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(isActive == null ? genre.IsActive : input.IsActive!.Value);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            output.CategoriesIds.Should().HaveCount(0);
            genreRepositoryMock.Verify(x => x.Update(
                It.Is<DomainEntity.Genre>(x => x == genre),
                It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(x => x.Commit(
               It.IsAny<CancellationToken>()));
        }
        [Fact(DisplayName = nameof(UpdateAddRelatedCategories))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        public async Task UpdateAddRelatedCategories()
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenre();
            var categoriesIds = _fixture.GetRandomCategoriesIs();
            categoryRepositoryMock.Setup(x => x.GetCategoriesByIds(
              It.IsAny<List<Guid>>(),
              It.IsAny<CancellationToken>()
              )).ReturnsAsync(categoriesIds);
            genreRepositoryMock.Setup(x => x.Get(
               It.Is<Guid>(x => x == genre.Id),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(genre);
            var input = new UpdateGenreInput(
                genre.Id,
                _fixture.GetValidName(),
                _fixture.GetRandomActive(),
                categoriesIds
                );
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            GenreModelOutput output = await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(input.IsActive!.Value);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            output.CategoriesIds.Should().HaveCount(categoriesIds.Count);
            categoriesIds.ForEach(x => output.CategoriesIds.Should().Contain(x));
            genreRepositoryMock.Verify(x => x.Update(
                It.Is<DomainEntity.Genre>(x => x == genre),
                It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(x => x.Commit(
               It.IsAny<CancellationToken>()));
        }
        [Fact(DisplayName = nameof(UpdateGenreWithoutCategories))]
        [Trait("Applcation", "UpdateGenre - Usecases")]
        public async Task UpdateGenreWithoutCategories()
        {
           
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var genre = _fixture.GetGenreWithCategories();

           genreRepositoryMock.Setup(x => x.Get(
               It.Is<Guid>(x => x == genre.Id),
               It.IsAny<CancellationToken>()
               )).ReturnsAsync(genre);
            var input = new UpdateGenreInput(
                genre.Id,
                _fixture.GetValidName(),
                _fixture.GetRandomActive());
            var usecase = new UpdateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            var output =  await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(input.IsActive!.Value);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            output.CategoriesIds.Should().HaveCount(genre.Categories.Count);
           
            genreRepositoryMock.Verify(x => x.Update(
                It.Is<DomainEntity.Genre>(x => x == genre),
                It.IsAny<CancellationToken>()));
            unitOfWorkMock.Verify(x => x.Commit(
               It.IsAny<CancellationToken>()));
        }
    }
}
