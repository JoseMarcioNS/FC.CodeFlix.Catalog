using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Create
{
    public class CreateGenreInput : IRequest<GenreModelOutput>
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public List<Guid>? CategoriesIds { get; private set; }

        public CreateGenreInput(
            string name,
            bool isActive,
            List<Guid>? categoriesIds = null)
        {
            Name = name;
            IsActive = isActive;
            CategoriesIds = categoriesIds;
        }
    }
}
