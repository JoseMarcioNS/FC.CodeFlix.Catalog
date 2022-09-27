using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common
{

    public class CategoryPersistence
    {
        private readonly CodeFlixCatalogDbContext _context;

        public CategoryPersistence(CodeFlixCatalogDbContext context)
        => _context = context;

        public async Task<DomainEntity.Category?> GetById(Guid id)
         =>await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task InsertCategories(List<DomainEntity.Category> categories)
        {
            await _context.Categories.AddRangeAsync(categories);
            await _context.SaveChangesAsync();
        }

    }
}

