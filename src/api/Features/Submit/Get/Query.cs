using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Articles.Features.Submit.Get
{
    public class Query : IRequest<SingleResponse<SampleDetail>>
    {
        [FromRoute(Name = "id")] public string Id { get; set; }
    }
}