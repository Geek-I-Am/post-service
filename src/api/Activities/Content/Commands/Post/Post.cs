using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Threenine.ApiResponse;

namespace Boleyn.Service.Activities.Posts.Commands.Post;

[Route(ResourceRoutes.Posts)]
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
        Tags = new[] { ResourceRoutes.Posts })
    ]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response))]
    public override async Task<ActionResult<SingleResponse<Response>>> HandleAsync([FromBody] Command request, CancellationToken cancellationToken = new())
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsValid)
        {
            return new CreatedResult(new Uri(string.Concat(ResourceRoutes.Posts, $"/{result.Item.Id}"), UriKind.Relative),
                new { result.Item });
        }

        return await HandleErrors(result.Errors);
    }
    
    private Task<ActionResult> HandleErrors(List<KeyValuePair<string, string[]>> errors)
    {
        ActionResult result = null;
        errors.ForEach(error =>
        {
            result = error.Key switch
            {
                "Conflict" => new ConflictResult(),
                _ => new BadRequestObjectResult(errors)
            };
        });
        return Task.FromResult<ActionResult>(result);
    }
}