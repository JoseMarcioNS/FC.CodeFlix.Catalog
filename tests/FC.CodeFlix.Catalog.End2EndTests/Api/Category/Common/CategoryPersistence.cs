using FC.CodeFlix.Catalog.Infra.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Common
{

    public class CategoryPersistence
    {
        private readonly CodeFlixCatalogDbContext _context;

        public CategoryPersistence(CodeFlixCatalogDbContext context)
        => _context = context;

        public async Task<Domainentity.Category?> GetById(Guid id)
         =>await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        
    }
}

