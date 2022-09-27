using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FC.CodeFlix.Catalog.End2EndTests.Api.Category.Create
{
    [Collection(nameof(CreateCategoryTestFixture))]
    public class CreateCategoryTest : IDisposable
    {
        private readonly CreateCategoryTestFixture _fixture;

        public CreateCategoryTest(CreateCategoryTestFixture fixture)
        {
            _fixture = fixture;
            
        }

        [Fact(DisplayName = (nameof(CreateCategory)))]
        [Trait("End2End/Api", "Category/Create - Endpoints")]
        public async Task CreateCategory()
        {
            var input = _fixture.CreateCategoryInput();

            var (response, ouput) = await _fixture.ApiClient.Post<CategoryModelOuput>(
                 "/categories",
                 input
                 );
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.Created);
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

        [Theory(DisplayName = (nameof(ThrowWhenCannotCreateCategory)))]
        [Trait("End2End/Api", "Category/Create - Endpoints")]
        [MemberData(nameof(CreateCategoryTestGeneretorData.GetInvalidInputs),
            MemberType = typeof(CreateCategoryTestGeneretorData))]
        public async Task ThrowWhenCannotCreateCategory(CreateCategoryInput input, string messageError)
        {
            var (response, ouput) = await _fixture.ApiClient.Post<ProblemDetails>(
                "/categories",
                input
                );
            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            ouput.Should().NotBeNull();
            ouput!.Title.Should().Be("One or more validation errors ocurred");
            ouput.Type.Should().Be("UnprocessableEntity");
            ouput.Status.Should().Be(StatusCodes.Status422UnprocessableEntity);
            ouput.Detail.Should().Be(messageError);
        }
        public void Dispose()
        =>  _fixture.CleanInMemoryDatabase();
        
    }
}
