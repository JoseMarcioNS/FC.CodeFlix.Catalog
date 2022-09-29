using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.End2EndTests.Extentions;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.List
{
    [Collection(nameof(ListCategoryTestFixture))]
    public class ListCategoryTest : IDisposable
    {
        private readonly ListCategoryTestFixture _fixture;
        private readonly ITestOutputHelper _outputHelper;

        public ListCategoryTest(ListCategoryTestFixture fixture, ITestOutputHelper outputHelper)
        => (_fixture, _outputHelper) = (fixture, outputHelper);

        [Fact(DisplayName = (nameof(SearchResultsAndTotalByDefault)))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        public async Task SearchResultsAndTotalByDefault()
        {
            var categories = _fixture.GetListCategories(15);
            await _fixture.Persistence.InsertCategories(categories);
            var input = new ListCategoriesInput();//Default

            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
                "/categories");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(input.Page);
            output.PerPage.Should().Be(input.PerPage);
            output.Items.Should().HaveCount(categories.Count);
            output.Total.Should().Be(categories.Count);
            foreach (var outputItem in output.Items)
            {
                var categoryItem = categories.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(categoryItem!.Name);
                outputItem.Description.Should().Be(categoryItem.Description);
                outputItem.IsActive.Should().Be(categoryItem.IsActive);
            }
        }
        [Fact(DisplayName = (nameof(SearchResultsIsEmpty)))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        public async Task SearchResultsIsEmpty()
        {
            var input = new ListCategoriesInput();//Default

            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
               "/categories");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(input.Page);
            output.PerPage.Should().Be(input.PerPage);
            output.Items.Should().HaveCount(0);
            output.Total.Should().Be(0);

        }
        [Theory(DisplayName = (nameof(SearchResultsPaginated)))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        [InlineData(15, 1, 5, 5)]
        [InlineData(15, 2, 5, 5)]
        [InlineData(15, 3, 6, 3)]
        public async Task SearchResultsPaginated(
            int quantityCategoryGenerator,
            int currentPage,
            int perPage,
            int expectedQuantityItems
            )
        {

            var categories = _fixture.GetListCategories(quantityCategoryGenerator);
            await _fixture.Persistence.InsertCategories(categories);
            var input = new ListCategoriesInput(currentPage, perPage, "", "", SearchOrder.Asc);


            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
                 $"/categories", input);


            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(input.Page);
            output.PerPage.Should().Be(input.PerPage);
            output.Items.Should().HaveCount(expectedQuantityItems);
            output.Total.Should().Be(categories.Count);
            foreach (var outputItem in output.Items)
            {
                var categoryItem = categories.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(categoryItem!.Name);
                outputItem.Description.Should().Be(categoryItem.Description);
                outputItem.IsActive.Should().Be(categoryItem.IsActive);
            }

        }
        [Theory(DisplayName = nameof(SearchResultsByText))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        [InlineData("Action", 1, 5, 1, 1)]
        [InlineData("Horror", 1, 5, 2, 2)]
        [InlineData("Sci-fi", 1, 5, 3, 3)]
        [InlineData("Sci-fi", 2, 5, 0, 3)]
        [InlineData("War", 1, 5, 0, 0)]
        public async Task SearchResultsByText(
           string search,
           int currentPage,
           int perPage,
           int expectedQuantityItemsReturned,
           int expectedQuantitytotalItems
           )
        {

            var categories = _fixture.CreateCategoriesWithNames(
                new string[] {
                    "Action",
                    "Horror",
                    "Horror - Based on Real Facts",
                    "Drama",
                    "Comedy",
                    "Sci-fi - Space",
                    "Sci-fi - IA",
                    "Sci-fi - Robots"
                }
            );
            await _fixture.Persistence.InsertCategories(categories);
            var input = new ListCategoriesInput(currentPage, perPage, search, "", SearchOrder.Asc);


            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
                $"/categories", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(currentPage);
            output.PerPage.Should().Be(perPage);
            output.Total.Should().Be(expectedQuantitytotalItems);
            output.Items.Should().HaveCount(expectedQuantityItemsReturned);
            output.Items.ToList().ForEach(categoryItem =>
            {
                var category = categories.Find(x => x.Id == categoryItem.Id);
                category.Should().NotBeNull();
                category!.Name.Should().Be(categoryItem.Name);
                category.Description.Should().Be(categoryItem.Description);
                category.IsActive.Should().Be(categoryItem.IsActive);
                category.CreatedAt.TrimMilliseconds().Should().Be(categoryItem.CreatedAt.TrimMilliseconds());
            });

        }
        [Theory(DisplayName = nameof(SearchResultsOrdered))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        [InlineData("name", "asc")]
        [InlineData("name", "desc")]
        [InlineData("id", "asc")]
        [InlineData("id", "desc")]
        [InlineData("", "asc")]
        public async Task SearchResultsOrdered(string orderBy, string order)
        {

            var categories = _fixture.GetListCategories();
            await _fixture.Persistence.InsertCategories(categories);
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var input = new ListCategoriesInput(1, categories.Count, "", orderBy, searchOrder);


            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
                $"/categories", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(1);
            output.PerPage.Should().Be(categories.Count);
            output.Total.Should().Be(categories.Count);
            output.Items.Should().HaveCount(categories.Count);
            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            WriteDetailSummary(output, categoriesOrdered);
            for (int i = 0; i < output.Items.Count; i++)
            {
                var categoryReturned = output.Items[i];
                var categoryOdered = categoriesOrdered[i];
                categoryReturned.Id.Should().Be(categoryOdered.Id);
                categoryReturned!.Name.Should().Be(categoryOdered.Name);
                categoryReturned.Description.Should().Be(categoryOdered.Description);
                categoryReturned.IsActive.Should().Be(categoryOdered.IsActive);

            }
        }
        [Theory(DisplayName = nameof(SearchResultsOrderedByDate))]
        [Trait("End2End/Api", "Category/List - Endpoints")]
        [InlineData("createdAt", "asc")]
        [InlineData("createdAt", "desc")]

        public async Task SearchResultsOrderedByDate(string orderBy, string order)
        {

            var categories = _fixture.GetListCategories();
            await _fixture.Persistence.InsertCategories(categories);
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var input = new ListCategoriesInput(1, categories.Count, "", orderBy, searchOrder);


            var (response, output) = await _fixture.ApiClient.Get<ListCategoriesOutput>(
                $"/categories", input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.CurrentPage.Should().Be(1);
            output.PerPage.Should().Be(categories.Count);
            output.Total.Should().Be(categories.Count);
            output.Items.Should().HaveCount(categories.Count);
            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            WriteDetailSummary(output, categoriesOrdered);
            DateTime? LastDate = null;
            for (int i = 0; i < output.Items.Count; i++)
            {
                var categoryReturned = output.Items[i];
                var categoryOdered = categoriesOrdered[i];
                categoryReturned.Id.Should().Be(categoryOdered.Id);
                categoryReturned!.Name.Should().Be(categoryOdered.Name);
                categoryReturned.Description.Should().Be(categoryOdered.Description);
                categoryReturned.IsActive.Should().Be(categoryOdered.IsActive);
                if (LastDate is not null)
                {
                    if (searchOrder == SearchOrder.Asc)
                        Assert.True(categoryReturned.CreatedAt >= LastDate);
                    else
                        Assert.True(categoryReturned.CreatedAt <= LastDate);
                }
                LastDate = categoryReturned.CreatedAt;
            }
        }

        private void WriteDetailSummary(ListCategoriesOutput? output, List<DomainEntity.Category> categoriesOrdered)
        {
            var count = 0;
            var expectedList = categoriesOrdered.Select(x => $"{++count} {x.Name} {x.CreatedAt} {JsonConvert.SerializeObject(x)}");
            count = 0;
            var itemsList = output!.Items.Select(x => $"{++count} {x.Name} {x.CreatedAt} {JsonConvert.SerializeObject(x)}");

            _outputHelper.WriteLine("Expecteds...");
            _outputHelper.WriteLine(String.Join('\n', expectedList));
            _outputHelper.WriteLine("Outputs...");
            _outputHelper.WriteLine(String.Join('\n', itemsList));
        }

        public void Dispose()
         => _fixture.CleanDatabase();
    }
}
