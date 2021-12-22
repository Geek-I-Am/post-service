using System;
using System.Collections.Generic;

namespace Geekiam.Posts.Service.Features.Submit.Post.Models;

public class PostBody
{
    public string Title { get; set; }
    public string Summary { get; set; }
    public string Author { get; set; }
    public DateTime Published { get; set; }
    public string Url { get; set; }
        
    public List<string> Tags { get; set; }
    public List<string> Categories { get; set; }
        
        

}