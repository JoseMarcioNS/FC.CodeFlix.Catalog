using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Get
{
    public interface IGetCategory : IRequestHandler<GetCategoryInput, CategoryModelOuput>
    {
    }
}
