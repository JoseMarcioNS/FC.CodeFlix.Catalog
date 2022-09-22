﻿using FC.CodeFlix.Catalog.Application.UseCases.Category.Delete;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Delete
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest : IDisposable
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteCategoryOk))]
        [Trait("Integration/Application", "DeleteCategoryTest - UseCases")]
        public async void DeleteCategoryOk()
        {
            var context = _fixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var unitOfWork = new UnitOfWork(context);
            var category = _fixture.GetCategory();
            await repository.Insert(category, CancellationToken.None);
            await context.SaveChangesAsync();
            var input = new DeleteCategoryInput(category.Id);
            var useCase = new DeleteCategory(repository, unitOfWork);

            await useCase.Handle(input, CancellationToken.None);

            var output = await context.Categories.FindAsync(category.Id);
            output.Should().BeNull();
        }

        public void Dispose()
        => _fixture.CleanInMemoryDatabase();
    }
}