using System;
using System.Collections.Generic;

namespace Articles.Features.Submit.Post.Models
{
    public class PostBody
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public string Url { get; set; }
        
        public List<string> Tags { get; set; }
        
        

    }
}