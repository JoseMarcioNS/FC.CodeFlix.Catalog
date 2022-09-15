using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Get;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Get
{
    [Collection(nameof(GetCategoryTestFixure))]
    public class GetCategoryTest
    {
        private readonly GetCategoryTestFixure _fixure;

        public GetCategoryTest(GetCategoryTestFixure fixure)
            => _fixure = fixure;

        [Fact(DisplayName = nameof(GetCategoryOK))]
        [Trait("Application", "GetCategoryTest - UseCases")]
        public async void GetCategoryOK()
        {

            var repositoryMock = _fixure.GetCategoryRepositoryMock();
            var category = _fixure.GetValidCategory();
            repositoryMock.Setup(c => c.Get(
              It.IsAny<Guid>(),
              It.IsAny<CancellationToken>()
              )).ReturnsAsync(category);
            var input = new GetCategoryInput(category.Id);
            var useCase = new GetCategory(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);
            repositoryMock.Verify(c => c.Get(
                           It.IsAny<Guid>(),
                           It.IsAny<CancellationToken>())
                       , Times.Once);

            output.Should().NotBeNull();
            output.Id.Should().Be(category.Id);
            output.Name.Should().Be(category.Name);
            output.Description.Should().Be(category.Description);
            output.IsActive.Should().Be(category.IsActive);
            output.CreatedAt.Should().Be(category.CreatedAt);
        }
        [Fact(DisplayName = nameof(ThrowExceptionWhenNotFoundCategory))]
        [Trait("Application", "GetCategoryTest - UseCases")]
        public async void ThrowExceptionWhenNotFoundCategory()
        {
            var repositoryMock = _fixure.GetCategoryRepositoryMock();
            var id = Guid.NewGuid();
            repositoryMock.Setup(x => x.Get(
                 It.IsAny<Guid>()
                , It.IsAny<CancellationToken>()
                )).ThrowsAsync(new NotFoundException($"Category '{id}' not found"));

            var input = new GetCategoryInput(id);
            var useCase = new GetCategory(repositoryMock.Object);

            var tast = async () => await useCase.Handle(input, CancellationToken.None);

            await tast.Should()
                      .ThrowAsync<NotFoundException>()
                      .WithMessage($"Category '{id}' not found");

            repositoryMock.Verify(c => c.Get(
                           It.IsAny<Guid>(),
                           It.IsAny<CancellationToken>())
                       , Times.Once);

        }
    }
}
