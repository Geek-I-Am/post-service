using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Boleyn.Service.Activities.Posts.Queries.Get;

[Route(ResourceRoutes.Posts)]
public class Get : EndpointBaseAsync.WithRequest<Query>.WithActionResult<Response>
{
    private readonly IMediator _mediator;

    public Get(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get an article by Id",
        Description = "get article to GeekIAm Article list",
        OperationId = "E5CD5320-B283-4E58-BE38-177389265D6D",
        Tags = new[] { ResourceRoutes.Posts })
    ]
    public override async Task<ActionResult<Response>> HandleAsync([FromRoute] Query request, CancellationToken cancellationToken = new CancellationToken())
    {
        var result = await _mediator.Send(request, cancellationToken);
        return result.IsValid ? new OkObjectResult(result.Item) : new BadRequestObjectResult(result.Errors);
    }
}