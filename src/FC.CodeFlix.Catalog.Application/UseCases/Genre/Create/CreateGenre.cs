using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Create
{
    public class CreateGenre : ICreateGenre
    {
        private readonly IGenreRepository _genreRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGenre(IGenreRepository genreRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _genreRepository = genreRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GenreModelOutput> Handle(CreateGenreInput request, CancellationToken cancellationToken)
        {
            var input = new DomainEntity.Genre(request.Name, request.IsActive);

            if ((request.CategoriesIds?.Count ?? 0) > 0)
            {
                await ValidateCategoriesIds(request, cancellationToken);
                request.CategoriesIds?.ForEach(input.AddCategory);
            }

            await _genreRepository.Insert(input, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return GenreModelOutput.FromGenre(input);
        }

        private async Task ValidateCategoriesIds(CreateGenreInput request, CancellationToken cancellationToken)
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
