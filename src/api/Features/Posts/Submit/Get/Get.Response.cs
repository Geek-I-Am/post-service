using System.Collections.Generic;

namespace Geekiam.Posts.Service.Features.Posts.Get;

public class Response 
{
    public string Summary { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public IList<string> Tags { get; set; }
    public IList<string> Categories { get; set; }

}