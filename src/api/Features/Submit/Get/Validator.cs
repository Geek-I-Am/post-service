using FluentValidation;

namespace Articles.Features.Submit.Get
{
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}