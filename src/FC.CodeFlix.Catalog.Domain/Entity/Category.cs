using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.Validation;

namespace FC.CodeFlix.Catalog.Domain.Entity
{
    public class Category : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Category(string name, string description, bool isActive = true) : base()
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }
        public void Update(string name, string? description = null)
        {
            Name = name;
            Description = description ?? Description;
            Validate();
        }
        public void Active()
        {
            IsActive = true;
            Validate();
        }

        public void Deactivate()
        {
            IsActive = false;
            Validate();
        }
        private void Validate()
        {
            DomainValidation.ShouldNotBeNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLenght(Name, 3, nameof(Name));
            DomainValidation.MaxLenght(Name, 255, nameof(Name));

            DomainValidation.ShouldNotBeNull(Description, nameof(Description));
            DomainValidation.MaxLenght(Description, 10_000, nameof(Description));

        }
    }
}
