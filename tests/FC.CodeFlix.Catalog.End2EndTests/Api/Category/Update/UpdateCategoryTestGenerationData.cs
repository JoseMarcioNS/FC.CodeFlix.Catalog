namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Update
{
    public class UpdateCategoryTestGenerationData
    {
        public static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new UpdateCategoryTestFixture();
            var invalidInputs = new List<object[]>();
            DomainEntity.Category category;
            var switchCase = 1;
            var totalInvalidCases = 3;
            for (int i = 0; i < totalInvalidCases; i++)
            {
                switch (switchCase)
                {
                    case 1:
                        category = fixture.GetValidCategory();
                        var inputNameMinLenght = fixture.GetInputInvalidNameMinLenght(category);
                        invalidInputs.Add(new object[]{category,inputNameMinLenght,"Name should not be less than 3 characters long"
                        });
                        break;
                    case 2:
                        category = fixture.GetValidCategory();
                        var inputNameMaxLeght = fixture.GetInputInvalidNameMaxLeght(category);
                        invalidInputs.Add(new object[] { category, inputNameMaxLeght, "Name should not be greater than 255 characters long" });
                        break;
                    case 3:
                        category = fixture.GetValidCategory();
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

