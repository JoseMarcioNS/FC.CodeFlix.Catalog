using FC.CodeFlix.Catalog.Domain.Entity;
namespace FC.CodeFlix.Catalog.Infra.Data.EF.Models
{
    public class GenresCategories
    {
        public GenresCategories(Guid genreId, Guid categoryId)
        {
            GenreId = genreId;
            CategoryId = categoryId;
        }
        public Guid GenreId { get; private set; }
        public Genre? Genre { get; private set; }
        public Guid CategoryId { get; private set; }
        public Category? Category { get; private set; }
    }
}
