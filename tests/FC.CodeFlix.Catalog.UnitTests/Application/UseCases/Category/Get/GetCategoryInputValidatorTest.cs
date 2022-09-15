using FC.CodeFlix.Catalog.Application.UseCases.Category.Get;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Get
{
    [Collection(nameof(GetCategoryTestFixure))]
    public class GetCategoryInputValidatorTest
    {
        private readonly GetCategoryTestFixure _fixure;

        public GetCategoryInputValidatorTest(GetCategoryTestFixure fixure)
         => _fixure = fixure;

        [Fact(DisplayName = nameof(ValidationOk))]
        [Trait("Application", "GetCategoryInputValidatorTest - UseCases")]
        public void ValidationOk()
        {
            var validInput = new GetCategoryInput(Guid.NewGuid());
            var validator = new GetCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);

        }
        [Fact(DisplayName = nameof(InvalidWhenEmptyGuid))]
        [Trait("Application", "GetCategoryInputValidatorTest - UseCases")]
        public void InvalidWhenEmptyGuid()
        {
            var validInput = new GetCategoryInput(Guid.Empty);
            var validator = new GetCategoryInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Should().HaveCount(1);
            validationResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");

        }
    }
}
