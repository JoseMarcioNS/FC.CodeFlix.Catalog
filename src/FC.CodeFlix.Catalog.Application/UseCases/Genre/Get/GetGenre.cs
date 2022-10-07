using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Get
{
    public class GetGenre : IGetGenre
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenre(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public async Task<GenreModelOutput> Handle(GetGenreInput request, CancellationToken cancellationToken)
        {
            var genre = await _genreRepository.Get(request.Id, cancellationToken);

            return GenreModelOutput.FromGenre(genre);
        }
    }
}
