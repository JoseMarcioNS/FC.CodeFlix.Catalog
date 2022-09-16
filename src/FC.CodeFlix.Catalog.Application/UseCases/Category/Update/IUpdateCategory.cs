using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Update
{
    public interface IUpdateCategory : IRequestHandler<UpdateCategoryInput, CategoryModelOuput>
    {
    }
}
