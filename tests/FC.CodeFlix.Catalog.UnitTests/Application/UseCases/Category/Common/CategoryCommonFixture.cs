using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.SharedTests;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Common
{
    public class CategoryCommonFixture : CategoryBaseFixture
    {
        public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

    }
}
