using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.Domain.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
        , ISearchableRepository<Genre>
    {
    }
}
