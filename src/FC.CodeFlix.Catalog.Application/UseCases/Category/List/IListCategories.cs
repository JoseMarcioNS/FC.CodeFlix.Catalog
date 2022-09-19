using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.List
{
    public interface IListCategories : IRequestHandler<ListCategoriesInput, ListCategoriesOutput>
    {
    }
}
