using System.Collections.Generic;

namespace Geekiam.Posts.Service.Exceptions;

public class ValidationException : ArticlesException
{
    public ValidationException(string message, IReadOnlyDictionary<string, string[]> errors) : base(
        nameof(ValidationException), message) =>
        Errors = errors;

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}