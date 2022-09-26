using FC.CodeFlix.Catalog.End2EndTests.Common;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common
{
    public class CategoryCommonFixture : BaseFixture
    {
        public CategoryPersistence Persistence;

        public CategoryCommonFixture() : base()
        {
            Persistence = new CategoryPersistence(CreateDbContext());
        }

    }
}
