using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Delete
{
    public class DeleteCategoryInputValidator : AbstractValidator<DeleteCategoryInput>
    {
        public DeleteCategoryInputValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
