using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Delete
{
    [Collection(nameof(DeleteCategoryTestFixture))]
    public class DeleteCategoryTest : IDisposable
    {
        private readonly DeleteCategoryTestFixture _fixture;

        public DeleteCategoryTest(DeleteCategoryTestFixture fixture)
         => _fixture = fixture;

        [Fact(DisplayName = nameof(DeleteCategory))]
        [Trait("End2End/Api", "Category/Delete - Endpoints")]
        public async Task DeleteCategory()
        {
            var categories = _fixture.GetListCategories();
            var category = categories[5];
            await _fixture.Persistence.InsertCategories(categories);

            var (response, output) = await _fixture.ApiClient.Delete<Object>(
                $"Categories/{category.Id}");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status204NoContent);
            output.Should().BeNull();
            var dbCategory = await _fixture.Persistence.GetById(category.Id);
            dbCategory.Should().BeNull();

        }
        [Fact(DisplayName = nameof(ErrorWhenNotFoundCategory))]
        [Trait("End2End/Api", "Category/Delete - Endpoints")]
        public async Task ErrorWhenNotFoundCategory()
        {
            var categories = _fixture.GetListCategories();
            var id = Guid.NewGuid();
            await _fixture.Persistence.InsertCategories(categories);

            var (response, output) = await _fixture.ApiClient.Delete<ProblemDetails>(
                $"Categories/{id}");

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be((HttpStatusCode)StatusCodes.Status404NotFound);
            output.Should().NotBeNull();
            output!.Title.Should().Be("Not Found");
            output.Type.Should().Be("NotFound");
            output.Status.Should().Be(StatusCodes.Status404NotFound);
            output.Detail.Should().Be($"Category '{id}' not found.");

        }
        public void Dispose()
        => _fixture.CleanInMemoryDatabase();
    }
}
