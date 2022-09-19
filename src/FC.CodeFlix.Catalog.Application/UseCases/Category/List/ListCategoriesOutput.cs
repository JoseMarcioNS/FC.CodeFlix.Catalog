using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.List
{
    public class ListCategoriesOutput : PaginatedListOutput<CategoryModelOuput>
    {
        public ListCategoriesOutput(int currentPage, int perPage, int total, IReadOnlyList<CategoryModelOuput> items)
            : base(currentPage, perPage, total, items)
        {
        }
    }
}
