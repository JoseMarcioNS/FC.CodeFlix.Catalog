using FC.CodeFlix.Catalog.Domain.Exceptions;


namespace FC.CodeFlix.Catalog.UnitTests.Domain.Entity.Category
{
    [Collection(nameof(CategoryTestFixture))]
    public class CategoryTest
    {
        private readonly CategoryTestFixture _fixture;
        public CategoryTest(CategoryTestFixture fixture) =>
                   _fixture = fixture;


        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Category - Aggregates")]
        public void Instantiate()
        {
            var validCategory = _fixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.Should().NotBeSameAs(default(DateTime));
            (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
            category.IsActive.Should().BeTrue();
        }
        [Theory(DisplayName = nameof(InstantiateWithActive))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithActive(bool isActive)
        {
            var validCategory = _fixture.GetValidCategory();
            var dateTimeBefore = DateTime.Now;
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, isActive);
            var dateTimeAfter = DateTime.Now.AddSeconds(1);

            category.Should().NotBeNull();
            category.Name.Should().Be(validCategory.Name);
            category.Description.Should().Be(validCategory.Description);
            category.Id.Should().NotBeEmpty();
            category.Should().NotBeSameAs(default(DateTime));
            (category.CreatedAt >= dateTimeBefore).Should().BeTrue();
            (category.CreatedAt <= dateTimeAfter).Should().BeTrue();
            category.IsActive.Should().Be(isActive);

        }
        [Theory(DisplayName = nameof(InstantiateNameIsEmptyOrNull))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void InstantiateNameIsEmptyOrNull(string? invalidName)
        {
            var validCategory = _fixture.GetValidCategory();

            Action action = () => new DomainEntity.Category(invalidName!, validCategory.Description);

           action.Should().Throw<EntityValidationException>().WithMessage("Name should not be null or empty");

        }
        [Theory(DisplayName = nameof(InstantiateErrorwhenNameLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetrwhenNameLessThan3Characters), parameters: 10)]
        public void InstantiateErrorwhenNameLessThan3Characters(string invalidName)
        {
            var validCategory = _fixture.GetValidCategory();
            Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);

            action.Should()
           .Throw<EntityValidationException>()
           .WithMessage("Name should not be less than 3 characters long");

        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var validCategory = _fixture.GetValidCategory();

            var invalidName = _fixture.Faker.Commerce.Categories(1)[0];
            while (invalidName.Length <= 255)
                invalidName += _fixture.Faker.Commerce.Categories(1)[0];

            Action action = () => new DomainEntity.Category(invalidName, validCategory.Description);

            action.Should()
                .Throw<EntityValidationException>()
                .WithMessage("Name should not be greater than 255 characters long");
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            var validCategory = _fixture.GetValidCategory();
            Action action = () => new DomainEntity.Category(validCategory.Name, null!);

            action.Should()
                 .Throw<EntityValidationException>()
                 .WithMessage("Description should not be null");


        }
        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var validCategory = _fixture.GetValidCategory();
            var invalidDescription = _fixture.Faker.Commerce.ProductDescription();
            while (invalidDescription.Length <= 10_000)
                invalidDescription += _fixture.Faker.Commerce.ProductDescription();

            Action action = () => new DomainEntity.Category(validCategory.Name, invalidDescription);

            action.Should()
                  .Throw<EntityValidationException>()
                  .WithMessage("Description should not be greater than 10000 characters long");


        }
        [Fact(DisplayName = nameof(InstantiateIsActive))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateIsActive()
        {
            var validCategory = _fixture.GetValidCategory();
            var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);

            category.Active();

            category.IsActive.Should().BeTrue();
        }
        [Fact(DisplayName = nameof(InstantiateIsDeactivate))]
        [Trait("Domain", "Category - Aggregates")]
        public void InstantiateIsDeactivate()
        {
            var category = _fixture.GetValidCategory();

            category.Deactivate();

            category.IsActive.Should().BeFalse();
        }
        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Category - Aggregates")]
        public void Update()
        {
            var category = _fixture.GetValidCategory();
            var updateCategory = _fixture.GetValidCategory();

            category.Update(updateCategory.Name, updateCategory.Description);

            category.Name.Should().Be(updateCategory.Name);
            category.Description.Should().Be(category.Description);
        }
        [Theory(DisplayName = nameof(UpdateNameIsEmptyOrNull))]
        [Trait("Domain", "Category - Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        public void UpdateNameIsEmptyOrNull(string? invalidName)
        {
            var category = _fixture.GetValidCategory();
            Action action = () => category.Update(invalidName!);

            action.Should()
                  .Throw<EntityValidationException>()
                  .WithMessage("Name should not be null or empty");
        }
        [Theory(DisplayName = nameof(UpdateErrorwhenNameLessThan3Characters))]
        [Trait("Domain", "Category - Aggregates")]
        [MemberData(nameof(GetrwhenNameLessThan3Characters), parameters: 10)]
        public void UpdateErrorwhenNameLessThan3Characters(string invalidName)
        {
            var category = _fixture.GetValidCategory();
            Action action = () => category.Update(invalidName);

            action.Should()
                 .Throw<EntityValidationException>()
                 .WithMessage("Name should not be less than 3 characters long");

        }
        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenNameIsGreaterThan255Characters()
        {
            var category = _fixture.GetValidCategory();
            var invalidName = _fixture.Faker.Commerce.Categories(1)[0];
            while (invalidName.Length <= 255)
                invalidName += _fixture.Faker.Commerce.Categories(1)[0];

            Action action = () => category.Update(invalidName);

            action.Should()
                  .Throw<EntityValidationException>()
                  .WithMessage("Name should not be greater than 255 characters long");
        }
        [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Category - Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var category = _fixture.GetValidCategory();
            var invalidDescription = _fixture.Faker.Commerce.ProductDescription();
            while (invalidDescription.Length <= 10_000)
                invalidDescription += _fixture.Faker.Commerce.ProductDescription();

            Action action = () => category.Update("Category New Name", invalidDescription);

            action.Should()
                  .Throw<EntityValidationException>()
                  .WithMessage("Description should not be greater than 10000 characters long");
        }
        public static IEnumerable<Object[]> GetrwhenNameLessThan3Characters(int numberOfTests)
        {
            var fixtureCategory = new CategoryTestFixture();

            for (int i = 0; i < numberOfTests; i++)
            {
                var isOdd = i % 2 == 1;
                yield return new object[] {
                         fixtureCategory.GetValidName()[..(isOdd ? 1 : 2)]
                };
            }

        }
    }
}
