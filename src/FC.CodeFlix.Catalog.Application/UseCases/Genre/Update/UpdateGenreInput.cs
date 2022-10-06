using FC.CodeFlix.Catalog.Application.UseCases.Genre.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Update
{
    public class UpdateGenreInput : IRequest<GenreModelOutput>
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool? IsActive { get; private set; }
        public List<Guid>? CategoriesIds { get; private set; }
        public UpdateGenreInput(
            Guid id, 
            string name, 
            bool? isActive = null,
            List<Guid>? categoriesIds =null
            )
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            CategoriesIds = categoriesIds;
        }

    }
}
