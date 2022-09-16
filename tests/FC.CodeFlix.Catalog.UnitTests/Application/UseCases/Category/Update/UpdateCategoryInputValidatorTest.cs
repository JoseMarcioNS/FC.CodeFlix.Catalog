using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [Collection(nameof(UpdateGategoryTestFixture))]
    public class UpdateCategoryInputValidatorTest
    {
        private readonly UpdateGategoryTestFixture _fixture;

        public UpdateCategoryInputValidatorTest(UpdateGategoryTestFixture fixture)
       => _fixture = fixture;

        [Fact(DisplayName = nameof(ValidateOk))]
        [Trait("Application", "UpdateCategoryInputValidatorTest - UseCases")]
        public void ValidateOk()
        {
            var category = _fixture.GetCategory();
            var validation = new UpdateCategoryInputValidator();

            var validationResult = validation.Validate(category);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }
        [Fact(DisplayName = nameof(InvalidCategory))]
        [Trait("Application", "UpdateCategoryInputValidatorTest - UseCases")]
        public void InvalidCategory()
        {
            var category = _fixture.GetCategory(Guid.Empty);
            var validation = new UpdateCategoryInputValidator();

            var validationResult = validation.Validate(category);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
        }
    }
}
