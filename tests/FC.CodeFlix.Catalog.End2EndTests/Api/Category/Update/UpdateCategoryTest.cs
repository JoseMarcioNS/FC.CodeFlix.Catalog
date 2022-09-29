using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Update
{
    [Collection(nameof(UpdateCategoryTestFixture))]
    public class UpdateCategoryTest : IDisposable
    {
        private readonly UpdateCategoryTestFixture _fixture;

        public UpdateCategoryTest(UpdateCategoryTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(UpdateCategory))]
        [Trait("End2End/Api", "Category/Update - Endpoints")]
        public async Task UpdateCategory()
        {
            var categories = _fixture.GetListCategories();
            var category = categories[5];
            await _fixture.Persistence.InsertCategories(categories);
            var input = _fixture.GetUpdateCategoryInput(category.Id);

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOuput>(
                "/categories",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(category.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive!.Value);
            var dbCategory = await _fixture.Persistence.GetById(category.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
            dbCategory.IsActive.Should().Be(input.IsActive.Value);

        }
        [Fact(DisplayName = nameof(UpdateCategoryOnlyName))]
        [Trait("End2End/Api", "Category/Update - Endpoints")]
        public async Task UpdateCategoryOnlyName()
        {
            var categories = _fixture.GetListCategories();
            var category = categories[5];
            await _fixture.Persistence.InsertCategories(categories);
            var input = new UpdateCategoryInput(
                category.Id,
                _fixture.GetValidName(),
                category.Description,
                category.IsActive);

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOuput>(
                "/categories",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(category.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(category.Description);
            output.IsActive.Should().Be(category.IsActive);
            var dbCategory = await _fixture.Persistence.GetById(category.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(category.Description);
            dbCategory.IsActive.Should().Be(category.IsActive);

        }
        [Fact(DisplayName = nameof(UpdateCategoryNameAndDescripiton))]
        [Trait("End2End/Api", "Category/Update - Endpoints")]
        public async Task UpdateCategoryNameAndDescripiton()
        {
            var categories = _fixture.GetListCategories();
            var category = categories[5];
            await _fixture.Persistence.InsertCategories(categories);
            var input = new UpdateCategoryInput(
                category.Id,
                _fixture.GetValidName(),
                _fixture.GetValidDescription(),
                category.IsActive);

            var (response, output) = await _fixture.ApiClient.Put<CategoryModelOuput>(
                "/categories",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(category.Id);
            output.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(category.IsActive);
            var dbCategory = await _fixture.Persistence.GetById(category.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Id.Should().Be(category.Id);
            dbCategory.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
            dbCategory.IsActive.Should().Be(category.IsActive);

        }
        [Fact(DisplayName = nameof(ErrorWhenNotFoundCategory))]
        [Trait("End2End/Api", "Category/Update - Endpoints")]
        public async Task ErrorWhenNotFoundCategory()
        {
            var categories = _fixture.GetListCategories();
            await _fixture.Persistence.InsertCategories(categories);
            var input = new UpdateCategoryInput(
                 Guid.NewGuid(),
                _fixture.GetValidName(),
                _fixture.GetValidDescription());

            var (response, output) = await _fixture.ApiClient.Put<ProblemDetails>(
                "/categories",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Type.Should().Be("NotFound");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Detail.Should().Be($"Category '{input.Id}' not found.");

        }
        [Theory(DisplayName = nameof(ErrorProblesDatailsCategory))]
        [Trait("End2End/Api", "Category/Update - Endpoints")]
        [MemberData(nameof(UpdateCategoryTestGenerationData.GetInvalidInputs),
            MemberType = typeof(UpdateCategoryTestGenerationData))]
        public async Task ErrorProblesDatailsCategory(
            DomainEntity.Category category,
            UpdateCategoryInput input,
            string errorMessage)
        {
            var categories = _fixture.GetListCategories();
            categories.Add(category);
            await _fixture.Persistence.InsertCategories(categories);
           
            var (response, output) = await _fixture.ApiClient.Put<ProblemDetails>(
                "/categories",
                input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status422UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be(errorMessage);

        }
        public void Dispose()
        => _fixture.CleanDatabase();
    }
}
