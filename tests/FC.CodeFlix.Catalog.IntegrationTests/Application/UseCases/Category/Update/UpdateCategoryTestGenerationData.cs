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
        public static IEnumerable<object[]> GetInvalidInputs(int times)
        {
            var fixture = new UpdateCategoryTestFixture();
            var invalidInputs = new List<object[]>();
            DomainEntity.Category category;
            var switchCase = 1;
            var totalInvalidCases = 4;
            for (int i = 0; i < times; i++)
            {
                switch (switchCase)
                {
                    case 1:
                        category = fixture.GetCategory();
                        var inputNameNull = fixture.GetInputInvalidNameNull(category);
                        invalidInputs.Add(new object[] { category, inputNameNull, "Name should not be null or empty" });
                        break;
                    case 2:
                        category = fixture.GetCategory();
                        var inputNameMinLeght = fixture.GetInputInvalidNameMinLenght(category);
                        invalidInputs.Add(new object[]{category,inputNameMinLeght,"Name should not be less than 3 characters long"
                        });
                        break;
                    case 3:
                        category = fixture.GetCategory();
                        var inputNameMaxLeght = fixture.GetInputInvalidNameMaxLeght(category);
                        invalidInputs.Add(new object[] { category, inputNameMaxLeght, "Name should not be greater than 255 characters long" });
                        break;
                    case 4:
                        category = fixture.GetCategory();
                        var inputDEscriptionMaxLeght = fixture.GetInputInvalidDescriptionMaxLeght(category);
                        invalidInputs.Add(new object[] { category, inputDEscriptionMaxLeght, "Description should not be greater than 10000 characters long" });
                        break;
                }
                if (switchCase == totalInvalidCases)
                    switchCase = 1;
                else
                    switchCase++;
            }
            return invalidInputs;
        }
    }
}
