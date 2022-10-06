using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Create;
using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.Create
{
    [Collection(nameof(CreateGenreTestFixture))]
    public class CreateGenreTest
    {
        private readonly CreateGenreTestFixture _fixture;

        public CreateGenreTest(CreateGenreTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(Create))]
        [Trait("Applcation", "Genre - Usecases")]
        public async Task Create()
        {
            var repositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var usecase = new CreateGenre(repositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);
            var input = _fixture.GetCreateGenreInput();

            var output = await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(input.IsActive);
            output.CreatedAt.Should().NotBeSameDateAs(default);
            output.CategoriesIds.Should().HaveCount(0);
            repositoryMock.Verify(x => x.Insert(
                It.IsAny<DomainEntity.Genre>(),
                It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                 It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact(DisplayName = nameof(CreateWithRelatedcategories))]
        [Trait("Applcation", "Genre - Usecases")]
        public async Task CreateWithRelatedcategories()
        {
            var input = _fixture.GetCreateGenreInputWithCategories();
            var repositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            categoryRepositoryMock.Setup(x => x.GetCategoriesByIds(
               It.IsAny<List<Guid>>(),
               It.IsAny<CancellationToken>())
           ).ReturnsAsync(input.CategoriesIds!);
            var usecase = new CreateGenre(repositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);


            var output = await usecase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Name.Should().Be(input.Name);
            output.IsActive.Should().Be(input.IsActive);
            output.CreatedAt.Should().NotBeSameDateAs(default);
            output.CategoriesIds.Should().HaveCount(input.CategoriesIds?.Count ?? 0);
            input.CategoriesIds?.ForEach(categoryId =>
                 output.CategoriesIds.Should().Contain(categoryId)
                 );
            categoryRepositoryMock.Verify(x => x.GetCategoriesByIds(
                         It.IsAny<List<Guid>>(),
                         It.IsAny<CancellationToken>()), Times.Once);
            repositoryMock.Verify(x => x.Insert(
                           It.IsAny<DomainEntity.Genre>(),
                           It.IsAny<CancellationToken>()), Times.Once);
            unitOfWorkMock.Verify(x => x.Commit(
                 It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact(DisplayName = nameof(ThrowExceptionWnenRelatedCategoryNotFound))]
        [Trait("Applcation", "Genre - Usecases")]
        public async Task ThrowExceptionWnenRelatedCategoryNotFound()
        {
            var input = _fixture.GetCreateGenreInputWithCategories();
            var categoryId = input.CategoriesIds!.Last();
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            categoryRepositoryMock.Setup(x => x.GetCategoriesByIds(
                It.IsAny<List<Guid>>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(input.CategoriesIds!.FindAll(x => x != categoryId));


            var usecase = new CreateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);

            var action = async () => await usecase.Handle(input, CancellationToken.None);
            await action.Should().ThrowAsync<RelatedAggregateException>()
                 .WithMessage($"Related category id(s) not found: {categoryId}");
            categoryRepositoryMock.Verify(x => x.GetCategoriesByIds(
                          It.IsAny<List<Guid>>(),
                          It.IsAny<CancellationToken>()), Times.Once);
        }
        [Theory(DisplayName = nameof(ThrowExceptionWnenInvaldName))]
        [Trait("Applcation", "Genre - Usecases")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task ThrowExceptionWnenInvaldName(string? name)
        {
            var input = _fixture.GetCreateGenreInput(name);
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoryRepositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();

            var usecase = new CreateGenre(genreRepositoryMock.Object, categoryRepositoryMock.Object, unitOfWorkMock.Object);


            var action = async () => await usecase.Handle(input, CancellationToken.None);

            await action.Should().ThrowAsync<EntityValidationException>()
                 .WithMessage($"Name should not be null or empty");
           
        }
    }
}
