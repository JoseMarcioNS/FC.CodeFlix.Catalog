using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Create
{
    public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CreateCategoryOuput>
    {
        public Task<CreateCategoryOuput> Handle(CreateCategoryInput input, CancellationToken cancellationToken);
    }
}
