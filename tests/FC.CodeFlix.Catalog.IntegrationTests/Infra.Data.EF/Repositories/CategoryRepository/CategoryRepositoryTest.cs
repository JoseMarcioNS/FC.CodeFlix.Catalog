using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.CategoryRepository
{
    [Collection(nameof(CategoryRepositoryTestFixture))]
    public class CategoryRepositoryTest : IDisposable
    {
        private readonly CategoryRepositoryTestFixture _fixture;

        public CategoryRepositoryTest(CategoryRepositoryTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(Insert))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task Insert()
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var category = _fixture.GetValidCategory();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await categoryRepository.Insert(category, CancellationToken.None);
            await codeFlixCatalogDbContext.SaveChangesAsync();

            var dbCategory = await codeFlixCatalogDbContext.Categories
                                  .AsNoTracking().
                                  FirstAsync(x => x.Id == category.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(category.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.IsActive.Should().Be(category.IsActive);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);

        }
        [Theory(DisplayName = nameof(Get))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Get(int index)
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var category = categories[index];
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();

            var dbCategory = await categoryRepository.Get(category.Id, CancellationToken.None);

            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(category.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.IsActive.Should().Be(category.IsActive);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);

        }
        [Fact(DisplayName = nameof(GetThowExceptionWhenNotFound))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task GetThowExceptionWhenNotFound()
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var category = _fixture.GetValidCategory();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();

            Func<Task> task = async () => await categoryRepository.Get(category.Id, CancellationToken.None);

            await task.Should()
                      .ThrowAsync<NotFoundException>()
                      .WithMessage($"Category '{category.Id}' not found.");

        }
        [Theory(DisplayName = nameof(Update))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public async Task Update(int index)
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var category = categories[index];
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            category.Update(_fixture.GetValidName(), _fixture.GetValidDescription());

            await categoryRepository.Update(category, CancellationToken.None);
            await codeFlixCatalogDbContext.SaveChangesAsync();

            var dbCategory = await codeFlixCatalogDbContext.Categories
                                             .AsNoTracking()
                                             .FirstAsync(x => x.Id == category.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(category.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.IsActive.Should().Be(category.IsActive);
            dbCategory.CreatedAt.Should().Be(category.CreatedAt);

        }
        [Fact(DisplayName = nameof(Delete))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task Delete()
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var category = _fixture.GetValidCategory();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await categoryRepository.Insert(category, CancellationToken.None);
            await codeFlixCatalogDbContext.SaveChangesAsync();


            await categoryRepository.Delete(category, CancellationToken.None);
            await codeFlixCatalogDbContext.SaveChangesAsync();

            var dbCategory = await codeFlixCatalogDbContext.Categories
                                  .AsNoTracking().
                                  FirstOrDefaultAsync(x => x.Id == category.Id);

            dbCategory.Should().BeNull();

        }
        [Fact(DisplayName = nameof(SearchResultsAndTotal))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task SearchResultsAndTotal()
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
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
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        public async Task SearchResultsIsEmpty()
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Total.Should().Be(0);
            searchOutput.Items.Should().HaveCount(0);
        }
        [Theory(DisplayName = nameof(SearchResultsPaginated))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
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
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories(quantityCategoryGenerator);
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            var searchInput = new SearchInput(currentPage, perPage, "", "", SearchOrder.Asc);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

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
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
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
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
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
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            var searchInput = new SearchInput(currentPage, perPage, search, "", SearchOrder.Asc);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(currentPage);
            searchOutput.PerPage.Should().Be(perPage);
            searchOutput.Total.Should().Be(expectedQuantitytotalItems);
            searchOutput.Items.Should().HaveCount(expectedQuantityItemsReturned);
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
        [Theory(DisplayName = nameof(SearchResultsOrdered))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        [InlineData("name", "asc")]
        [InlineData("name", "desc")]
        [InlineData("id", "asc")]
        [InlineData("id", "desc")]
        [InlineData("", "asc")]
        public async Task SearchResultsOrdered(string orderBy, string order)
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var searchInput = new SearchInput(1, categories.Count, "", orderBy, searchOrder);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(1);
            searchOutput.PerPage.Should().Be(categories.Count);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(categories.Count);

            for (int i = 0; i < categoriesOrdered.Count; i++)
            {
                var categoryReturned = searchOutput.Items[i];
                var categoryOdered = categoriesOrdered[i];
                categoryReturned.Id.Should().Be(categoryOdered.Id);
                categoryReturned!.Name.Should().Be(categoryOdered.Name);
                categoryReturned.Description.Should().Be(categoryOdered.Description);
                categoryReturned.IsActive.Should().Be(categoryOdered.IsActive);
                
            }
        }
        [Theory(DisplayName = nameof(SearchResultsOrderedByDate))]
        [Trait("Integration/Infra.Data", "CategoryRepository - Repositories")]
        [InlineData("createdAt", "asc")]
        [InlineData("createdAt", "desc")]
         public async Task SearchResultsOrderedByDate(string orderBy, string order)
        {
            var codeFlixCatalogDbContext = _fixture.CreateDbContext();
            var categories = _fixture.GetListCategories();
            var categoryRepository = new Repository.CategoryRepository(codeFlixCatalogDbContext);
            await codeFlixCatalogDbContext.AddRangeAsync(categories);
            await codeFlixCatalogDbContext.SaveChangesAsync();
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var searchInput = new SearchInput(1, categories.Count, "", orderBy, searchOrder);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            var categoriesOrdered = _fixture.CloneCategoriesOrdered(categories, orderBy, searchOrder);
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(1);
            searchOutput.PerPage.Should().Be(categories.Count);
            searchOutput.Total.Should().Be(categories.Count);
            searchOutput.Items.Should().HaveCount(categories.Count);
            DateTime? LastDate = null;
            for (int i = 0; i < categoriesOrdered.Count; i++)
            {
                var categoryReturned = searchOutput.Items[i];
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
