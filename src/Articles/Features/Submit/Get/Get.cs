using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Articles.Features.Submit.Get;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Articles.Features.Sample
{
    [Route(Routes.Submit)]
    public class Get : BaseAsyncEndpoint.WithRequest<Query>.WithResponse<SampleDetail>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retrieve a sample response by id ",
            Description = "Retrieves a sample response ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] { Routes.Submit })
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SampleDetail))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(NotFoundResult))]
        [Produces("application/json")]
        public override async Task<ActionResult<SampleDetail>> HandleAsync([FromRoute] Query query,
            CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(query, cancellationToken);
            return result.IsValid ? new OkObjectResult(result.Item) : new BadRequestObjectResult(result.Errors);
        }
    }
}