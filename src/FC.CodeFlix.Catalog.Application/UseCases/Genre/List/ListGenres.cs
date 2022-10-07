using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.List
{
    public class ListGenres : IListGenres
    {
        private readonly IGenreRepository _genreRepository;

        public ListGenres(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public async Task<ListGenresOutput> Handle(ListGenresInput request, CancellationToken cancellationToken)
        {
            var output = await _genreRepository.Search(request.ToSearchInput(), cancellationToken);
            return ListGenresOutput.FromSearchOutput(output);
        }
    }
}
