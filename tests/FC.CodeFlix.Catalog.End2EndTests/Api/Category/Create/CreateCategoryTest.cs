using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Create
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName =(nameof(CreateCategory)))]
        [Trait("End2End/Api","Category - Endpoints")]
        public async Task CreateCategory()
        {
            var input = _fixture.GetCategory();

           var (response,ouput) = await _fixture.ApiClient.Post<CategoryModelOuput>(
                "/categories",
                input
                );
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
            ouput.Should().NotBeNull();
            ouput!.Id.Should().NotBeEmpty();
            ouput.Name.Should().Be(input.Name);
            ouput.Description.Should().Be(input.Description);
            ouput.IsActive.Should().Be(input.IsActive);
            ouput.CreatedAt.Should().NotBeSameDateAs(default);
            var dbCategory = await _fixture.Persistence.GetById(ouput.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Id.Should().NotBeEmpty();
            dbCategory.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
            dbCategory.IsActive.Should().Be(input.IsActive);
            dbCategory.CreatedAt.Should().NotBeSameDateAs(default);

           
        }
    }
}
