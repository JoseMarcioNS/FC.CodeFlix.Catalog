using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Get
{
    public class GetGenreInput : IRequest<GenreModelOutput>
    {
        public Guid Id { get; private set; }

        public GetGenreInput(Guid id) => Id = id;
    }
}
