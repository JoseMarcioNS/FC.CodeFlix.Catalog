using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Interfaces;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Update
{
    public class UpdateCategory : IUpdateCategory
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork) =>
          (_categoryRepository, _unitOfWork) = (categoryRepository, unitOfWork);

        public async Task<CategoryModelOuput> Handle(UpdateCategoryInput request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.Get(request.Id, cancellationToken);
            category.Update(request.Name, request.Description);
            if (request.IsActive.HasValue)
                if (request.IsActive.Value)
                    category.Active();
                else
                    category.Deactivate();

            await _categoryRepository.Update(category, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return CategoryModelOuput.FromCategory(category);
        }
    }
}
