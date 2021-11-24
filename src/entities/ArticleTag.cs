using System;

namespace Entities
{
    public class ArticleTag
    {
        public int Id { get; set; }
        public Guid ArticleId { get; set; }
        public Guid TagId { get; set; }
        
    }
}