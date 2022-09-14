using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Create
{
    public class CreateCategoryInput : IRequest<CreateCategoryOuput>
    {
        public CreateCategoryInput(string name, string? description, bool isActive = true)
        {
            Name = name;
            Description = description ?? "";
            IsActive = isActive;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
