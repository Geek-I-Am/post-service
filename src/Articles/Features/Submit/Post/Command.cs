using Articles.Features.Submit.Post.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Articles.Features.Submit.Post
{
    public class Command : IRequest<SingleResponse<Response>>
    {
        [FromBody] public PostBody Article { get; set; }
        
    }
}