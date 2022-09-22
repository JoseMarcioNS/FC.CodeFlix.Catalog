namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.Update
{
    public class UpdateCategoryTestGenerationData
    {
        public static IEnumerable<object[]> GetCategoriesUpdate(int times)
        {
            var fixture = new UpdateCategoryTestFixture();
            var listCategories = new List<object[]>();

            for (int i = 0; i < times; i++)
            {
                var category = fixture.GetCategory();
                var categoryUpdated = fixture.GetUpdateCategoryInput(category.Id);
                listCategories.Add(new object[] { category, categoryUpdated });
            }
            return listCategories;
        }
        public static IEnumerable<object[]> GetCategoriesUpdateInvalidInputs(int times)
        {
            var fixture = new UpdateCategoryTestFixture();
            var listCategories = new List<object[]>();

            for (int i = 0; i < times; i++)
            {
                var category = fixture.GetCategory();
                var categoryUpdated = fixture.GetUpdateCategoryInput(category.Id);
                listCategories.Add(new object[] { category, categoryUpdated });
            }
            return listCategories;
        }
    }
}
