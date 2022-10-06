using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;
namespace FC.CodeFlix.Catalog.Application.UseCases.Genre.Common
{
    public class GenreModelOutput
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<Guid>? CategoriesIds { get; private set; }

        public GenreModelOutput(Guid id,
            string name,
            bool isActive,
            DateTime createdAt,
            List<Guid>? categoriesIds)
        {
            Id = id;
            Name = name;
            IsActive = isActive;
            CreatedAt = createdAt;
            CategoriesIds = categoriesIds;
        }
        public static GenreModelOutput FromGenre(DomainEntity.Genre genre)
        {
            return new(genre.Id,
              genre.Name,
              genre.IsActive,
              genre.CreatedAt,
              genre.Categories.ToList());
        }
    }
}
