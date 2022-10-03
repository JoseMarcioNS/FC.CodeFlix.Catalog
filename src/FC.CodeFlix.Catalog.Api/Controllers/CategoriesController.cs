using FC.CodeFlix.Catalog.Api.Models;
using FC.CodeFlix.Catalog.Api.Models.Responses;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Delete;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Get;
using FC.CodeFlix.Catalog.Application.UseCases.Category.List;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
using FC.CodeFlix.Catalog.Domain.SeedWork.SearchableRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FC.CodeFlix.Catalog.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        => _mediator = mediator;

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CategoryModelOuput>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(nameof(Create), output.Id, new ApiResponse<CategoryModelOuput>(output));
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryModelOuput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid Id,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetCategoryInput(Id), cancellationToken);
            return Ok(new ApiResponse<CategoryModelOuput>(output));
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryInput(id), cancellationToken);
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<CategoryModelOuput>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update(
            [FromBody] UpdateCategoryApiInput input,
            [FromRoute] Guid id,
            CancellationToken cancellationToken)
        {
            var category = new UpdateCategoryInput(id, input.Name, input.Description, input.IsActive);
            var output = await _mediator.Send(category, cancellationToken);
            return Ok(new ApiResponse<CategoryModelOuput>(output));
        }
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseList<ListCategoriesOutput>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List(
            CancellationToken cancellationToken,
           [FromQuery] int? page,
           [FromQuery(Name = "per_page")] int? perPage,
           [FromQuery] string? search,
           [FromQuery] string? sort,
           [FromQuery] SearchOrder? dir

            )
        {
            var input = new ListCategoriesInput();
            if (page is not null) input.Page = page.Value;
            if (perPage is not null) input.PerPage = perPage.Value;
            if (!string.IsNullOrWhiteSpace(search)) input.Search = search;
            if (!string.IsNullOrWhiteSpace(sort)) input.Sort = sort;
            if (dir is not null) input.Dir = dir.Value;

            var output = await _mediator.Send(input, cancellationToken);
            var response = new ApiResponseList<CategoryModelOuput>(output);
            return Ok(response);
        }
    }
}