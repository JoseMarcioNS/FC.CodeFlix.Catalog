using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Create;
using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Update
{
    public class UpdateGenre : IUpdateGenre
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGenre(IGenreRepository genreRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenreModelOutput> Handle(UpdateGenreInput request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.Get(request.Id, cancellationToken);
            genre.Update(request.Name);
            if (request.IsActive is not null)
            {
                if (request.IsActive.Value)
                    genre.Active();
                else
                    genre.Deactivate();
            }
            if (request.CategoriesIds is not null)
            {
                await ValidateCategoriesIds(request, cancellationToken);
                genre.RemoveAllCategories();
                request.CategoriesIds.ForEach(genre.AddCategory);
            }

            await _genreRepository.Update(genre, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return GenreModelOutput.FromGenre(genre);
        }
        private async Task ValidateCategoriesIds(UpdateGenreInput request, CancellationToken cancellationToken)
        {
            var categriesListIds = await _categoryRepository.GetCategoriesByIds(request.CategoriesIds!, cancellationToken);
            if (categriesListIds.Count != request.CategoriesIds!.Count)
            {
                var categoriesNotFoundIds = request.CategoriesIds.Except(categriesListIds);
                throw new RelatedAggregateException($"Related category id(s) not found: {string.Join(", ", categoriesNotFoundIds)}");
            }
        }
    }
}
