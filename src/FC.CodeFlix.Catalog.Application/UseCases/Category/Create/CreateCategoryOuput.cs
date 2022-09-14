using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Create
{
    public class CreateCategoryOuput
    {
        public CreateCategoryOuput(Guid id, string name, string description, bool isActive, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public static CreateCategoryOuput FromCategory(DomainEntity.Category category)
        {
           return new CreateCategoryOuput(
                category.Id,
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt);
        }
    }
      
}
