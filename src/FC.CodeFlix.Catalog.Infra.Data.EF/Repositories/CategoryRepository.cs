using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CodeFlixCatalogDbContext _context;
        private DbSet<Category> _categories => _context.Categories;
        public CategoryRepository(CodeFlixCatalogDbContext context)
         => _context = context;

        public async Task Insert(Category aggregate, CancellationToken cancellationToken)
                 => await _categories.AddAsync(aggregate, cancellationToken);
        public async Task<Category> Get(Guid id, CancellationToken cancellationToken)
        {
            var category = await _categories.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.IfNull(category, $"Category '{id}' not found.");
            return category!;
        }
        public Task Update(Category aggregate, CancellationToken cancellationToken)
        => Task.FromResult(_categories.Update(aggregate));

        public Task Delete(Category aggregate, CancellationToken cancellationToken)
         => Task.FromResult(_categories.Remove(aggregate));


        public async Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var query = _categories.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(x => x.Name.Contains(input.Search));

            query = ReturnQueryableCategoriesOrdered(query,input.OrderBy,input.Oder);

            var skip = ((input.Page - 1) * input.PerPage);
            var total = await query.CountAsync();
            var items = await query.Skip(skip).Take(input.PerPage).ToListAsync();
            return new(input.Page, input.PerPage, total, items);
        }
        private IQueryable<Category> ReturnQueryableCategoriesOrdered(
            IQueryable<Category> query,
            string oderBy,
            SearchOrder searchOrder)
        {
            var queryOrdered = (oderBy.ToLower(), searchOrder) switch
            {
                ("name", SearchOrder.Asc) => query.OrderBy(x=>x.Name).ThenBy(x=> x.Id),
                ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderBy(x => x.Name).ThenBy(x=> x.Id)
            };
            return queryOrdered;
        }

        public Task<List<Guid>> GetCategoriesByIds(List<Guid> ids, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
