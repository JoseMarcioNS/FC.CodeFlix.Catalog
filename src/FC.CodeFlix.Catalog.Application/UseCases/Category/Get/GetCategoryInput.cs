using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Get
{
    public class GetCategoryInput : IRequest<CategoryModelOuput>
    {
        public Guid Id { get; set; }
        public GetCategoryInput(Guid id) => Id = id;

    }
}
