using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Threenine.ApiResponse;

namespace Geekiam.Posts.Service.Features.Submit.Post;

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
    public Metadata Metadata { get; set; }
    
}

public class Metadata
{
    public IList<string> Tags { get; set; }
    public IList<string> Categories { get; set; }
    
    
}