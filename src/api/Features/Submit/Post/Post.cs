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
public class Post : EndpointBaseAsync.WithRequest<Command>.WithActionResult<SingleResponse<Response>>
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
        OperationId = "153E384B-7EF5-4EC8-BFD7-E10E7F47E6C1",
        Tags = new[] { Routes.Submit })
    ]
    [ProducesResponseType(StatusCodes.Status202Accepted, Type = typeof(Response))]
    public override async Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromBody] Command request, CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsValid ? new AcceptedResult(new Uri(Routes.Submit, UriKind.Relative), new {result.Item.Title,  result.Item.Url }): new BadRequestObjectResult(result.Errors);
    }
}