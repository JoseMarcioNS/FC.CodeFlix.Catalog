using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Update
{
    public interface IUpdateGenre : IRequestHandler<UpdateGenreInput,GenreModelOutput>
    {
    }
}
