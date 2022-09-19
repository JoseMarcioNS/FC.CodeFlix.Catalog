using FC.CodeFlix.Catalog.Application.UseCases.Category.List;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.List
{
    public class ListCategoriesTestGeneratorData
    {
        public static IEnumerable<object[]> GetListCategoriesInputWithDiferentsParameters(int times)
        {
            var fixture = new ListCategoriesTestFixture();
            var listCategoriesInput = fixture.GetListCategoriesInput();
            for (int i = 0; i < times; i++)
            {
                switch (i % 7)
                {
                    case 0:
                        yield return new object[] { new ListCategoriesInput() };
                        break;
                    case 1:
                        yield return new object[] { new ListCategoriesInput(
                            listCategoriesInput.Page) };
                        break;
                    case 2:
                        yield return new object[] { new ListCategoriesInput(
                            listCategoriesInput.Page,
                            listCategoriesInput.PerPage) };
                        break;
                    case 3:
                        yield return new object[] { new ListCategoriesInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search) };
                        break;
                    case 4:
                        yield return new object[] { new ListCategoriesInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search,
                             listCategoriesInput.Sort) };
                        break;
                    case 5:
                        yield return new object[] { new ListCategoriesInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search,
                             listCategoriesInput.Sort,
                             listCategoriesInput.Dir) };
                        break;
                    default:
                        yield return new object[] { new ListCategoriesInput() };
                        break;
                }
            }
        }
    }
}
