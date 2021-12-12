using GeekIAm.Features.Submit.Post.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace GeekIAm.Features.Submit.Post
{
    public class Command : IRequest<SingleResponse<Response>>
    {
        [FromBody] public PostBody Article { get; set; }
        
    }
}