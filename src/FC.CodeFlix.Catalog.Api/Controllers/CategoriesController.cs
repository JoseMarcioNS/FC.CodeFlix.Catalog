using FC.CodeFlix.Catalog.Application.UseCases.Category.Common;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Create;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Delete;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Get;
using FC.CodeFlix.Catalog.Application.UseCases.Category.Update;
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
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return CreatedAtAction(nameof(Create), output.Id, output);
        }
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get([FromRoute] Guid Id,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(new GetCategoryInput(Id), cancellationToken);
            return Ok(output);
        }
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id, 
            CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryInput(id),cancellationToken);
            return NoContent();
        }
        [HttpPut]
        [ProducesResponseType(typeof(CategoryModelOuput), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryInput input,
            CancellationToken cancellationToken)
        {
            var output = await _mediator.Send(input, cancellationToken);
            return Ok(output);
        }
    }
}