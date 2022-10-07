using FC.CodeFlix.Catalog.Application.UseCases.Genre.List;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.List
{
    public class ListGenresTestGeneratorData
    {
        public static IEnumerable<object[]> GetListGeresInputWithDiferentsParameters(int times)
        {
            var fixture = new ListGenresTestFixture();
            var listCategoriesInput = fixture.GetListGenresInput();
            for (int i = 0; i < times; i++)
            {
                switch (i % 7)
                {
                    case 0:
                        yield return new object[] { new ListGenresInput() };
                        break;
                    case 1:
                        yield return new object[] { new ListGenresInput(
                            listCategoriesInput.Page) };
                        break;
                    case 2:
                        yield return new object[] { new ListGenresInput(
                            listCategoriesInput.Page,
                            listCategoriesInput.PerPage) };
                        break;
                    case 3:
                        yield return new object[] { new ListGenresInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search) };
                        break;
                    case 4:
                        yield return new object[] { new ListGenresInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search,
                             listCategoriesInput.Sort) };
                        break;
                    case 5:
                        yield return new object[] { new ListGenresInput(
                             listCategoriesInput.Page,
                             listCategoriesInput.PerPage,
                             listCategoriesInput.Search,
                             listCategoriesInput.Sort,
                             listCategoriesInput.Dir) };
                        break;
                    default:
                        yield return new object[] { new ListGenresInput() };
                        break;
                }
            }
        }
    }
}
