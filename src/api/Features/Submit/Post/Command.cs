using MediatR;
using Microsoft.AspNetCore.Mvc;
using PostService.Features.Submit.Post.Models;
using Threenine.ApiResponse;

namespace PostService.Features.Submit.Post
{
    public class Command : IRequest<SingleResponse<Response>>
    {
        [FromBody] public PostBody Article { get; set; }
        
    }
}