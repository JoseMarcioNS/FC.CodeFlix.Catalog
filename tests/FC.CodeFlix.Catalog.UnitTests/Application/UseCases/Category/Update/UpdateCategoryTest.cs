using FC.CodeFlix.Catalog.Application.Excepitons;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.Domain.Entity;
using Xunit;

namespace FC.CodeFlix.Catalog.UnitTests.Application.UseCases.Category.Update
{
    [Collection(nameof(UpdateGategoryTestFixture))]
    public class UpdateCategoryTest
    {
        private readonly UpdateGategoryTestFixture _fiture;

        public UpdateCategoryTest(UpdateGategoryTestFixture fiture)
         => _fiture = fiture;

        [Theory(DisplayName = nameof(UpdateCategoryOk))]
        [Trait("Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
            parameters: 10,
            MemberType = typeof(UpdateCategoryTestGenerationData))
        ]
        public async void UpdateCategoryOk(DomainEntity.Category category, UpdateCategoryInput input)
        {
            var repositoryMock = _fiture.GetCategoryRepositoryMock();
            var unitOfWork = _fiture.GetUnitOfWorkMock();
            repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
            var useCase = new UpdateCategory(repositoryMock.Object, unitOfWork.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive!.Value);
            repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

        }
        [Theory(DisplayName = nameof(UpdateCategoryWithoutProvidingIsAcitve))]
        [Trait("Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
           parameters: 10,
           MemberType = typeof(UpdateCategoryTestGenerationData))
       ]
        public async void UpdateCategoryWithoutProvidingIsAcitve(DomainEntity.Category category, UpdateCategoryInput exampleCategory)
        {
            var repositoryMock = _fiture.GetCategoryRepositoryMock();
            var unitOfWork = _fiture.GetUnitOfWorkMock();
            var input = new UpdateCategoryInput(
                exampleCategory.Id,
                exampleCategory.Name,
                exampleCategory.Description
                );
            repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
            var useCase = new UpdateCategory(repositoryMock.Object, unitOfWork.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(category.IsActive);
            repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

        }
        [Theory(DisplayName = nameof(UpdateCategoryProvidingOnlyName))]
        [Trait("Application", "UpdateCategoryTest - UseCaes")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetCategoriesUpdate),
          parameters: 10,
          MemberType = typeof(UpdateCategoryTestGenerationData))
      ]
        public async void UpdateCategoryProvidingOnlyName(DomainEntity.Category category, UpdateCategoryInput exampleCategory)
        {
            var repositoryMock = _fiture.GetCategoryRepositoryMock();
            var unitOfWork = _fiture.GetUnitOfWorkMock();
            var input = new UpdateCategoryInput(
                exampleCategory.Id,
                exampleCategory.Name
                );
            repositoryMock.Setup(x => x.Get(category.Id, It.IsAny<CancellationToken>())).ReturnsAsync(category);
            var useCase = new UpdateCategory(repositoryMock.Object, unitOfWork.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            output.Should().NotBeNull();
            output.Id.Should().Be(input.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(category.Description);
            output.IsActive.Should().Be(category.IsActive);
            repositoryMock.Verify(x => x.Update(category, It.IsAny<CancellationToken>()), Times.Once);
            unitOfWork.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact(DisplayName = nameof(ThrowExceptioWhenNotFoundCategory))]
        [Trait("Application", "UpdateCategoryTest - UseCaes")]
        public async void ThrowExceptioWhenNotFoundCategory()
        {
            var repositoryMock = _fiture.GetCategoryRepositoryMock();
            var unitOfWork = _fiture.GetUnitOfWorkMock();
            var input = _fiture.GetCategory();
            repositoryMock.Setup(x => x.Get(input.Id, It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Category '{input.Id}' not found."));
            var useCase = new UpdateCategory(repositoryMock.Object, unitOfWork.Object);

            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();
            repositoryMock.Verify(x => x.Get(input.Id, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
