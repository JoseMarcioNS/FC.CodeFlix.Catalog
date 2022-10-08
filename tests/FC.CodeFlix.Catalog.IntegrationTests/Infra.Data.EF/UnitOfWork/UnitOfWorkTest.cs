namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.UnitOfWork
{
    [Collection(nameof(UnitOfWorkTestFixture))]
    public class UnitOfWorkTest : IDisposable
    {
        private readonly UnitOfWorkTestFixture _fixture;

        public UnitOfWorkTest(UnitOfWorkTestFixture fixture)
         => _fixture = fixture;

        [Fact(DisplayName = nameof(Commit))]
        [Trait("Integration/Infra.Data", "UnitOfWorkTest - Persistence")]
        public async Task Commit()
        {
            var repository = _fixture.CreateDbContext();
            var categories = _fixture.CategoryBaseFixture.GetListCategories();
            await repository.AddRangeAsync(categories);
            var unitOfwork = new UnitOfWorkInfra.UnitOfWork(repository);

            await unitOfwork.Commit(CancellationToken.None);

            var categoryResults = repository.Categories.AsNoTracking().ToList();
            categoryResults.Should().NotBeNull();
            categoryResults.Should().HaveCount(categories.Count);
        }
        [Fact(DisplayName = nameof(Rollback))]
        [Trait("Integration/Infra.Data", "UnitOfWorkTest - Persistence")]
        public async Task Rollback()
        {
            var repository = _fixture.CreateDbContext();
            var categories = _fixture.CategoryBaseFixture.GetListCategories();
            await repository.AddRangeAsync(categories);
            var unitOfwork = new UnitOfWorkInfra.UnitOfWork(repository);

            Func<Task> task = async () => await unitOfwork.Rollback(CancellationToken.None);

            await task.Should().NotThrowAsync();

        }
        public void Dispose()
            => _fixture.CleanInMemoryDatabase();
    }
}
