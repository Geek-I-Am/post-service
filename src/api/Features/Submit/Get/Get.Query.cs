using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Geekiam.Posts.Service.Features.Submit.Get;

public class Query : IRequest<SingleResponse<Response>>
{
    [FromRoute(Name = "id")] public Guid Id { get; set; }
}