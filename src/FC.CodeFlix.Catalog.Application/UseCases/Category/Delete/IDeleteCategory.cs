using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Delete
{
    public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
    { }
}
