using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Update
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTest : IDisposable
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
       => _fixture = fixture;

        [Theory(DisplayName = nameof(UpdateCategoryOk))]
        [Trait("Integration/Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestGenerationData))
        ]
        public async void UpdateCategoryOk(DomainEntity.Category category, UpdateCategoryInput input)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var useCase = new UpdateCategory(repository, unitOfWork);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive!.Value);

        }
        [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingIsAcitve))]
        [Trait("Integration/Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
          parameters: 10,
          MemberType = typeof(UpdateCategoryTestGenerationData))]
        public async void UpdateCategoryWithoutProvidingIsAcitve(
            DomainEntity.Category category,
            UpdateCategoryInput exampleCategory)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var input = new UpdateCategoryInput(
                exampleCategory.Id,
                exampleCategory.Name,
                exampleCategory.Description
                );
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var useCase = new UpdateCategory(repository, unitOfWork);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(category.IsActive);

        }
        [Theory(DisplayName = nameof(UpdateCategoryProvidingOnlyName))]
        [Trait("Integration/Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
          parameters: 10,
          MemberType = typeof(UpdateCategoryTestGenerationData))]
        public async void UpdateCategoryProvidingOnlyName(DomainEntity.Category category, UpdateCategoryInput exampleCategory)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var input = new UpdateCategoryInput(
                exampleCategory.Id,
                exampleCategory.Name
                );
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var useCase = new UpdateCategory(repository, unitOfWork);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(category.Description);
            output.IsActive.Should().Be(category.IsActive);
        }
        [Theory(DisplayName = nameof(ThrowExcepitonWhenInvalidInputs))]
        [Trait("Integration/Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetInvalidInputs),
           parameters: 10,
           MemberType = typeof(UpdateCategoryTestGenerationData))
       ]
        public async void ThrowExcepitonWhenInvalidInputs(
           DomainEntity.Category category,
           UpdateCategoryInput input, string ErrorMessage)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            var useCase = new UpdateCategory(repository, unitOfWork);

            Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<EntityValidationException>().WithMessage(ErrorMessage);

        }
        [Fact(DisplayName = nameof(ThrowExceptioWhenNotFoundCategory))]
        [Trait("Integration/Application", "UpdateCategoryTest - UseCaes")]
        public async void ThrowExceptioWhenNotFoundCategory()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var input = _fixture.GetUpdateCategoryInput();
            var useCase = new UpdateCategory(repository, unitOfWork);

            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Category '{input.Id}' not found.");

        }
        public void Dispose()
        => _fixture.CommonFixture.CleanInMemoryDatabase();
    }
}
