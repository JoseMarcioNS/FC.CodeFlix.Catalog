using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.Domain.Interfaces;
using FC.CodeFlix.Catalog.UnitTests.Common;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [CollectionDefinition(nameof(UpdateGategoryTestFixture))]
    public class UpdateCategoryColletionFixture : ICollectionFixture<UpdateGategoryTestFixture>
    { }
    public class UpdateGategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetCategoryRepositoryMock() => new();
        public Mock<IUnitOfWork> GetUnitOfWorkMock() => new();

        public string GetValidName()
        {
            var name = "";
            while (name.Length < 3)
                name = Faker.Commerce.Categories(1)[0];

            if (name.Length > 255)
                name = name[..255];

            return name;
        }
        public string GetValidDescription()
        {
            var description = Faker.Commerce.ProductDescription();
            if (description.Length > 10_000)
                description = description[..10_000];

            return description;
        }
        public bool GetRandomActive() => new Random().NextDouble() > 0.5;

        public DomainEntity.Category GetValidCategory()
            => new(GetValidName(),
                  GetValidDescription(),
                  GetRandomActive());

        public UpdateCategoryInput GetCategory(Guid? id = null)
            => new(
                id ?? Guid.NewGuid(),
                GetValidName(),
                GetValidDescription(),
                GetRandomActive()
                );
    }
}
