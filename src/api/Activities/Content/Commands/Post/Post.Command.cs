using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Boleyn.Service.Activities.Posts.Commands.Post
{
    public class Command : IRequest<SingleResponse<Response>>
    {
        [FromBody] public Article Article { get; set; }
        
    }

    public class Article
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
       
    
    }
}
