using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.End2EndTests.Extentions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Get
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryTest : IDisposable
    {
        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryTest(GetCategoryTestFixture fixture)
        => _fixture = fixture;

        [Fact(DisplayName = nameof(Get))]
        [Trait("End2End/Api", "Category/Get - Endpoints")]
        public async Task Get()
        {
            var categories = _fixture.GetListCategories();
            var category = categories[5];
            await _fixture.Persistence.InsertCategories(categories);

            var (response, output) = await _fixture.ApiClient.Get<CategoryModelOuput>(
                $"Categories/{category.Id}"
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status200OK);
            output.Should().NotBeNull();
            output!.Id.Should().Be(category.Id);
            output.Name.Should().Be(category.Name);
            output.Description.Should().Be(category.Description);
            output.IsActive.Should().Be(category.IsActive);
            output.CreatedAt.TrimMilliseconds().Should().Be(category.CreatedAt.TrimMilliseconds());
        }
        [Fact(DisplayName = nameof(ErrorWhenNotFoundCategory))]
        [Trait("End2End/Api", "Category/Get - Endpoints")]
        public async Task ErrorWhenNotFoundCategory()
        {
            var categories = _fixture.GetListCategories();
            var id = Guid.NewGuid();
            await _fixture.Persistence.InsertCategories(categories);

            var (response, output) = await _fixture.ApiClient.Get<ProblemDetails>(
                $"Categories/{id}"
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be($"Not Found");
            output.Type.Should().Be("NotFound");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Detail.Should().Be($"Category '{id}' not found.");

        }
        public void Dispose()
        => _fixture.CleanDatabase();
    }
}
