using System.Collections.Generic;
using GeekIAm.Resources;

namespace GeekIAm.Exceptions
{
    public class ValidationException : ArticlesException
    {
        public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(
            ExceptionTitle.Validation, message) =>
            Errors = errors;

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}