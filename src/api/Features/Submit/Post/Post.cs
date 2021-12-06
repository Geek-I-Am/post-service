using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Threenine.ApiResponse;

namespace PostService.Features.Submit.Post
{
    [Route(Routes.Submit)]
    public class Post : BaseAsyncEndpoint.WithRequest<Command>.WithResponse<SingleResponse<Response>>
    {
        private readonly IMediator _mediator;

        public Post(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Retrieve a sample response by id ",
            Description = "Retrieves a sample response ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] { Routes.Submit })
        ]
        [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response))]
        public async override Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromBody] Command request, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result.IsValid ? new AcceptedResult(): new BadRequestObjectResult(result.Errors);
        }
    }
}