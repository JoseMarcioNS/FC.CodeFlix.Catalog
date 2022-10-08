using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;


namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Create
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest : IDisposable
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
         => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("Integration/Appication", "UseCases - Category")]
        public async void CreateCategory()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var useCase = new CreateCategory(repository, unitOfWork);
            var input = _fixture.GetCreateCategoryInput();

            var output = await useCase.Handle(input, CancellationToken.None);

            var outputBd = await context.Categories.FindAsync(output.Id);
            outputBd.Should().NotBeNull();
            outputBd!.Id.Should().Be(outputBd.Id);
            outputBd.Name.Should().Be(output.Name);
            outputBd.Description.Should().Be(output.Description);
            outputBd.IsActive.Should().Be(output.IsActive);
            outputBd.CreatedAt.Should().NotBeSameDateAs(default);

            output.Should().NotBeNull();
            output.Id.Should().NotBeEmpty();
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.CreatedAt.Should().NotBeSameDateAs(default);

        }
        [Theory(DisplayName = nameof(ThrowWhenCreateCategaryFail))]
        [Trait("Integration/Appication", "UseCases - Category")]
        [MemberData(nameof(CreateCategoryTestGeneretorData.GetInvalidInputs),
                   parameters: 10,
                   MemberType = typeof(CreateCategoryTestGeneretorData)
                   )]
        public async void ThrowWhenCreateCategaryFail(CreateCategoryInput input, string exeptionMessage)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var useCase = new CreateCategory(repository, unitOfWork);

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(exeptionMessage);

            var categoriesList = context.Categories.AsNoTracking().ToList();
            categoriesList.Should().HaveCount(0);
        }
        public void Dispose()
         => _fixture.CommonFixture.CleanInMemoryDatabase();

    }
}
