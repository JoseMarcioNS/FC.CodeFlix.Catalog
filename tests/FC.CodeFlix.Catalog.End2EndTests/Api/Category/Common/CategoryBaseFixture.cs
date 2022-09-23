using FC.CodeFlix.Catalog.End2EndTests.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common
{
    public class CategoryBaseFixture : BaseFixture
    {
        public CategoryPersistence Persistence;

        public CategoryBaseFixture() : base()
        {
            Persistence = new CategoryPersistence(CreateDbContext());
        }
    }
}
