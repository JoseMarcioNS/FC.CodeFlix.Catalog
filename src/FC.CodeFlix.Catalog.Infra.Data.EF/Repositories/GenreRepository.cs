using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using FC.CodeFlix.Catalog.Infra.Data.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly CodeFlixCatalogDbContext _context;
        private DbSet<Genre> _genres => _context.Genres;
        private DbSet<GenresCategories> _genresCategories => _context.GenresCategories;
        public GenreRepository(CodeFlixCatalogDbContext context)
         => _context = context;

        public async Task Insert(Genre genre, CancellationToken cancellationToken)
        {
            await _genres.AddAsync(genre);
            if (genre.Categories.Count > 0)
            {
                var realtions = genre.Categories.Select(categoryId => new GenresCategories(genre.Id, categoryId));
                await _genresCategories.AddRangeAsync(realtions);
            }

        }
        public async Task<Genre> Get(Guid id, CancellationToken cancellationToken)
        {
            var genre = await _genres.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            NotFoundException.IfNull(genre, $"Genre '{id}' not found.");
            return genre!;
        }

        public async Task Delete(Genre genre, CancellationToken cancellationToken)
        {
            var genresCategories = await _genresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            if (genresCategories.Any())
                _genresCategories.RemoveRange(genresCategories);
            _genres.Remove(genre);
        }
        public async Task Update(Genre genre, CancellationToken cancellationToken)
        {
            _genres.Update(genre);
            var genresCategories = await _genresCategories.Where(x => x.GenreId == genre.Id).ToListAsync();
            if (genresCategories.Any())
                _genresCategories.RemoveRange(genresCategories);
            if (genre.Categories.Count > 0)
            {
                var realtions = genre.Categories.Select(categoryId => new GenresCategories(genre.Id, categoryId));
                await _genresCategories.AddRangeAsync(realtions);
            }
        }

        public async Task<SearchOutput<Genre>> Search(SearchInput input, CancellationToken cancellationToken)
        {
            var skip = (input.Page - 1) * input.PerPage;
            var query = _genres.AsNoTracking();
            query = ReturnQueryableCategoriesOrdered(query, input.OrderBy, input.Oder);
            if (!string.IsNullOrWhiteSpace(input.Search))
                query = query.Where(genre => genre.Name.Contains(input.Search));

            var total = await query.CountAsync();
            var genres = await query.Skip(skip).Take(input.PerPage).ToListAsync();
            var genresIds = genres.Select(genre => genre.Id);
            var genresCategories = await _genresCategories.Where(genreCategories => genresIds.Contains(genreCategories.GenreId)).ToListAsync();
            genres.ForEach(genre =>
            {
                var relations = genresCategories.Where(related => related.GenreId == genre.Id).ToList();
                if (relations.Count <= 0) return;
                relations.ForEach(related => genre.AddCategory(related.CategoryId)
               );
            });
            return new(input.Page, input.PerPage, total, genres);
        }
        private IQueryable<Genre> ReturnQueryableCategoriesOrdered(
            IQueryable<Genre> query,
            string oderBy,
            SearchOrder searchOrder)
        {
            var queryOrdered = (oderBy.ToLower(), searchOrder) switch
            {
                ("name", SearchOrder.Asc) => query.OrderBy(x => x.Name).ThenBy(x => x.Id),
                ("name", SearchOrder.Desc) => query.OrderByDescending(x => x.Name).ThenByDescending(x => x.Id),
                ("id", SearchOrder.Asc) => query.OrderBy(x => x.Id),
                ("id", SearchOrder.Desc) => query.OrderByDescending(x => x.Id),
                ("createdat", SearchOrder.Asc) => query.OrderBy(x => x.CreatedAt),
                ("createdat", SearchOrder.Desc) => query.OrderByDescending(x => x.CreatedAt),
                _ => query.OrderBy(x => x.Name).ThenBy(x => x.Id)
            };
            return queryOrdered;
        }
    }
}
