using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.List;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.List;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Genre.List
{
    [Collection(nameof(ListGenresTestFixture))]
    public class ListGenresTest
    {
        private readonly ListGenresTestFixture _fixture;

        public ListGenresTest(ListGenresTestFixture fixture)
            => _fixture = fixture;
        [Fact(DisplayName = nameof(GetListGenres))]
        [Trait("Applcation", "ListGenres - Usecases")]
        public async Task GetListGenres()
        {
            var genreRepositoryMock = _fixture.GetGenreRepositoryMock();
            var categoriesList = _fixture.GetGenres();
            var input = _fixture.GetListGenresInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Genre>(
                 currentPage: input.Page,
                 perPage: input.PerPage,
                 items: categoriesList,
                 total: new Random().Next(70, 100));
            genreRepositoryMock.Setup(x => x.Search(
                 It.Is<SearchInput>(searchInput =>
                      searchInput.Page == input.Page
                      && searchInput.PerPage == input.PerPage
                      && searchInput.Search == input.Search
                      && searchInput.OrderBy == input.Sort
                      && searchInput.Oder == input.Dir
                      ),
                 It.IsAny<CancellationToken>()
                )).ReturnsAsync(outputRepositorySearch);
            var useCase = new ListGenres(genreRepositoryMock.Object);

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
                outputItem.IsActive.Should().Be(reposiotryCategory.IsActive);
                outputItem.CreatedAt.Should().Be(reposiotryCategory.CreatedAt);

            });
            genreRepositoryMock.Verify(x => x.Search(
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
        [Trait("Application", "ListGenres - UseCases")]
        public async Task ListCategoriesOkWhenEmpty()
        {
            var repositoryMock = _fixture.GetGenreRepositoryMock();
            var input = _fixture.GetListGenresInput();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Genre>(
                 currentPage: input.Page,
                 perPage: input.PerPage,
                 items: new List<DomainEntity.Genre>().AsReadOnly(),
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
            var useCase = new ListGenres(repositoryMock.Object);

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
        [Trait("Application", "ListGenres - UseCases")]
        [MemberData(
            nameof(ListGenresTestGeneratorData.GetListGeresInputWithDiferentsParameters),
            parameters: 14,
            MemberType = typeof(ListGenresTestGeneratorData)
            )]
        public async Task ListCategoriesWithSomeParemeters(ListGenresInput input)
        {
            var repositoryMock = _fixture.GetGenreRepositoryMock();
            var geresList = _fixture.GetGenres();
            var outputRepositorySearch = new SearchOutput<DomainEntity.Genre>(
               currentPage: input.Page,
               perPage: input.PerPage,
               items: geresList,
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
            var useCase = new ListGenres(repositoryMock.Object);

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

