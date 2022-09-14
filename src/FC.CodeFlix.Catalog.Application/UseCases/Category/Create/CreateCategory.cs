using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Application.UseCases.Category.Create
{
    public class CreateCategory
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategory(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCategoryOuput> Handler(CreateCategoryInput input, CancellationToken cancellationToken)
        {
            var category = new DomainEntity.Category(
                input.Name,
                input.Description,
                input.IsActive);

            await _categoryRepository.Insert(category, cancellationToken);

            await _unitOfWork.Commit(cancellationToken);

            return new CreateCategoryOuput(
                category.Id, 
                category.Name,
                category.Description,
                category.IsActive,
                category.CreatedAt);
        }
    }
}
