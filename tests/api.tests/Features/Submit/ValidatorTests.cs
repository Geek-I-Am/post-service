using FluentValidation.TestHelper;
using Geekiam.Posts.Service.Features.Submit.Post;
using Geekiam.Posts.Service.Features.Submit.Post.Models;
using Xunit;

namespace TestProject1.Features.Submit;

public class ValidatorTests
{
    private readonly Validator _validator;
    public ValidatorTests()
    {
        _validator = new Validator();
    }

    [Fact(DisplayName = "Should have a validation error for empty id")]
       
    public void Should_have_validation_error_for_empty_id()
    {
        var command = new Command{ Article = new PostBody{ Url = "silly string"}};
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Article.Url);
    }
}