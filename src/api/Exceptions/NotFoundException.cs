using System;
using PostService.Resources;

namespace PostService.Exceptions
{
    [Serializable]
    public class NotFoundException : ArticlesException
    {
        public NotFoundException(string title, string message) : base(ExceptionTitle.NotFound, message)
        {
        }
    }
}