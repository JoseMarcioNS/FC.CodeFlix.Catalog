namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Create
{
    public class CreateCategoryTestGeneretorData
    {
        public static IEnumerable<object[]> GetInvalidInputs()
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputs = new List<object[]>();
            var switchCase = 1;
            var totalInvalidCases = 4;
            for (int i = 0; i < totalInvalidCases; i++)
            {
                switch (switchCase)
                {
                    case 1:
                        var inputNameMinLeght = fixture.GetCategoryInputInvalidNameMinLenght();
                        invalidInputs.Add(new object[]{inputNameMinLeght,"Name should not be less than 3 characters long"
                        });
                        break;
                    case 2:
                        var inputNameMaxLeght = fixture.GetCategoryInvalidNameMaxLeght();
                        invalidInputs.Add(new object[] { inputNameMaxLeght, "Name should not be greater than 255 characters long" });
                        break;
                    case 3:
                        var inputDEscriptionMaxLeght = fixture.GetCategoryInputInvalidDescriptionMaxLeght();
                        invalidInputs.Add(new object[] { inputDEscriptionMaxLeght, "Description should not be greater than 10000 characters long" });

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

