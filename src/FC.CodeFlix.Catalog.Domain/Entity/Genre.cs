using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.Validation;

namespace FC.CodeFlix.Catalog.Domain.Entity
{
    public class Genre : AggregateRoot
    {
        public string Name { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; set; }
        public IReadOnlyList<Guid> Categories => _categories.AsReadOnly();

        private List<Guid> _categories;

        public Genre(string name, bool isActive = true)
        {
            Name = name;
            IsActive = isActive;
            CreatedAt = DateTime.Now;
            _categories = new List<Guid>();
            Validate();
        }
        public void Update(string name)
        {
            Name = name;
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
        public void AddCategory(Guid categaryId)
        {
            _categories.Add(categaryId);
            Validate();
        }
        public void RemoveCategory(Guid categaryId)
        {
            _categories.Remove(categaryId);
            Validate();
        }

        public void RemoveAllCategories()
        {
            _categories.Clear();
            Validate();
        }
        public void Validate()
        => DomainValidation.ShouldNotBeNullOrEmpty(Name, nameof(Name));

    }
}
