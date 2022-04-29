using FluentValidation.TestHelper;
using Geekiam.Posts.Service.Features.Posts.Post;
using Xunit;

namespace TestProject1.Features.Submit;

public class ValidatorTests
{
    private readonly Validator _validator;
    public ValidatorTests()
    {
        _validator = new Validator();
    }

    [Fact]
    public void ShouldThrowValidationErrorForEmptySummary()
    {

        var command = new Command
        {
            Article = new Article
            {
                Content = "sdfasfafsd",
                Summary = "",
                Title = "sdfadsfad"

            }
        };

        _validator.ShouldHaveValidationErrorFor(x => x.Article.Summary, command);
        

    }
  
}