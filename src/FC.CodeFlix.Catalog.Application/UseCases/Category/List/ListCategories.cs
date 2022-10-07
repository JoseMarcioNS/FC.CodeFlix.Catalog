using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.List
{
    public class ListCategories : IListCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository)
        => _categoryRepository = categoryRepository;

        public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
        {
            var searchOutput = await _categoryRepository.Search(request.ToSearchInput(), cancellationToken);
            return ListCategoriesOutput.FromSearchOutput(searchOutput);
        }
    }
}
