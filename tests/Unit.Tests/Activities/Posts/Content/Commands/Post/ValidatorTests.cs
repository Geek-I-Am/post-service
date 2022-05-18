using System.ComponentModel;
using Boleyn.Service.Activities.Posts.Commands.Post;
using FluentValidation.TestHelper;
using Xunit;

namespace Api.Tests.Content.Commands.Post;

public class ValidatorTests
{
    private readonly Validator _validator;
    public ValidatorTests()
    {
        _validator = new Validator();
    }
    
    [Theory, Description("SHould fail for empty or for strings over 75 characters")]
    [InlineData("")]
    [InlineData("1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 ")]
    [InlineData("REST API’s often require a PATCH method to partially update resources. A typical scenario")]
    public void Should_Have_Validation_Error_For_Title(string testString)
    {
        var command = new Command { Article = new Article { Title = testString } };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Article.Title);
    } 
    
    [Theory, Description("SHould fail for empty or for strings over 300 characters")]
    [InlineData("")]
    [InlineData("1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890")]
    
    public void Should_Have_Validation_Error_For_Summary(string testString)
    {
        var command = new Command { Article = new Article { Summary = testString } };

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Article.Summary);
    } 
    
    [Theory, Description("SHould fail for empty or for strings over 300 characters")]
    [InlineData("")]
    [InlineData("1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890")]
     
    public void Should_Have_Validation_Error_For_Url(string testString)
    {
        var command = new Command { Article = new Article { Url = testString } };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Article.Url);
    } 
    
}