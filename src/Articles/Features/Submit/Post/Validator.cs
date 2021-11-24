using FluentValidation;

namespace Articles.Features.Submit.Post
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Article.Title).NotEmpty().MaximumLength(75);
            RuleFor(x => x.Article.Summary).NotEmpty().MaximumLength(300);
            RuleFor(x => x.Article.Author).NotEmpty().MaximumLength(60);
            RuleFor(x => x.Article.Url).NotEmpty().MaximumLength(286);

        }
        
    }
}