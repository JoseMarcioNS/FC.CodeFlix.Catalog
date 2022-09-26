using FC.CodeFlix.Catalog.SharedTests;


namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Category
{
    [CollectionDefinition(nameof(CategoryTestFixture))]
    public class CategoryTestFixtureCollection : ICollectionFixture<CategoryTestFixture> { }
    public class CategoryTestFixture : CategoryBaseFixture
    { }

   
}
