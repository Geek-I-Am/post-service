using System.Collections.Generic;
using Articles.Resources;

namespace Articles.Exceptions
{
    public class ValidationException : ArticlesException
    {
        public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(
            ExceptionTitle.Validation, message) =>
            Errors = errors;

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}