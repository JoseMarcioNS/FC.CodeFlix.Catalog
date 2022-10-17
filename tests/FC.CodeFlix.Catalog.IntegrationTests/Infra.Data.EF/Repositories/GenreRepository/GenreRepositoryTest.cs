using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.Infra.Data.EF.Models;

namespace FC.CodeFlix.Catalog.IntegrationTests.Infra.Data.EF.Repositories.GenreRepository
{
    [Collection(nameof(GenreRepositoryTestFixture))]
    public class GenreRepositoryTest : IDisposable
    {
        private readonly GenreRepositoryTestFixture _fixture;

        public GenreRepositoryTest(GenreRepositoryTestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(Insert))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task Insert()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var genre = _fixture.GetValidGenre();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
            categories.ForEach(c => genre.AddCategory(c.Id));
            var genreRepository = new Repository.GenreRepository(context);

            await genreRepository.Insert(genre, CancellationToken.None);
            await context.SaveChangesAsync();

            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().NotBeNull();
            genreDb!.Name.Should().Be(genre.Name);
            genreDb.IsActive.Should().Be(genre.IsActive);
            genreDb.CreatedAt.Should().Be(genre.CreatedAt);
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            genreRelatedCategories.ForEach(x => genre.Categories.Should().Contain(x.CategoryId));
        }
        [Fact(DisplayName = nameof(Get))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task Get()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new List<GenresCategories>();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);


            var output = await genreRepository.Get(genre.Id, CancellationToken.None);

            output.Should().NotBeNull();
            output!.Id.Should().Be(genre.Id);
            output.Name.Should().Be(genre.Name);
            output.IsActive.Should().Be(genre.IsActive);
            output.CreatedAt.Should().Be(genre.CreatedAt);
            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().NotBeNull();
            genreDb!.Name.Should().Be(genre.Name);
            genreDb.IsActive.Should().Be(genre.IsActive);
            genreDb.CreatedAt.Should().Be(genre.CreatedAt);
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            genreRelatedCategories.ForEach(x => genre.Categories.Should().Contain(x.CategoryId));
        }
        [Fact(DisplayName = nameof(WhenGetThrowExceptionWhenNotFound))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task WhenGetThrowExceptionWhenNotFound()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);
            var genreIdNotExist = Guid.NewGuid();

            var action = async () => await genreRepository.Get(genreIdNotExist, CancellationToken.None);

            await action.Should().ThrowAsync<NotFoundException>().WithMessage($"Genre '{genreIdNotExist}' not found.");

        }
        [Fact(DisplayName = nameof(Delete))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task Delete()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);

            await genreRepository.Delete(genre, CancellationToken.None);
            await context.SaveChangesAsync();

            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().BeNull();
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            genreRelatedCategories.Should().HaveCount(0);

        }
        [Fact(DisplayName = nameof(Update))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task Update()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var actDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreRepository = new Repository.GenreRepository(actDbcontext);


            genre.Update(_fixture.GetValidName());
            if (genre.IsActive)
                genre.Deactivate();
            else
                genre.Active();
            await genreRepository.Update(genre, CancellationToken.None);
            await actDbcontext.SaveChangesAsync();

            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().NotBeNull();
            genreDb!.Name.Should().Be(genre.Name);
            genreDb.IsActive.Should().Be(genre.IsActive);
            genreDb.CreatedAt.Should().Be(genre.CreatedAt);
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            categories.ForEach(r => genreRelatedCategories.Should().Contain(c => c.CategoryId == r.Id));

        }
        [Fact(DisplayName = nameof(UpdateRemoveRelatedCategoreis))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task UpdateRemoveRelatedCategoreis()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            await context.Categories.AddRangeAsync(categories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var actDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreRepository = new Repository.GenreRepository(actDbcontext);


            genre.Update(_fixture.GetValidName());
            if (genre.IsActive)
                genre.Deactivate();
            else
                genre.Active();
            genre.RemoveAllCategories();
            await genreRepository.Update(genre, CancellationToken.None);
            await actDbcontext.SaveChangesAsync();

            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().NotBeNull();
            genreDb!.Name.Should().Be(genre.Name);
            genreDb.IsActive.Should().Be(genre.IsActive);
            genreDb.CreatedAt.Should().Be(genre.CreatedAt);
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            genreRelatedCategories.Should().HaveCount(0);

        }
        [Fact(DisplayName = nameof(UpdateRelatedCategoreis))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task UpdateRelatedCategoreis()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var categories = _fixture.CategoryFixture.GetListCategories();
            var newcategories = _fixture.CategoryFixture.GetListCategories(5);
            await context.Categories.AddRangeAsync(categories);
            await context.Categories.AddRangeAsync(newcategories);
            var genre = _fixture.GetValidGenre();
            await context.Genres.AddAsync(genre);
            categories.ForEach(c => genre.AddCategory(c.Id));
            List<GenresCategories> genresCategories = new();
            categories.ForEach(c => genresCategories.Add(new GenresCategories(genre.Id, c.Id)));
            await context.GenresCategories.AddRangeAsync(genresCategories);
            await context.SaveChangesAsync();
            var actDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreRepository = new Repository.GenreRepository(actDbcontext);


            genre.Update(_fixture.GetValidName());
            if (genre.IsActive)
                genre.Deactivate();
            else
                genre.Active();
            genre.RemoveAllCategories();
            newcategories.ForEach(c => genre.AddCategory(c.Id));
            await genreRepository.Update(genre, CancellationToken.None);
            await actDbcontext.SaveChangesAsync();

            var assertDbcontext = _fixture.CommonFixture.CreateDbContext();
            var genreDb = await assertDbcontext.Genres.FindAsync(genre.Id);
            genreDb.Should().NotBeNull();
            genreDb!.Name.Should().Be(genre.Name);
            genreDb.IsActive.Should().Be(genre.IsActive);
            genreDb.CreatedAt.Should().Be(genre.CreatedAt);
            var genreRelatedCategories = await assertDbcontext.GenresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            genreRelatedCategories.Should().HaveCount(newcategories.Count);
            newcategories.ForEach(c => genreRelatedCategories.Should().Contain(x => x.CategoryId == c.Id));

        }
        [Fact(DisplayName = nameof(SearchReturnItemsAndTotal))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task SearchReturnItemsAndTotal()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var genres = _fixture.GetValidGenres(5);
            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);
            var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

            var searchOutput = await genreRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Items.Should().HaveCount(genres.Count);
            searchOutput.Total.Should().Be(genres.Count);
            foreach (var genreItem in searchOutput.Items)
            {
                var genre = genres.Find(x => x.Id == genreItem.Id);
                genre.Should().NotBeNull();
                genre!.Name.Should().Be(genreItem.Name);
                genre.CreatedAt.Should().Be(genreItem.CreatedAt);
                genre.IsActive.Should().Be(genreItem.IsActive);

            }
        }
        [Fact(DisplayName = nameof(SearchReturnRelated))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        public async Task SearchReturnRelated()
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var genres = _fixture.GetValidGenres(5);
            await context.Genres.AddRangeAsync(genres);
            genres.ForEach(genre =>
            {
                var categories = _fixture.CategoryFixture.GetListCategories(new Random().Next(0,5));
                if (categories.Count <= 0) return;
                 categories.ForEach(category =>
                {
                    genre.AddCategory(category.Id);
                    context.GenresCategories.Add(new GenresCategories(genre.Id, category.Id));
                });
                context.Categories.AddRange(categories);

            });
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);
            var searchInput = new SearchInput(1, 20, "", "", SearchOrder.Asc);

            var searchOutput = await genreRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Items.Should().HaveCount(genres.Count);
            searchOutput.Total.Should().Be(genres.Count);
            foreach (var genreItem in searchOutput.Items)
            {
                var genre = genres.Find(x => x.Id == genreItem.Id);
                genre.Should().NotBeNull();
                genre!.Name.Should().Be(genreItem.Name);
                genre.CreatedAt.Should().Be(genreItem.CreatedAt);
                genre.IsActive.Should().Be(genreItem.IsActive);
                genre.Categories.Should().HaveCount(genreItem.Categories.Count);
                genre.Categories.Should().BeEquivalentTo(genreItem.Categories);
            }
        }
        [Theory(DisplayName = nameof(SearchResultsPaginated))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        [InlineData(15, 1, 5, 5)]
        [InlineData(15, 2, 5, 5)]
        [InlineData(15, 3, 6, 3)]
        public async Task SearchResultsPaginated(
            int quantityGenerator,
            int currentPage,
            int perPage,
            int expectedQuantityItems
            )
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var genres = _fixture.GetValidGenres(quantityGenerator);
            await context.Genres.AddRangeAsync(genres);
            genres.ForEach(genre =>
            {
                var categories = _fixture.CategoryFixture.GetListCategories(new Random().Next(0, 5));
                if (categories.Count <= 0) return;
                categories.ForEach(category =>
                {
                    genre.AddCategory(category.Id);
                    context.GenresCategories.Add(new GenresCategories(genre.Id, category.Id));
                });
                context.Categories.AddRange(categories);

            });
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);
            var searchInput = new SearchInput(currentPage, perPage, "", "", SearchOrder.Asc);

            var searchOutput = await genreRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Items.Should().HaveCount(expectedQuantityItems);
            searchOutput.Total.Should().Be(genres.Count);
            foreach (var genreItem in searchOutput.Items)
            {
                var genre = genres.Find(x => x.Id == genreItem.Id);
                genre.Should().NotBeNull();
                genre!.Name.Should().Be(genreItem.Name);
                genre.CreatedAt.Should().Be(genreItem.CreatedAt);
                genre.IsActive.Should().Be(genreItem.IsActive);
                genre.Categories.Should().HaveCount(genreItem.Categories.Count);
                genre.Categories.Should().BeEquivalentTo(genreItem.Categories);
            }
        }
        [Theory(DisplayName = nameof(SearchResultsByText))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
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
            var context = _fixture.CommonFixture.CreateDbContext();
            var genres = _fixture.GetValidGenresWithNames(
               new List<string> {
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
          
            await context.Genres.AddRangeAsync(genres);
            genres.ForEach(genre =>
            {
                var categories = _fixture.CategoryFixture.GetListCategories(new Random().Next(0, 5));
                if (categories.Count <= 0) return;
                categories.ForEach(category =>
                {
                    genre.AddCategory(category.Id);
                    context.GenresCategories.Add(new GenresCategories(genre.Id, category.Id));
                });
                context.Categories.AddRange(categories);

            });
            await context.SaveChangesAsync();
            var genreRepository = new Repository.GenreRepository(context);
            var searchInput = new SearchInput(currentPage, perPage, search, "", SearchOrder.Asc);

            var searchOutput = await genreRepository.Search(searchInput, CancellationToken.None);

            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(searchInput.Page);
            searchOutput.PerPage.Should().Be(searchInput.PerPage);
            searchOutput.Items.Should().HaveCount(expectedQuantityItemsReturned);
            searchOutput.Total.Should().Be(expectedQuantitytotalItems);
            foreach (var genreItem in searchOutput.Items)
            {
                var genre = genres.Find(x => x.Id == genreItem.Id);
                genre.Should().NotBeNull();
                genre!.Name.Should().Be(genreItem.Name);
                genre.CreatedAt.Should().Be(genreItem.CreatedAt);
                genre.IsActive.Should().Be(genreItem.IsActive);
                genre.Categories.Should().HaveCount(genreItem.Categories.Count);
                genre.Categories.Should().BeEquivalentTo(genreItem.Categories);
            }
        }
        [Theory(DisplayName = nameof(SearchResultsOrdered))]
        [Trait("Integration/Infra.Data", "GenreRepositoryRepository - Repositories")]
        [InlineData("name", "asc")]
        [InlineData("name", "desc")]
        [InlineData("id", "asc")]
        [InlineData("id", "desc")]
        [InlineData("createdAt", "asc")]
        [InlineData("createdAt", "desc")]
        [InlineData("", "asc")]
        public async Task SearchResultsOrdered(string orderBy, string order)
        {
            var context = _fixture.CommonFixture.CreateDbContext();
            var genres = _fixture.GetValidGenres();
            var categoryRepository = new Repository.GenreRepository(context);
            await context.AddRangeAsync(genres);
            await context.SaveChangesAsync();
            var searchOrder = order.ToLower() == "asc" ? SearchOrder.Asc : SearchOrder.Desc;
            var searchInput = new SearchInput(1, genres.Count, "", orderBy, searchOrder);

            var searchOutput = await categoryRepository.Search(searchInput, CancellationToken.None);

            var genresOrdered = _fixture.CloneGenresOrdered(genres, orderBy, searchOrder);
            searchOutput.Should().NotBeNull();
            searchOutput.CurrentPage.Should().Be(1);
            searchOutput.PerPage.Should().Be(genres.Count);
            searchOutput.Total.Should().Be(genres.Count);
            searchOutput.Items.Should().HaveCount(genres.Count);

            for (int i = 0; i < genresOrdered.Count; i++)
            {
                var categoryReturned = searchOutput.Items[i];
                var categoryOdered = genresOrdered[i];
                categoryReturned.Id.Should().Be(categoryOdered.Id);
                categoryReturned!.Name.Should().Be(categoryOdered.Name);
                categoryReturned.IsActive.Should().Be(categoryOdered.IsActive);

            }
        }
        public void Dispose()
        => _fixture.CommonFixture.CleanInMemoryDatabase();
    }
}
