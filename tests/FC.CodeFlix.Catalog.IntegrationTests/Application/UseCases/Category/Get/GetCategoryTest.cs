using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Get;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Get
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryTest : IDisposable
    {
        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryTest(GetCategoryTestFixture fixture)
         => _fixture = fixture;

        [Fact(DisplayName = nameof(GetCategory))]
        [Trait("Integration/Application", "GetCategoryTest - UseCases")]
        public async Task GetCategory()
        {
            var context = _fixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var category = _fixture.GetValidCategory();
            await repository.Insert(category, CancellationToken.None);
            await context.SaveChangesAsync();
            var getCategoryInput = new GetCategoryInput(category.Id);
            var useCase = new GetCategory(repository);

            var getCategoryOut = await useCase.Handle(getCategoryInput, CancellationToken.None);

            getCategoryOut.Should().NotBeNull();
            getCategoryOut.Id.Should().Be(category.Id);
            getCategoryOut.Name.Should().Be(category.Name);
            getCategoryOut.Description.Should().Be(category.Description);
            getCategoryOut.IsActive.Should().Be(category.IsActive);
            getCategoryOut.CreatedAt.Should().Be(category.CreatedAt);
        }
        [Fact(DisplayName = nameof(ThrowExceptionWhenNotFoundCategory))]
        [Trait("Integration/Application", "GetCategoryTest - UseCases")]
        public async void ThrowExceptionWhenNotFoundCategory()
        {
            var context = _fixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var category = _fixture.GetValidCategory();
            await repository.Insert(category, CancellationToken.None);
            await context.SaveChangesAsync();
            var input = new GetCategoryInput(Guid.NewGuid());
            var useCase = new GetCategory(repository);

            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should()
                      .ThrowAsync<NotFoundException>()
                      .WithMessage($"Category '{input.Id}' not found.");

        }
        public void Dispose()
       => _fixture.CleanInMemoryDatabase();

    }
}
