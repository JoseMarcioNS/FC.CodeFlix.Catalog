using Bogus.DataSets;
using FC.CodeFlix.Catalog.Domain.Exceptions;

namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Genre
{
    [Collection(nameof(GenreTestFixture))]
    public class GenreTest
    {
        private readonly GenreTestFixture _fixture;

        public GenreTest(GenreTestFixture fixture)
       => _fixture = fixture;

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Genre - Aggregates")]
        public void Instantiate()
        {
            var name = _fixture.GetValidName();
            var genre = new DomainEntity.Genre(name);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.IsActive.Should().BeTrue();
            genre.CreatedAt.Should().NotBeSameDateAs(default);

        }
        [Theory(DisplayName = nameof(InstantiateWithActiveTrueAndFalse))]
        [Trait("Domain", "Genre - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithActiveTrueAndFalse(bool isActive)
        {
            var name = _fixture.GetValidName();
            var genre = new DomainEntity.Genre(name, isActive);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.IsActive.Should().Be(isActive);
            genre.CreatedAt.Should().NotBeSameDateAs(default);

        }
        [Theory(DisplayName = nameof(ThrowExceptionWhenInstantiateWithNameEmpty))]
        [Trait("Domain", "Genre - Aggregates")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ThrowExceptionWhenInstantiateWithNameEmpty(string? name)
        {
            Action action = () => new DomainEntity.Genre(name!);

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should not be null or empty");
        }

        [Fact(DisplayName = nameof(UpdateGenre))]
        [Trait("Domain", "Genre - Aggregates")]
        public void UpdateGenre()
        {
            var genre = _fixture.GetValidGenre();
            var name = _fixture.GetValidName();

            genre.Update(name);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.Name.Should().Be(name);

        }
        [Theory(DisplayName = nameof(ThrowExceptionWhenUpdateWithNameEmpty))]
        [Trait("Domain", "Genre - Aggregates")]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public void ThrowExceptionWhenUpdateWithNameEmpty(string? name)
        {
            var genre = _fixture.GetValidGenre();

            Action action = () => genre.Update(name!);

            action.Should().Throw<EntityValidationException>()
                .WithMessage("Name should not be null or empty");
        }
        [Fact(DisplayName = nameof(UpdateIsActive))]
        [Trait("Domain", "Genre - Aggregates")]
        public void UpdateIsActive()
        {
            var genre = new DomainEntity.Genre(_fixture.GetValidName(), false);

            genre.Active();

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.IsActive.Should().BeTrue();

        }
        [Fact(DisplayName = nameof(UpdateIsDeactivate))]
        [Trait("Domain", "Genre - Aggregates")]
        public void UpdateIsDeactivate()
        {
            var genre = new DomainEntity.Genre(_fixture.GetValidName(), false);

            genre.Deactivate();

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.IsActive.Should().BeFalse();

        }
        [Fact(DisplayName = nameof(AddCategory))]
        [Trait("Domain", "Genre - Aggregates")]
        public void AddCategory()
        {
            var genre = _fixture.GetValidGenre();
            var categaryId = Guid.NewGuid();

            genre.AddCategory(categaryId);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.Categories.Should().HaveCount(1);
            genre.Categories.Should().Contain(categaryId);

        }
        [Fact(DisplayName = nameof(AddSameCategories))]
        [Trait("Domain", "Genre - Aggregates")]
        public void AddSameCategories()
        {
            var categaryId1 = Guid.NewGuid();
            var categaryId2 = Guid.NewGuid();
            var genre = _fixture.GetValidGenre();

            genre.AddCategory(categaryId1);
            genre.AddCategory(categaryId2);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.Categories.Should().HaveCount(2);
            genre.Categories.Should().Contain(categaryId1);
            genre.Categories.Should().Contain(categaryId2);

        }
        [Fact(DisplayName = nameof(RemoveCategory))]
        [Trait("Domain", "Genre - Aggregates")]
        public void RemoveCategory()
        {
            var categaryId = Guid.NewGuid();
            var categoriesIds = new List<Guid>() {
                Guid.NewGuid(),
                categaryId,
                Guid.NewGuid()
            };
            var genre = _fixture.GetValidGenre(categoriesIds);

            genre.RemoveCategory(categaryId);

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.Categories.Should().HaveCount(2);
            genre.Categories.Should().NotContain(categaryId);

        }
        [Fact(DisplayName = nameof(RemoveAllCategories))]
        [Trait("Domain", "Genre - Aggregates")]
        public void RemoveAllCategories()
        {
            var categaryId = Guid.NewGuid();
            var categoriesIds = new List<Guid>() {
                Guid.NewGuid(),
                categaryId,
                Guid.NewGuid()
            };
            var genre = _fixture.GetValidGenre(categoriesIds);

            genre.RemoveAllCategories();

            genre.Should().NotBeNull();
            genre.Id.Should().NotBeEmpty();
            genre.Categories.Should().HaveCount(0);

        }
    }
}
