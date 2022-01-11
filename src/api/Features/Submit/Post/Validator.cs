using System;
using FluentValidation;

namespace Geekiam.Posts.Service.Features.Submit.Post;

public class Validator : AbstractValidator<Command>
{
    public Validator()
    {
        RuleFor(x => x.Post.Article.Title).NotEmpty().MaximumLength(75);
        RuleFor(x => x.Post.Body.Summary).NotEmpty()
            .MaximumLength(300)
            .WithMessage("The maximum allowed length for a summary is 300 characters");
            
     

        RuleFor(x => x.Post.Article.Url).NotEmpty();
        /*RuleFor(x => x.Article.Url)
            .MinimumLength(2)
            .MaximumLength(286);
            
            
        RuleFor(x => x.Article.Url)
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.Article.Url))
            .WithMessage("A valid url is required");*/
    }
        
}