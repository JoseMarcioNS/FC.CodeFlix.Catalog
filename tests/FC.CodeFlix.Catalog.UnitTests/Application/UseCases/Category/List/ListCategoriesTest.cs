using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.List
{
    [Collection(nameof(ListCategoriesTestFixture))]
    public class ListCategoriesTest
    {
        private readonly ListCategoriesTestFixture _fixture;

        public ListCategoriesTest(ListCategoriesTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(ListCategories))]
        [Trait("Application", "ListCategoriesTest - UseCases")]
        public async Task ListCategories()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var categoriesList = _fixture.ListCategories();
            var input = _fixture.GetListCategoriesInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Category>(
                 currentPage: input.Page,
                 perPage: input.PerPage,
                 items: categoriesList,
                 total: new Random().Next(70, 100));
            repositoryMock.Setup(x => x.Search(
                 It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                      ),
                 It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            output.Items.ToList().ForEach(outputItem =>
            {
                var reposiotryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(reposiotryCategory!.Name);
                outputItem.Description.Should().Be(reposiotryCategory.Description);
                outputItem.IsActive.Should().Be(reposiotryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(reposiotryCategory.CreatedAt);

            });
            repositoryMock.Verify(x => x.Search(
                      It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                    ),
                 It.IsAny<CancellationToken>()
                ), Times.Once);
        }
        [Fact(DisplayName = nameof(ListCategoriesOkWhenEmpty))]
        [Trait("Application", "ListCategoriesTest - UseCases")]
        public async Task ListCategoriesOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();

            var input = _fixture.GetListCategoriesInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Category>(
                 currentPage: input.Page,
                 perPage: input.PerPage,
                 items: new List<DomainEntity.Category>().AsReadOnly(),
                 total: 0);
            repositoryMock.Setup(x => x.Search(
                 It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                      ),
                 It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(0);
            output.Items.Should().HaveCount(0);
            repositoryMock.Verify(x => x.Search(
                     It.Is<SearchInput>(searchInput =>
                     searchInput.Page == input.Page
                     && searchInput.PerPage == input.PerPage
                     && searchInput.Search == input.Search
                     && searchInput.OrderBy == input.Sort
                     && searchInput.Oder == input.Dir
                   ),
                It.IsAny<CancellationToken>()
               ), Times.Once);
        }
        [Theory(DisplayName = nameof(ListCategoriesWithSomeParemeters))]
        [Trait("Application", "ListCategoriesTest - UseCases")]
        [MemberData(
            nameof(ListCategoriesTestGeneratorData.GetListCategoriesInputWithDiferentsParameters),
            parameters: 14,
            MemberType = typeof(ListCategoriesTestGeneratorData)
            )]
        public async Task ListCategoriesWithSomeParemeters(ListCategoriesInput input)
        {
            var repositoryMock = _fixture.GetCategoryRepositoryMock();
            var categoriesList = _fixture.ListCategories();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Category>(
               currentPage: input.Page,
               perPage: input.PerPage,
               items: categoriesList,
               total: 70);
            repositoryMock.Setup(x => x.Search(
                 It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                      ),
                 It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new ListCategories(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);
            output.Should().NotBeNull();
            output.CurrentPage.Should().Be(outputRepositorySearch.CurrentPage);
            output.PerPage.Should().Be(outputRepositorySearch.PerPage);
            output.Total.Should().Be(outputRepositorySearch.Total);
            output.Items.Should().HaveCount(outputRepositorySearch.Items.Count);
            output.Items.ToList().ForEach(outputItem =>
            {
                var reposiotryCategory = outputRepositorySearch.Items.FirstOrDefault(x => x.Id == outputItem.Id);
                outputItem.Should().NotBeNull();
                outputItem.Name.Should().Be(reposiotryCategory!.Name);
                outputItem.Description.Should().Be(reposiotryCategory.Description);
                outputItem.IsActive.Should().Be(reposiotryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(reposiotryCategory.CreatedAt);

            });
            repositoryMock.Verify(x => x.Search(
                      It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                    ),
                 It.IsAny<CancellationToken>()
                ), Times.Once);
        }
    }
}
