using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Create
{
    public interface ICreateCategory : IRequestHandler<CreateCategoryInput, CategoryModelOuput>
    { }
}
