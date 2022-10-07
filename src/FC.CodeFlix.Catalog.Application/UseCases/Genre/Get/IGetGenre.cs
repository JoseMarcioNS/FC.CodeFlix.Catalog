using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Get
{
    public interface IGetGenre : IRequestHandler<GetGenreInput, GenreModelOutput>
    {
    }
}
