using Boleyn.Service.Activities.Posts.Commands.Post;
using Xunit;
using FluentValidation.TestHelper;

namespace Api.Tests;
public class ValidatorTests
{
    private readonly Validator _validator;
    public ValidatorTests()
    {
        _validator = new Validator();
    }

  
}