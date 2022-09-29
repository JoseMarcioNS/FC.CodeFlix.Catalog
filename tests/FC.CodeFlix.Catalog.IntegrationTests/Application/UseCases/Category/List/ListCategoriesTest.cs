using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.Infra.Data.EF.Repositories;

namespace FC.CodeFlix.Catalog.IntegrationTests.Application.UseCases.Category.List
{
    [Collection(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTest : IDisposable
    {
        private readonly ListCategoriesTestFixture _fixture;

        public ListCategoriesTest(ListCategoriesTestFixture fixture)
       => _fixture = fixture;

        [Fact(DisplayName = nameof(SearchResultsAndTotal))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
        public async Task SearchResultsAndTotal()
        {
            var context = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories(10);
            var repository = new CategoryRepository(context);
            await context.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var input = new ListCategoriesInput(1, 20, "", "", SearchOrder.Asc);
            var useCase = new ListCategories(repository);

            var searchOutput = await useCase.Handle(input, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(input.Page);
            searchOutput.PerPage.Should().Be(input.PerPage);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(categories.Count);
            searchOutput.Items.ToList().ForEach(categoryItem =>
            {
                var category = categories.Find(x => x.Id == categoryItem.Id);
                category.Should().NotBeNull();
                category!.Name.Should().Be(categoryItem.Name);
                category.Description.Should().Be(categoryItem.Description);
                category.IsActive.Should().Be(categoryItem.IsActive);
                category.CreatedAt.Should().Be(categoryItem.CreatedAt);
            });

        }
        [Fact(DisplayName = nameof(SearchResultsIsEmpty))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
        public async Task SearchResultsIsEmpty()
        {
            var context = _fixture.CreateDbContext();
            var repository = new CategoryRepository(context);
            var input = new ListCategoriesInput(1, 20, "", "", SearchOrder.Asc);
            var useCase = new ListCategories(repository);

            var searchOutput = await useCase.Handle(input, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(input.Page);
            searchOutput.PerPage.Should().Be(input.PerPage);
            searchOutput.Total.Should().Be(0);
            searchOutput.Items.Should().HaveCount(0);
        }
        [Theory(DisplayName = nameof(SearchResultsPaginated))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
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
            var context = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories(quantityCategoryGenerator);
            var repository = new CategoryRepository(context);
            await context.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var input = new ListCategoriesInput(currentPage, perPage, "", "", SearchOrder.Asc);
            var useCase = new ListCategories(repository);

            var searchOutput = await useCase.Handle(input, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(currentPage);
            searchOutput.PerPage.Should().Be(perPage);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(expectedQuantityItems);
            searchOutput.Items.ToList().ForEach(categoryItem =>
            {
                var category = categories.Find(x => x.Id == categoryItem.Id);
                category.Should().NotBeNull();
                category!.Name.Should().Be(categoryItem.Name);
                category.Description.Should().Be(categoryItem.Description);
                category.IsActive.Should().Be(categoryItem.IsActive);
                category.CreatedAt.Should().Be(categoryItem.CreatedAt);
            });

        }
        [Theory(DisplayName = nameof(SearchResultsByText))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
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
            var context = _fixture.CreateDbContext();
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
            var repository = new CategoryRepository(context);
            await context.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var input = new ListCategoriesInput(currentPage, perPage, search, "", SearchOrder.Asc);
            var useCase = new ListCategories(repository);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(currentPage);
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
                category.CreatedAt.Should().Be(categoryItem.CreatedAt);
            });

        }
        [Theory(DisplayName = nameof(SearchResultsOrdered))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
        [InlineData("name", "asc")]
        [InlineData("name", "desc")]
        [InlineData("id", "asc")]
        [InlineData("id", "desc")]
        [InlineData("", "asc")]
        public async Task SearchResultsOrdered(string orderBy, string order)
        {
            var context = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var repository = new CategoryRepository(context);
            await context.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var input = new ListCategoriesInput(1, categories.Count, "", orderBy, searchOrder);
            var useCase = new ListCategories(repository);

            var output = await useCase.Handle(input, CancellationToken.None);

            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(1);
            output.PerPage.Should().Be(categories.Count);
            output.Total.Should().Be(categories.Count);
            output.Items.Should().HaveCount(categories.Count);

            for (int i = 0; i < categoriesOrdered.Count; i++)
            {
                var categoryReturned = output.Items[i];
                var categoryOdered = categoriesOrdered[i];
                categoryReturned.Id.Should().Be(categoryOdered.Id);
                categoryReturned!.Name.Should().Be(categoryOdered.Name);
                categoryReturned.Description.Should().Be(categoryOdered.Description);
                categoryReturned.IsActive.Should().Be(categoryOdered.IsActive);
                categoryReturned.CreatedAt.Should().Be(categoryOdered.CreatedAt);
            }
        }
        [Theory(DisplayName = nameof(SearchResultsOrderedByDate))]
        [Trait("Integration/Application", "CategoryRepository - UseCases")]
        [InlineData("createdAt", "asc")]
        [InlineData("createdAt", "desc")]
        
        public async Task SearchResultsOrderedByDate(string orderBy, string order)
        {
            var context = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var repository = new CategoryRepository(context);
            await context.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var input = new ListCategoriesInput(1, categories.Count, "", orderBy, searchOrder);
            var useCase = new ListCategories(repository);

            var output = await useCase.Handle(input, CancellationToken.None);

            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(1);
            output.PerPage.Should().Be(categories.Count);
            output.Total.Should().Be(categories.Count);
            output.Items.Should().HaveCount(categories.Count);
            DateTime? LastDate = null;
            for (int i = 0; i < categoriesOrdered.Count; i++)
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
        public void Dispose()
                  => _fixture.CleanInMemoryDatabase();
    }
}
