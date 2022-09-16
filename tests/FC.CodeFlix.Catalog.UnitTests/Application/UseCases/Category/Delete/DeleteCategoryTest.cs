using FC.CodeFlix.Catalog.Application.UseCases.Category.Delete;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Delete
{
    [Collection(nameof(DeleteCategoryTestFixure))]
    public class DeleteCategoryTest
    {
        private readonly DeleteCategoryTestFixure _fixure;

        public DeleteCategoryTest(DeleteCategoryTestFixure fixure)
           => _fixure = fixure;

        [Fact(DisplayName = nameof(DeleteCategoryOk))]
        [Trait("Application", "DeleteCategoryTest - UseCases")]
        public async void DeleteCategoryOk()
        {
            var repository = _fixure.GetCategoryRepositoryMock();
            var unitOfWork = _fixure.GetUnitOfWorkMock();
            var category = _fixure.GetValidCategory();
            repository.Setup(x => x.Get(category.Id,It.IsAny<CancellationToken>())).ReturnsAsync(category);
            var input = new DeleteCategoryInput(category.Id);
            var useCase = new DeleteCategory(repository.Object, unitOfWork.Object);

            await useCase.Handle(input, CancellationToken.None);

            repository.Verify(x => x.Get(category.Id, It.IsAny<CancellationToken>()), Times.Once);
            repository.Verify(x => x.Delete(category, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
