using FluentValidation;

namespace Boleyn.Service.Activities.Posts.Commands.Post
{
    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Article.Title).NotEmpty().MaximumLength(75);
            RuleFor(x => x.Article.Summary).NotEmpty();
            RuleFor(x => x.Article.Summary)
                .MaximumLength(300)
                .WithMessage("The maximum allowed length for a summary is 300 characters");
        

            RuleFor(x => x.Article.Url).NotEmpty();
       
        }
        
    }
}