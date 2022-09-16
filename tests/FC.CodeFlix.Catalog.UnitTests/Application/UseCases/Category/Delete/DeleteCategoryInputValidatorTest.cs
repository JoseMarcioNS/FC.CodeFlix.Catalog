using FC.CodeFlix.Catalog.Application.UseCases.Category.Delete;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Delete
{

    public class DeleteCategoryInputValidatorTest
    {
        [Fact(DisplayName = nameof(ValidationOk))]
        [Trait("Application", "DeleteCategoryInputValidatorTest -  UseCases")]
        public void ValidationOk()
        {
            var deleteCategoryInput = new DeleteCategoryInput(Guid.NewGuid());
            var validation = new DeleteCategoryInputValidator();

            var validatinoResult = validation.Validate(deleteCategoryInput);

            validatinoResult.Should().NotBeNull();
            validatinoResult.IsValid.Should().BeTrue();
            validatinoResult.Errors.Should().HaveCount(0);

        }
        [Fact(DisplayName = nameof(ValidationOk))]
        [Trait("Application", "DeleteCategoryInputValidatorTest -  UseCases")]
        public void InvalidWhenEmptyGuid()
        {
            var deleteCategoryInput = new DeleteCategoryInput(Guid.Empty);
            var validation = new DeleteCategoryInputValidator();

            var validatinoResult = validation.Validate(deleteCategoryInput);

            validatinoResult.Should().NotBeNull();
            validatinoResult.IsValid.Should().BeFalse();
            validatinoResult.Errors.Should().HaveCount(1);
            validatinoResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");

        }
    }
}
