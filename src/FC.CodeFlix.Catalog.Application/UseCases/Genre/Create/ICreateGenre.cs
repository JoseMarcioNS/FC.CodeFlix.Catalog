using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Create
{
    public interface ICreateGenre : IRequestHandler<CreateGenreInput, GenreModelOutput>
    {}
}
