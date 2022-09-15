using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Common
{
    public class CategoryModelOuput
    {
        public CategoryModelOuput(Guid id, string name, string description, bool isActive, DateTime createdAt)
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

        public static CategoryModelOuput FromCategory(DomainEntity.Category category)
        {
            return new(
                 category.Id,
                 category.Name,
                 category.Description,
                 category.IsActive,
                 category.CreatedAt);
        }
    }
}

