namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    public class UpdateCategoryTestGenerationData
    {
        public static IEnumerable<object[]> GetCategoriesUpdate(int times)
        {
            var fixture = new UpdateGategoryTestFixture();
            var listCategories = new List<object[]>();

            for (int i = 0; i < times; i++)
            {
                var category = fixture.GetValidCategory();
                var categoryUpdated = fixture.GetCategory(category.Id);
                listCategories.Add(new object[] { category, categoryUpdated });
            }
            return listCategories;
        }
    }
}
