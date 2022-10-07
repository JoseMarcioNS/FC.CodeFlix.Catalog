using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Delete
{
    public interface IDeleteGenre : IRequestHandler<DeleteGenreInput>
    {
    }
}
