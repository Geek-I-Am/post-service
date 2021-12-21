using System;

namespace GeekIAm.Exceptions
{
    [Serializable]
    public class NotFoundException : ArticlesException
    {
        public NotFoundException(string title, string message) : base(nameof(NotFoundException), message)
        {
        }
    }
}