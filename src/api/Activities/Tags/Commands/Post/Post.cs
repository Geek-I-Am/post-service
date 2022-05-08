using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Threenine.ApiResponse;

namespace Boleyn.Service.Activities.Tags.Commands.Post;

[Route(ResourceRoutes.Posts)]
public class Post : EndpointBaseAsync.WithRequest<Command>.WithActionResult<SingleResponse<Response>>
{
    private readonly IMediator _mediator;

    public Post(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("{id:guid}/" + ResourceRoutes.Tags)]
    [SwaggerOperation(
        Summary = "Post",
        Description = "Post",
        OperationId = "d58e3197-1154-4c48-9a0c-5c78206c842f",
        Tags = new[] { ResourceRoutes.Posts })
    ]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response))]
    public override async Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromRoute] Command request, CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(request, cancellationToken);
       
        return result.IsValid ? new CreatedResult(new Uri(string.Concat(ResourceRoutes.Tags, "/", result.Item.Id), UriKind.Relative), new { id = result.Item.Id }): new BadRequestObjectResult(result.Errors);
    }
}
