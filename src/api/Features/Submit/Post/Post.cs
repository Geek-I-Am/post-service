using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Threenine.ApiResponse;

namespace Geekiam.Posts.Service.Features.Submit.Post;

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
        Summary = "Post new article",
        Description = "Add new article to GeekIAm Article list",
        OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
        Tags = new[] { Routes.Submit })
    ]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response))]
    public override async Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromBody] Command request, CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsValid ? new AcceptedResult(new Uri(Routes.Submit, UriKind.Relative), new {result.Item.Title,  result.Item.Url }): new BadRequestObjectResult(result.Errors);
    }
}