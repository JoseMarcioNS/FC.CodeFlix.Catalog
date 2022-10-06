using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.Domain.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
        , ISearchableRepository<Category>
    {
        Task<List<Guid>> GetCategoriesByIds(List<Guid> ids, CancellationToken cancellationToken);
    }
}
