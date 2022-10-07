using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.List
{
    public interface IListGenres : IRequestHandler<ListGenresInput, ListGenresOutput>
    {
    }
}
