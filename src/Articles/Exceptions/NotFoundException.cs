using System;
using Articles.Resources;

namespace Articles.Exceptions
{
    [Serializable]
    public class NotFoundException : ArticlesException
    {
        public NotFoundException(string title, string message) : base(ExceptionTitle.NotFound, message)
        {
        }
    }
}