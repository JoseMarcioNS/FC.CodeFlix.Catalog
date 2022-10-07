using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.List
{
    public class ListGenresOutput : PaginatedListOutput<GenreModelOutput>
    {
        public ListGenresOutput(int currentPage, int perPage, int total, IReadOnlyList<GenreModelOutput> items)
            : base(currentPage, perPage, total, items)
        {
        }
        public static ListGenresOutput FromSearchOutput(SearchOutput<DomainEntity.Genre> searchOutput)
        => new(searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items.Select(GenreModelOutput.FromGenre).ToList());

    }
}
