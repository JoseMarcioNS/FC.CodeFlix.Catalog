using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.List
{
    public class ListCategoriesOutput : PaginatedListOutput<CategoryModelOuput>
    {
        public ListCategoriesOutput(int currentPage, int perPage, int total, IReadOnlyList<CategoryModelOuput> items)
            : base(currentPage, perPage, total, items)
        {
        }
        public static ListCategoriesOutput FromSearchOutput(SearchOutput<DomainEntity.Category> searchOutput)
            => new(searchOutput.CurrentPage,
                   searchOutput. PerPage,
                   searchOutput.Total,
                   searchOutput.Items.Select(CategoryModelOuput.FromCategory).ToList());

    }
}
