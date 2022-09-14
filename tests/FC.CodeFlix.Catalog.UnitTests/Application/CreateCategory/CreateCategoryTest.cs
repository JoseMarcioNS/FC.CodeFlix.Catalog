using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Exeptions;
using useCase = FC.CodeFlix.Catalog.Application.UseCases.Category.Create;

namespace FC.CodeFlix.Catalog.UnitTests.Application.CreateCategory
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Appication", "UseCases - Category")]
        public async void CreateCategory()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new useCase.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);
            var input = _fixture.GetCreateCategoryInput();

            var output = await useCase.Handle(input, CancellationToken.None);
            repositoryMock.Verify(repository => repository.Insert(It.IsAny<Category>(), CancellationToken.None), Times.Once);
            unitOfWorkMock.Verify(ufw => ufw.Commit(CancellationToken.None), Times.Once);

            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.CreatedAt.Should().NotBeSameDateAs(default(DateTime));

        }
        [Theory(DisplayName = nameof(ThrowWhenCreateCategaryFail))]
        [Trait("Appication", "UseCases - Category")]
        [MemberData(nameof(CreateCategoryTestGeneretorData.GetInvalidInputs),
            parameters: 28,
            MemberType = typeof(CreateCategoryTestGeneretorData)
            )]
        public async void ThrowWhenCreateCategaryFail(CreateCategoryInput input, string exeptionMessage)
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var unitOfWorkMock = _fixture.GetUnitOfWorkMock();
            var useCase = new useCase.CreateCategory(repositoryMock.Object, unitOfWorkMock.Object);

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationExeption>().WithMessage(exeptionMessage);
        }
    }
}
