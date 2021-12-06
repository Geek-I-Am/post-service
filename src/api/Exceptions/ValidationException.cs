using System.Collections.Generic;
using PostService.Resources;

namespace PostService.Exceptions
{
    public class ValidationException : ArticlesException
    {
        public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(
            ExceptionTitle.Validation, message) =>
            Errors = errors;

        public IReadOnlyDictionary<string, string[]> Errors { get; }
    }
}