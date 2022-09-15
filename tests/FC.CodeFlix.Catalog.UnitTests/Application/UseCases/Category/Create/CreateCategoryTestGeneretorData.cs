namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Create
{
    public class CreateCategoryTestGeneretorData
    {
        public static IEnumerable<object[]> GetInvalidInputs(int times)
        {
            var fixture = new CreateCategoryTestFixture();
            var invalidInputs = new List<object[]>();
            var switchCase = 1;
            var totalInvalidCases = 5;
            for (int i = 0; i < times; i++)
            {
                switch (switchCase)
                {
                    case 1:
                        var inputNameNull = fixture.GetInputInvalidNameNull();
                        invalidInputs.Add(new object[] { inputNameNull, "Name should not be null or empty" });
                        break;
                    case 2:
                        var inputNameMinLeght = fixture.GetInputInvalidNameMinLenght();
                        invalidInputs.Add(new object[]{inputNameMinLeght,"Name should not be less than 3 characters long"
                        });
                        break;
                    case 3:
                        var inputNameMaxLeght = fixture.GetInputInvalidNameMaxLeght();
                        invalidInputs.Add(new object[] { inputNameMaxLeght, "Name should not be greater than 255 characters long" });
                        break;
                    case 4:
                        var inputDEscriptionNull = fixture.GetInputInvalidDescriptionNull();
                        invalidInputs.Add(new object[] { inputDEscriptionNull, "Description should not be null" });
                        break;
                    case 5:
                        var inputDEscriptionMaxLeght = fixture.GetInputInvalidDescriptionMaxLeght();
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

