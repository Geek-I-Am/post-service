using System;
using GeekIAm.Resources;

namespace GeekIAm.Exceptions
{
    [Serializable]
    public class NotFoundException : ArticlesException
    {
        public NotFoundException(string title, string message) : base(ExceptionTitle.NotFound, message)
        {
        }
    }
}