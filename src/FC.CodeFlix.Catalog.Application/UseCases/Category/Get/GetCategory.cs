using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Get
{
    public class GetCategory : IGetCategory
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategory(ICategoryRepository categoryRepository)
         => _categoryRepository = categoryRepository;


        public async Task<CategoryModelOuput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.Get(request.Id, cancellationToken);
            return CategoryModelOuput.FromCategory(result);
        }
    }
}
