using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Delete
{
    public class DeleteGenreInput : IRequest
    {
        public Guid Id { get; private set; }

        public DeleteGenreInput(Guid id) => Id = id;
    }
}
